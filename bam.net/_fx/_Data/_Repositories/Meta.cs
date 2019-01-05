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
                return _objectPersisterLock.DoubleCheckLock(ref _objectPersister, () => new ObjectPersister(Path.Combine(DefaultDataDirectoryProvider.Current.AppDataDirectory, nameof(ObjectPersister))));
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
            IpcMessage msg = IpcMessage.Get("meta.id", typeof(MetaId), dir.FullName);
            MetaId metaId = msg.Read<MetaId>();
            if (metaId == null)
            {
                metaId = new MetaId { Value = 0 };
                msg.Write(metaId);
            }

            ulong retrievedId = ++metaId.Value;
            msg.Write(new MetaId { Value = retrievedId });
            return retrievedId;
        }
    }
}
