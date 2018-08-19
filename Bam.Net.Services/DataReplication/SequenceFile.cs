using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class SequenceFile
    {
        public static string Name
        {
            get
            {
                return $"{nameof(DataReplicationJournal)}_{nameof(SequenceFile)}"; ;
            }
        }

        protected SequenceFile() { }

        public SequenceFile(SystemPaths paths)
        {
            IpcMessage = IpcMessage.Create(Name, typeof(ulong), Path.Combine(paths.Data.AppData, $"{nameof(DataReplicationJournal)}_Sequence"), true);
        }

        public static implicit operator IpcMessage(SequenceFile file)
        {
            return file.IpcMessage;
        }

        public static implicit operator SequenceFile(IpcMessage message)
        {
            return new SequenceFile { IpcMessage = message };
        }

        public IpcMessage IpcMessage { get; set; }
    }
}
