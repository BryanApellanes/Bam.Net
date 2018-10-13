using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Bam.Net;
using Bam.Net.Logging;
using NCuid;
using Bam.Net.CoreServices;

namespace Bam.Net.Data.Repositories
{
    public partial class Meta
    {
        IObjectPersister _objectPersister;
        object _objectPersisterLock = new object();
        /// <summary>
        /// Gets or sets the object reader writer.
        /// </summary>
        /// <value>
        /// The object reader writer.
        /// </value>
        public IObjectPersister ObjectPersister
        {
            get
            {
                return _objectPersisterLock.DoubleCheckLock(ref _objectPersister, () => ServiceRegistry.Default.Get<IObjectPersister>());
            }
            set
            {
                _objectPersister = value;
            }
        }

        protected internal ulong GetNextId(Type type, IObjectPersister objectReaderWriter = null)
        {
            objectReaderWriter = objectReaderWriter ?? this.ObjectPersister;
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(objectReaderWriter.RootDirectory, type.Name));
            FileInfo metaFile = new FileInfo(Path.Combine(dir.FullName, "meta.id"));
            if (metaFile.Exists)
            {
                string idString = metaFile.FullName.SafeReadFile();
                idString = string.IsNullOrEmpty(idString) ? "0" : idString;
                ulong result = 0;
                if (ulong.TryParse(idString, out ulong parsed))
                {
                    result = ++parsed;
                }
                else
                {
                    ++result;
                }
                metaFile.FullName.SafeWriteFile(result.ToString());
                return result;
            }
            else
            {
                "1".SafeWriteToFile(metaFile.FullName);
                return 1;
            }
        }
    }
}
