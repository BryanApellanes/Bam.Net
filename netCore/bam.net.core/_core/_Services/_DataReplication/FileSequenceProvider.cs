﻿using Bam.Net.Data.Repositories;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// An implementation of ISequenceProvider that tracks the latest sequence value in a
    /// file.
    /// </summary>
    /// <seealso cref="Bam.Net.Services.DataReplication.ISequenceProvider" />
    public class FileSequenceProvider : ISequenceProvider
    {
        ulong _current;
        public FileSequenceProvider(FileInfo file, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            SystemPaths paths = SystemPaths.Get(DefaultDataDirectoryProvider.Current);

            File = new FileInfo(Path.Combine(paths.Data.AppFiles, $"{nameof(FileSequenceProvider)}.txt"));
            if (ulong.TryParse(file.FullName.SafeReadFile(), out ulong value))
            {
                _current = value;
            }
        }

        public ILogger Logger { get; set; }
        public FileInfo File { get; set; }

        public ulong Next()
        {
            ++_current;
            Task.Run(() => _current.ToString().SafeWriteToFile(File.FullName, true));
            return _current;
        }
    }
}
