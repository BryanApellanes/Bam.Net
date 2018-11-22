using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    [Serializable]
    public class Journal: Loggable
    {
        Queue<JournalEntry> _journalEntryQueue;
        bool _keepFlushing;

        public Journal(ISequenceProvider sequenceProvider, IJournalEntryValueFlusher flusher, IJournalEntryValueLoader loader, ITypeConverter typeConverter, ILogger logger = null) 
            : this(SystemPaths.Get(DefaultDataDirectoryProvider.Current), new JournalTypeMap(SystemPaths.Get(DefaultDataDirectoryProvider.Current)), sequenceProvider, flusher, loader, typeConverter, logger)
        { }

        public Journal(SystemPaths paths, JournalTypeMap typeMap, ISequenceProvider sequenceProvider, IJournalEntryValueFlusher flusher, IJournalEntryValueLoader loader, ITypeConverter typeConverter = null, ILogger logger = null)
        {
            _journalEntryQueue = new Queue<JournalEntry>();
            _keepFlushing = true;
            FlushCycleDelay = 300;
            ExceptionThreshold = 100;
            SequenceProvider = sequenceProvider;
            Paths = paths;
            TypeMap = typeMap;
            Flusher = flusher;
            Loader = loader;
            TypeConverter = typeConverter ?? new DefaultTypeConverter();
            Logger = logger;
            AppDomain.CurrentDomain.DomainUnload += (o, a) => _keepFlushing = false;
            FlushQueue();
        }

        SystemPaths _paths;
        protected internal SystemPaths Paths
        {
            get
            {
                return _paths;
            }
            set
            {
                _journalDirectory = null;
                _paths = value;
            }

        }
        protected internal JournalTypeMap TypeMap { get; set; }
        protected internal ISequenceProvider SequenceProvider { get; set; }

        DirectoryInfo _journalDirectory;
        public DirectoryInfo JournalDirectory
        {
            get
            {
                if(_journalDirectory == null)
                {
                    _journalDirectory = new DirectoryInfo(Path.Combine(Paths.Data.AppData, GetType().Name, nameof(JournalEntry).Pluralize()));
                }
                return _journalDirectory;
            }
        }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait for the next check for entries waiting
        /// to be flushed.
        /// </summary>
        /// <value>
        /// The flush cycle delay.
        /// </value>
        public int FlushCycleDelay { get; set; }

        public ITypeConverter TypeConverter { get; set; }
        
        public IJournalEntryValueFlusher Flusher { get; internal set; }

        public IJournalEntryValueLoader Loader { get; internal set; }

        public ILogger Logger { get; set; }

        public string GetTypeName(long typeId)
        {
            return TypeMap.GetTypeName(typeId);
        }

        public string GetPropertyName(long propId)
        {
            return TypeMap.GetPropertyName(propId);
        }

        public string GetTypeShortName(long typeId)
        {
            return TypeMap.GetTypeShortName(typeId);
        }

        public string GetPropertyShortName(long propId)
        {
            return TypeMap.GetPropertyShortName(propId);
        }

        public T LoadInstance<T>(JournalEntryInfo info) where T : KeyHashAuditRepoData, new()
        {
            return LoadInstance<T>(info.InstanceId);
        }

        public T LoadInstance<T>(JournalEntry entry) where T : KeyHashAuditRepoData, new()
        {
            return LoadInstance<T>(entry.InstanceId);
        }

        /// <summary>
        /// Reads the entry from disk by determining what the Id is using GetULongKeyHash.  Keys must be 
        /// made of one or more properties addorned with CompositeKeyAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public T LoadInstance<T>(T instance) where T: KeyHashAuditRepoData, new()
        {
            return LoadInstance<T>(instance.GetULongKeyHash());
        }

        /// <summary>
        /// Reads the entry from disk by determining what the Id is using GetULongKeyHash.  Keys must be 
        /// made of one or more properties addorned with CompositeKeyAttribute.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T LoadInstance<T>(ulong id) where T : KeyHashAuditRepoData, new()
        {
            T toLoad = new T();
            List<JournalEntry> instanceEntries = JournalEntry.LoadInstanceEntries<T>(id, this).ToList();
            foreach (JournalEntry entry in instanceEntries)
            {
                string propertyName = TypeMap.GetPropertyShortName(entry.PropertyId);
                if (string.IsNullOrEmpty(propertyName))
                {
                    Log.Warn("Failed to get property name for property id {0}", entry.PropertyId);
                    continue;
                }
                PropertyInfo prop = typeof(T).GetProperty(propertyName);
                if(prop == null)
                {
                    Log.Warn("Failed to get property {0}.{1}", typeof(T).Name, propertyName);
                    continue;
                }
                try
                {
                    object value = TypeConverter.ChangeType(entry.Value, prop.PropertyType);
                    toLoad.Property(propertyName, value);
                }
                catch (Exception ex)
                {
                    Log.AddEntry("Failed to set property ({0} ({1})) on instance ({2}) of type ({3})", ex, propertyName, prop?.Name, id.ToString(), typeof(T).FullName);
                }
            }
            toLoad.Id = id;
            return toLoad;
        }

        /// <summary>
        /// Gets the journal entry file for the specified journal entry.
        /// </summary>
        /// <param name="journalEntry">The journal entry.</param>
        /// <returns></returns>
        public FileInfo GetJournalEntryFileInfo(JournalEntry journalEntry)
        {
            return journalEntry.GetFileInfo(JournalDirectory, TypeMap);
        }

        /// <summary>
        /// Gets or sets the number of exceptions to tolerate while flushing the queue before stopping the flush thread.
        /// </summary>
        /// <value>
        /// The exception threshold.
        /// </value>
        public int ExceptionThreshold { get; set; }

        public IEnumerable<JournalEntry> Enqueue(KeyHashAuditRepoData data)
        {
            return Enqueue(data, (e) => { });
        }

        public IEnumerable<JournalEntry> Enqueue(KeyHashAuditRepoData data, Action<JournalEntry[]> onFullyFlushed)
        {
            TypeMap.AddMapping(data);
            
            JournalEntry[] journalEntries = JournalEntry.FromInstance(data, this).ToArray();
            return Enqueue(journalEntries, onFullyFlushed);
        }

        internal protected IEnumerable<JournalEntry> Enqueue(JournalEntry[] journalEntries, Action<JournalEntry[]> onFullyFlushed)
        {
            HashSet<JournalEntry> written = new HashSet<JournalEntry>();
            int doneCount = journalEntries.Length;
            foreach (JournalEntry entry in journalEntries)
            {
                entry.Written += (o, a) =>
                {
                    written.Add(((JournalEntryWrittenEventArgs)a).JournalEntry);
                    if (written.Count == doneCount)
                    {
                        onFullyFlushed(written.ToArray());
                    }
                };
            }
            EnqueueEntriesForWrite(journalEntries);
            return journalEntries;
        }

        public int QueueLength
        {
            get
            {
                return _journalEntryQueue.Count;
            }
        }

        public DirectoryInfo GetTypeDirectory(long typeId)
        {
            return JournalEntry.GetTypeDirectory(this, typeId);
        }

        public event EventHandler EntryFlushed;

        bool _fireQueueEmpty;
        public event EventHandler QueueEmpty;

        public event EventHandler ExceptionThresholdReached;

        public event EventHandler EntriesEnqueued;

        protected internal void EnqueueEntriesForWrite(params JournalEntry[] journalEntries)
        {
            foreach(JournalEntry journalEntry in journalEntries)
            {
                journalEntry.Seq = SequenceProvider.Next();
                _journalEntryQueue.Enqueue(journalEntry);
            }
            FireEvent(EntriesEnqueued, new JournalEntriesEnqueuedEventArgs { JournalEntries = journalEntries });
        }
        
        protected internal Thread QueueFlusher
        {
            get;
            set;
        }

        int _exceptionCount;
        private void FlushQueue()
        {
            QueueFlusher = Exec.InThread(() =>
            {
                while (_keepFlushing)
                {
                    try
                    {
                        if (_journalEntryQueue.Count > 0)
                        {
                            _fireQueueEmpty = true;
                            while (_journalEntryQueue.Count > 0)
                            {
                                JournalEntry journalEntry = _journalEntryQueue.Dequeue();
                                if (journalEntry != null)
                                {
                                    FileInfo propertyFile = Flusher.Flush(this, journalEntry);
                                    JournalEntryWrittenEventArgs eventArgs = new JournalEntryWrittenEventArgs { JournalEntry = journalEntry, File = propertyFile };
                                    FireEvent(EntryFlushed, eventArgs);
                                    journalEntry.OnEntryWritten(this, eventArgs);
                                    Log.DebugInfo("Entry flushed: {0}", journalEntry.ToString());
                                }
                            }
                        }
                        else
                        {
                            if (_fireQueueEmpty)
                            {
                                _fireQueueEmpty = false;
                                _exceptionCount = 0;
                                FireEvent(QueueEmpty, EventArgs.Empty);
                            }
                            Log.DebugInfo("Journal Flush thread sleeping: {0}", FlushCycleDelay);
                            Thread.Sleep(FlushCycleDelay);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exceptionCount++;
                        Log.DebugError("Exception in {0}.{1}: Count {2}", ex, nameof(Journal), nameof(FlushQueue), _exceptionCount.ToString());
                        Logger.AddEntry("Exception in {0}.{1}: Count {2}", ex, nameof(Journal), nameof(FlushQueue), _exceptionCount.ToString());
                        if (_exceptionCount >= ExceptionThreshold)
                        {
                            _keepFlushing = false;
                            Logger.AddEntry("ExceptionThreshold reached ({0}), stopping flushing thread.", ExceptionThreshold.ToString());
                            FireEvent(ExceptionThresholdReached, new JournalExceptionThresholdReachedEventArgs { Journal = this });
                        }
                    }
                }
            });
            
        }
    }
}
