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
        Queue<JournalEntry> _dataReplicationJournalEntries;
        bool _keepFlushing;

        public Journal(ISequenceProvider sequenceProvider, IJournalEntryValueFlusher flusher, IJournalEntryValueLoader loader, ITypeConverter typeConverter, ILogger logger = null) 
            : this(SystemPaths.Get(DefaultDataDirectoryProvider.Current), new JournalTypeMap(SystemPaths.Get(DefaultDataDirectoryProvider.Current)), sequenceProvider, flusher, loader, typeConverter, logger)
        { }

        public Journal(SystemPaths paths, JournalTypeMap typeMap, ISequenceProvider sequenceProvider, IJournalEntryValueFlusher flusher, IJournalEntryValueLoader loader, ITypeConverter typeConverter = null, ILogger logger = null)
        {
            _dataReplicationJournalEntries = new Queue<JournalEntry>();
            _keepFlushing = true;
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
            foreach(JournalEntry entry in JournalEntry.LoadInstanceEntries<T>(id, JournalDirectory, TypeMap, Loader))
            {
                string propertyName = TypeMap.GetPropertyShortName(entry.PropertyId);
                PropertyInfo prop = typeof(T).GetProperty(propertyName);
                try
                {
                    object value = TypeConverter.ChangeType(entry.Value, prop.PropertyType);
                    toLoad.Property(propertyName, value);
                }
                catch (Exception ex)
                {
                    Log.Default.AddEntry("Failed to set property ({0}) on instance ({1}) of type ({2})", ex, prop.Name, id.ToString(), typeof(T).FullName);
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
            Task.Run(() => TypeMap.AddMapping(data));
            JournalEntry[] journalEntries = JournalEntry.FromInstance(data).ToArray();
            return EnqueueEntriesForWrite(journalEntries);
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

        protected internal IEnumerable<JournalEntry> EnqueueEntriesForWrite(params JournalEntry[] journalEntries)
        {
            foreach(JournalEntry journalEntry in journalEntries)
            {
                journalEntry.Seq = SequenceProvider.Next();
                _dataReplicationJournalEntries.Enqueue(journalEntry);
                yield return journalEntry;
            }
            FireEvent(EntriesEnqueued, new JournalEntriesEnqueuedEventArgs { JournalEntries = journalEntries });
        }

        int _exceptionCount;
        private void FlushQueue()
        {
            _fireQueueEmpty = true;
            Task.Run(() =>
            {
                while (_keepFlushing )
                {
                    try
                    {
                        if (_dataReplicationJournalEntries.Count > 0)
                        {
                            _fireQueueEmpty = true;
                            while(_dataReplicationJournalEntries.Count > 0)
                            {
                                JournalEntry journalEntry = _dataReplicationJournalEntries.Dequeue();
                                if(journalEntry != null)
                                {
                                    FileInfo propertyFile = Flusher.Flush(this, journalEntry);
                                    FireEvent(EntryFlushed, new JournalEntryWrittenEventArgs { JournalEntry = journalEntry, File = propertyFile });                                    
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
                            Thread.Sleep(300);
                        }
                    }
                    catch (Exception ex)
                    {
                        _exceptionCount++;
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
