using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class ObjectReader : Loggable, IObjectReader
    {
        public ObjectReader(string rootDirectory)
        {
            RootDirectory = rootDirectory;
            FileSystemPersisterDirectoryProvider = new ObjectPersisterDirectoryProvider(rootDirectory);
        }

        public string RootDirectory { get; set; }

        protected IObjectPersisterDirectoryProvider FileSystemPersisterDirectoryProvider { get; }

        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Read<T>(long id)
        {
            string idHash = Meta.GetIdHash(id, typeof(T));
            return ReadByHash<T>(idHash);
        }

        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Read<T>(ulong id)
        {
            string idHash = Meta.GetIdHash(id, typeof(T));
            return ReadByHash<T>(idHash);
        }

        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object Read(Type type, long id)
        {
            string idHash = Meta.GetIdHash(id, type);
            return ReadByHash(type, idHash);
        }
        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object Read(Type type, ulong id)
        {
            string idHash = Meta.GetIdHash(id, type);
            return ReadByHash(type, idHash);
        }

        /// <summary>
        /// Reads the specified UUID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uuid">The UUID.</param>
        /// <returns></returns>
        public T Read<T>(string uuid)
        {
            string uuidHash = Meta.GetUuidHash(uuid, typeof(T));
            return ReadByHash<T>(uuidHash);
        }

        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="uuid">The UUID.</param>
        /// <returns></returns>
        public object Read(Type type, string uuid)
        {
            string uuidHash = Meta.GetUuidHash(uuid, type);
            return ReadByHash(type, uuidHash);
        }

        /// <summary>
        /// Reads the instance of T by hash.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public virtual T ReadByHash<T>(string hash)
        {
            return (T)ReadByHash(typeof(T), hash);
        }

        /// <summary>
        /// Reads the by hash.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public virtual object ReadByHash(Type type, string hash)
        {
            if (type.HasCustomAttributeOfType<SerializableAttribute>())
            {
                return ReadSerializableTypeByHash(type, hash);
            }
            else
            {
                return ReadPropertiesByHash(type, hash);
            }
        }

        public T ReadProperty<T>(PropertyInfo prop, long id)
        {
            return (T)ReadProperty(prop, id);
        }

        public T ReadProperty<T>(PropertyInfo prop, ulong id)
        {
            return (T)ReadProperty(prop, id);
        }

        public T ReadProperty<T>(PropertyInfo prop, string uuid)
        {
            string hash = Meta.GetUuidHash(uuid, typeof(T));
            return ReadPropertyByHash<T>(prop, hash);
        }
        
        public T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version)
        {
            DirectoryInfo propRoot = GetPropertyDirectory(typeof(T), prop);
            return (T)ReadPropertyVersion(prop, hash, propRoot, version);
        }

        protected T ReadPropertyByHash<T>(PropertyInfo prop, string hash)
        {
            return (T)ReadProperty(prop, hash);
        }

        protected object ReadProperty(PropertyInfo prop, ulong id)
        {
            string hash = Meta.GetIdHash(id, prop.DeclaringType);
            return ReadProperty(prop, hash);
        }

        protected object ReadProperty(PropertyInfo prop, long id)
        {
            string hash = Meta.GetIdHash(id, prop.DeclaringType);
            return ReadProperty(prop, hash);
        }

        protected object ReadProperty(PropertyInfo prop, string hash)
        {
            return ReadProperty(prop.DeclaringType, prop, hash);
        }

        /// <summary>
        /// Reads the properties by hash.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        protected virtual object ReadPropertiesByHash(Type type, string hash)
        {
            object result = type.Construct();
            foreach (PropertyInfo prop in type.GetProperties().Where(p => p.CanWrite))
            {
                prop.SetValue(result, ReadProperty(type, prop, hash));
            }

            return result;
        }

        public DirectoryInfo GetTypeDirectory(Type type)
        {
            return FileSystemPersisterDirectoryProvider.GetTypeDirectory(type);
        }

        public DirectoryInfo GetPropertyDirectory(PropertyInfo prop)
        {
            return FileSystemPersisterDirectoryProvider.GetPropertyDirectory(prop);
        }

        public DirectoryInfo GetPropertyDirectory(Type type, PropertyInfo prop)
        {
            return FileSystemPersisterDirectoryProvider.GetPropertyDirectory(type, prop);
        }

        /// <summary>
        /// Reads the serializable type by hash.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        protected virtual object ReadSerializableTypeByHash(Type type, string hash)
        {
            IpcMessage msg = IpcMessage.Get(hash, type, RootDirectory);
            SubscribeToIpcMessageEvents(msg);
            return msg.Read<object>();
        }

        protected object ReadProperty(Type type, PropertyInfo prop, string hash)
        {
            DirectoryInfo propRoot = GetPropertyDirectory(type, prop);
            int version = Meta.GetHighestVersionNumber(propRoot, prop, hash);
            return ReadPropertyVersion(prop, hash, propRoot, version);
        }

        private void SubscribeToIpcMessageEvents(IpcMessage msg)
        {
            Subscribers.Each(logger => msg.Subscribe(logger));
        }

        public static object ReadPropertyVersion(PropertyInfo prop, string hash, DirectoryInfo propRoot, int version)
        {
            string readFromRootDir = Path.Combine(propRoot.FullName, hash);
            IpcMessage msg = IpcMessage.Get(version.ToString(), prop.PropertyType, readFromRootDir);
            return msg.Read<object>();
        }
    }
}
