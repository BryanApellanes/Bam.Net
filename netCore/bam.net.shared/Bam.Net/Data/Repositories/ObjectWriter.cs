using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class ObjectWriter : ObjectPersisterDirectoryProvider, IObjectWriter
    {
        public ObjectWriter(string rootDirectory) : base(rootDirectory) { }

        public event EventHandler WriteObjectFailed;
        public event EventHandler WriteObjectPropertiesFailed;
        public event EventHandler DeleteFailed;

        public bool Delete(object data, Type type = null)
        {
            try
            {
                if (data == null)
                    return false;
                type = type ?? data.GetType();
                string idHash = Meta.GetIdHash(data, type);
                IpcMessage.Delete(idHash, type, RootDirectory);
                string uuidHash = Meta.GetUuidHash(data, type);
                IpcMessage.Delete(uuidHash, type, RootDirectory);
                DeleteObjectProperties(type, data);
                DeleteHash(data, type);
                return true;
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                OnDeleteFailed();
                return false;
            }
        }

        public void Enqueue(Type type, object data)
        {
            throw new NotImplementedException();
        }

        public void Write(Type type, object data)
        {
            throw new NotImplementedException();
        }

        public void WriteProperty(PropertyInfo prop, object value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the object properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        protected void DeleteObjectProperties(Type type, object value)
        {
            type = type ?? value.GetType();
            string idHash = Meta.GetIdHash(value, type);
            string uuidHash = GetUuidHash(value, type);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                DeleteProperty(type, prop, idHash);
                DeleteProperty(type, prop, uuidHash);
            }
        }

        /// <summary>
        /// Deletes the hash.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="type">The type.</param>
        protected void DeleteHash(object data, Type type = null)
        {
            Meta meta = GetMeta(data, type);
            DeleteHash(meta);
        }

        /// <summary>
        /// Deletes the hash.
        /// </summary>
        /// <param name="meta">The meta.</param>
        protected void DeleteHash(Meta meta)
        {
            DirectoryInfo hashDir = GetHashDir(meta.Type);
            FileInfo uuidHash = new FileInfo(Path.Combine(hashDir.FullName, meta.UuidHash));
            if (uuidHash.Exists)
            {
                uuidHash.Delete();
            }
            FileInfo idHash = new FileInfo(Path.Combine(hashDir.FullName, meta.IdHash));
            if (idHash.Exists)
            {
                idHash.Delete();
            }
        }
    }
}
