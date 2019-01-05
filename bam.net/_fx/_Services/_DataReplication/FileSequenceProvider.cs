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
            IpcMessage = sequenceFile.IpcMessage;
            _current = IpcMessage.Read<ulong>();
        }

        public ILogger Logger { get; set; }
        public IpcMessage IpcMessage { get; set; }

        public ulong Next()
        {
            ++_current;
            Task.Run(() => IpcMessage.Write(_current));
            return _current;
        }
    }
}
