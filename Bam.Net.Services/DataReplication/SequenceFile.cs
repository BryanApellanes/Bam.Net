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
        protected SequenceFile() { }

        public SequenceFile(SystemPaths paths)
        {
            File = new FileInfo(Path.Combine(paths.Data.AppData, $"{nameof(DataReplicationJournal)}_{nameof(SequenceFile)}"));
        }

        public static implicit operator FileInfo(SequenceFile file)
        {
            return file.File;
        }

        public static implicit operator SequenceFile(FileInfo file)
        {
            return new SequenceFile { File = file };
        }

        public FileInfo File { get; set; }
    }
}
