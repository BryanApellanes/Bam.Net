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
        public FileSequenceProvider(SequenceFile sequenceFile, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            File = sequenceFile.File;
            if (sequenceFile.File.Exists)
            {
                string content = sequenceFile.File.ReadAllText();
                if (ulong.TryParse(content, out ulong result))
                {
                    _current = result;
                    Logger.AddEntry("Sequence value found in file: {0}", result.ToString());
                }
            }
            AppDomain.CurrentDomain.DomainUnload += (o, a) => _current.ToString().SafeWriteToFile(File.FullName, true);
        }

        public ILogger Logger { get; set; }
        public FileInfo File { get; set; }

        public ulong Next()
        {
            ++_current;
            Task.Run(() => _current.ToString().SafeWriteToFile(File.FullName, true, (o) => o.ClearFileAccessLocks()));
            return _current;
        }
    }
}
