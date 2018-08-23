﻿using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class JournalGroomer
    {
        public JournalGroomer(Journal journal, int groomIntervalMilliseconds, ILogger logger = null)
        {
            MaxEntries = 100;
            Journal = journal;
            Logger = logger ?? Log.Default;
            Timer = new Timer(Groom, null, groomIntervalMilliseconds, groomIntervalMilliseconds);
        }

        protected Timer Timer { get; }
        public int MaxEntries { get; set; }
        public Journal Journal { get; }
        public ILogger Logger { get; set; }

        public void Groom()
        {
            Groom(null);
        }

        protected void Groom(object state)
        {
            Logger.AddEntry("Journal.Groom started {0}", new Instant().ToString());
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
            Logger.AddEntry("Journal.Groom finished {0}", new Instant().ToString());
        }
    }
}
