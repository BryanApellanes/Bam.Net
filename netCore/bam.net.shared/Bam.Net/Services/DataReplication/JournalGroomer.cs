using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// Periodically ensures that there are no more than a specified number of
    /// journal entries for any single property.
    /// </summary>
    public class JournalGroomer: Loggable
    {
        public JournalGroomer(Journal journal, int groomIntervalMilliseconds, ILogger logger = null)
        {
            MaxEntries = 100;
            Journal = journal;
            Logger = logger ?? Log.Default;
            Subscribe(Logger);
            IntervalMilliseconds = groomIntervalMilliseconds;
            Timer = new Timer(Groom, null, groomIntervalMilliseconds, groomIntervalMilliseconds);
        }

        protected Timer Timer { get; }
        public int IntervalMilliseconds { get; set; }
        public int MaxEntries { get; set; }
        public Journal Journal { get; }
        public ILogger Logger { get; set; }

        public void Groom()
        {
            Groom(null);
        }

        public void Pause()
        {
            Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Resumes the groomer.
        /// </summary>
        /// <param name="doGroom">if set to <c>true</c> begin grooming immediately otherwise wait IntervalMilliseconds before grooming.</param>
        public void Resume(bool doGroom = true)
        {
            int dueTime = IntervalMilliseconds;
            if (doGroom)
            {
                dueTime = 0;
            }
            Timer.Change(dueTime, IntervalMilliseconds);
        }

        public event EventHandler GroomingStarted;
        public event EventHandler GroomingException;
        public event EventHandler GroomingFinished;

        protected void Groom(object state)
        {
            Logger.AddEntry("Journal.Groom started {0}", new Instant().ToString());
            try
            {
                FireEvent(GroomingStarted);
                Queue<DirectoryInfo> directoryInfos = new Queue<DirectoryInfo>();
                directoryInfos.Enqueue(Journal.JournalDirectory);
                while (directoryInfos.Count > 0)
                {
                    DirectoryInfo directoryInfo = directoryInfos.Dequeue();
                    if (directoryInfo != null && directoryInfo.Exists)
                    {
                        List<FileInfo> fileInfos = directoryInfo.GetFiles().ToList();
                        fileInfos.Sort((x, y) => y.Name.CompareTo(x.Name));
                        if (fileInfos.Count > MaxEntries)
                        {
                            for (int i = MaxEntries; i < fileInfos.Count; i++)
                            {
                                FileInfo fileInfo = fileInfos[i];
                                try
                                {
                                    fileInfo.Delete();
                                    Logger.AddEntry("Groomer deleted file ({0}).", fileInfo.FullName);
                                }
                                catch (Exception ex)
                                {
                                    Logger.AddEntry("Error deleting file ({0}) while grooming journal entries. {1}", ex, fileInfo?.FullName ?? "null", ex.Message);
                                }
                            }
                        }
                        foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
                        {
                            directoryInfos.Enqueue(directory);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception occurred while grooming: {0}", ex, ex.Message);
                FireEvent(GroomingException);
            }
            Logger.AddEntry("Journal.Groom finished {0}", new Instant().ToString());
            FireEvent(GroomingFinished);
        }
    }
}
