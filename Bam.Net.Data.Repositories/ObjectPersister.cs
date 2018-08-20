/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Threading;
using Bam.Net;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// Class used to write objects to disk.  Will 
	/// write two representations of the object, one
	/// as an IpcMessage <see cref="Bam.Net.IpcMessage"/>
	/// and another "searchable" version of all
	/// the properties in crawlable files.  This should
    /// not be used for any IO intensive applications
    /// as it needs more testing.
	/// </summary>
	[Serializable]
	public class ObjectPersister: Loggable, IObjectPersister
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPersister"/> class.
        /// </summary>
        /// <param name="rootDirectory">The root directory.</param>
        public ObjectPersister(string rootDirectory)
			: base()
		{
			RootDirectory = rootDirectory;
            ObjectReader = new ObjectReader(rootDirectory);
            BackgroundThreadQueue = new BackgroundThreadQueue<Meta> { Process = Write };
		}

		static IObjectPersister _objectReaderWriter;
		static object _readerWriterLock = new object();
        /// <summary>
        /// Gets or sets the default.
        /// </summary>
        /// <value>
        /// The default.
        /// </value>
        public static IObjectPersister Default
		{
			get
			{
                return _readerWriterLock.DoubleCheckLock(ref _objectReaderWriter, () => new ObjectPersister(Path.Combine(DefaultDataDirectoryProvider.Current.AppDataDirectory, "ObjectRepositoryData")));
			}
			set
			{
				_objectReaderWriter = value;
			}
		}

        public IObjectReader ObjectReader { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the maximum retries.
        /// </summary>
        /// <value>
        /// The maximum retries.
        /// </value>
        public int MaxRetries { get; set; }

        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Read<T>(long id)
		{
            return ObjectReader.Read<T>(id);
		}


        /// <summary>
        /// Reads the specified identifier.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public T Read<T>(ulong id)
        {
            return ObjectReader.Read<T>(id);
        }

        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object Read(Type type, long id)
		{
            return ObjectReader.Read(type, id);
		}

        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public object Read(Type type, ulong id)
        {
            return ObjectReader.Read(type, id);
        }

        /// <summary>
        /// Reads the specified UUID.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uuid">The UUID.</param>
        /// <returns></returns>
        public T Read<T>(string uuid)
		{
            return ObjectReader.Read<T>(uuid);
		}

        /// <summary>
        /// Reads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="uuid">The UUID.</param>
        /// <returns></returns>
        public object Read(Type type, string uuid)
		{
            return ObjectReader.Read(type, uuid);
		}

        /// <summary>
        /// Reads the instance of T by hash.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public virtual T ReadByHash<T>(string hash)
		{
            return ObjectReader.ReadByHash<T>(hash);
		}

        /// <summary>
        /// Reads the by hash.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public virtual object ReadByHash(Type type, string hash)
		{
            return ObjectReader.ReadByHash(type, hash);
        }

        /// <summary>
        /// Gets or sets the background thread queue.
        /// </summary>
        /// <value>
        /// The background thread queue.
        /// </value>
        public BackgroundThreadQueue<Meta> BackgroundThreadQueue
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the write queue count.
        /// </summary>
        /// <value>
        /// The write queue count.
        /// </value>
        public int WriteQueueCount
		{
			get
			{
                return BackgroundThreadQueue.WriteQueueCount;
			}
		}

		Queue<object> _writeQueue = new Queue<object>();
		object _writeQueueLock = new object();
        /// <summary>
        /// Enqueues the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        public void Enqueue(Type type, object data)
		{
            BackgroundThreadQueue.Enqueue(new Meta(data, this) { Type = type });
		}

        /// <summary>
        /// Occurs when [write object failed].
        /// </summary>
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "IpcMessage Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler WriteObjectFailed;

        /// <summary>
        /// Writes the specified meta.
        /// </summary>
        /// <param name="meta">The meta.</param>
        public virtual void Write(Meta meta)
        {
            object data = meta.Data;
            Type type = meta.Type;
            List<Task> waitFor = new List<Task>();
            if (meta.IsSerializable)
            {
                IpcMessage idMessage = GetIdMessage(data, type);
                IpcMessage uuidMessage = GetUuidMessage(data, type);
                SubscribeToIpcMessageEvents(idMessage);
                SubscribeToIpcMessageEvents(uuidMessage);
                Task idWrite = Task.Run(() => TryWrite(idMessage, data, 0, Logger));
                Task uuidWrite = Task.Run(() => TryWrite(uuidMessage, data, 0, Logger));

                waitFor.Add(idWrite);
                waitFor.Add(uuidWrite);
            }

            waitFor.Add(Task.Run(() => TryWriteObjectProperties(type, data)));
            waitFor.Add(Task.Run(() => WriteHash(meta)));

            Task.WaitAll(waitFor.ToArray());
        }

        /// <summary>
        /// Writes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        public virtual void Write(Type type, object data)
        {
            Write(GetMeta(data, type));           
        }

        /// <summary>
        /// Tries the write.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <param name="retriedCount">The retried count.</param>
        /// <param name="logger">The logger.</param>
        private void TryWrite(IpcMessage message, object data, int retriedCount = 0, ILogger logger = null)
        {
            try
            {
                message.Write(data);
            }
            catch (Exception ex)
            {
                if (retriedCount < MaxRetries)
                {
                    TryWrite(message, data, ++retriedCount, logger);
                }
                else
                {
                    Message = ex.Message;
                    (logger ?? Log.Default).AddEntry("Failed to write IpcMessage: {0}", ex, ex.Message);
                    FireEvent(WriteObjectFailed, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Writes the hash.
        /// </summary>
        /// <param name="meta">The meta.</param>
        protected void WriteHash(Meta meta)
		{
			DirectoryInfo hashDir = GetHashDir(meta.Type);			
			FileInfo uuidHash = new FileInfo(Path.Combine(hashDir.FullName, meta.UuidHash));
			FileInfo idHash = new FileInfo(Path.Combine(hashDir.FullName, meta.IdHash));
			if(!uuidHash.Exists)
			{
				meta.UuidHash.SafeWriteToFile(uuidHash.FullName, true, o => o.ClearFileAccessLocks());
			}
			if (!idHash.Exists)
			{
				meta.IdHash.SafeWriteToFile(idHash.FullName, true, o => o.ClearFileAccessLocks());
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
			if(uuidHash.Exists)
			{
				uuidHash.Delete();
			}
			FileInfo idHash = new FileInfo(Path.Combine(hashDir.FullName, meta.IdHash));
			if(idHash.Exists)
			{
				idHash.Delete();
			}
		}

		private DirectoryInfo GetHashDir(Type type)
		{
			DirectoryInfo hashDir = new DirectoryInfo(Path.Combine(RootDirectory, "Hashes", type.Name));
			if (!hashDir.Exists)
			{
				hashDir.Create();
			}
			return hashDir;
		}

        /// <summary>
        /// Loads the hashes.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected HashSet<string> LoadHashes(Type type)
		{
			DirectoryInfo hashDir = GetHashDir(type);
			HashSet<string> results = new HashSet<string>();
			foreach (FileInfo file in hashDir.GetFiles())
			{
				results.Add(file.Name);
			}
			return results;
		}

        /// <summary>
        /// Gets the meta.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        protected Meta GetMeta(object data, Type type = null)
		{
            if (data is Meta meta)
            {
                data = meta.Data;
                meta.ObjectPersister = this;
            }
            else
            {
                SetId(data);
                SetUuid(data);
                meta = new Meta(data, this);
            }
            if(type != typeof(object))
            {
                meta.Type = type;
            }
			return meta;
		}

        /// <summary>
        /// Deletes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public virtual bool Delete(object data, Type type = null)
		{
			try
			{
				if (data == null)
					return false;
				type = type ?? data.GetType();
				string idHash = Meta.GetIdHash(data, type);
				IpcMessage.Delete(idHash, type, RootDirectory);
				string uuidHash = GetUuidHash(data, type);
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

        /// <summary>
        /// Gets or sets the root directory.
        /// </summary>
        /// <value>
        /// The root directory.
        /// </value>
        public string RootDirectory { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Occurs when [write object properties failed].
        /// </summary>
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Properties Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler WriteObjectPropertiesFailed;

        /// <summary>
        /// Occurs when [delete failed].
        /// </summary>
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Properties Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler DeleteFailed;

        /// <summary>
        /// Called when [delete failed].
        /// </summary>
        protected void OnDeleteFailed()
		{
            DeleteFailed?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Tries the write object properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        /// <param name="retryCount">The retry count.</param>
        /// <returns></returns>
        protected bool TryWriteObjectProperties(Type type, object value, int retryCount = 0)
		{
			try
			{
				WriteObjectProperties(type, value);
				return true;
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				if(!string.IsNullOrEmpty(ex.StackTrace))
				{
					Message += "\r\nStackTrace: " + ex.StackTrace; 
				}
				if (retryCount < MaxRetries)
				{
					TryWriteObjectProperties(type, value, ++retryCount);
				}
				else
				{
					FireEvent(WriteObjectPropertiesFailed, EventArgs.Empty);
				}
			}
			
			return false;
		}

        /// <summary>
        /// Writes the object properties.
        /// </summary>
        /// <param name="value">The value.</param>
        protected void WriteObjectProperties(object value)
        {
            WriteObjectProperties(value.GetType(), value);
        }

        /// <summary>
        /// Writes the object properties.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        protected void WriteObjectProperties(Type type, object value)
        {
            PropertyInfo[] properties = type
                .GetProperties()
                .Where(prop => prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(bool) ||
                    prop.PropertyType == typeof(bool?) ||
                    prop.PropertyType == typeof(int) ||
                    prop.PropertyType == typeof(int?) ||
                    prop.PropertyType == typeof(long) ||
                    prop.PropertyType == typeof(long?) ||
                    prop.PropertyType == typeof(ulong) ||
                    prop.PropertyType == typeof(ulong?) ||                    
                    prop.PropertyType == typeof(decimal) ||
                    prop.PropertyType == typeof(decimal?) ||
                    prop.PropertyType == typeof(byte[]) ||
                    prop.PropertyType == typeof(DateTime) ||
                    prop.PropertyType == typeof(DateTime?)).ToArray();
            foreach (PropertyInfo prop in properties)
            {
                WriteProperty(type, prop, value);
            }
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
        /// Queries the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public object[] Query(Type type, Func<object, bool> predicate)
		{
            return NonSerializableQuery(type, predicate);
		}

        /// <summary>
        /// Queries the specified predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T[] Query<T>(Func<T, bool> predicate)
		{
            return NonSerializableQuery<T>(predicate);
		}

        /// <summary>
        /// Queries the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public T[] QueryProperty<T>(string propertyName, object value)
		{
			return QueryProperty<T>(propertyName, (o) => o != null && (o.Equals(value) || o == value));
		}

        /// <summary>
        /// Queries the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate)
		{
			PropertyInfo prop = typeof(T).GetProperty(propertyName);
			return QueryProperty<T>(prop, predicate);
		}

        /// <summary>
        /// Queries the property.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="prop">The property.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public T[] QueryProperty<T>(PropertyInfo prop, Func<object, bool> predicate)
		{
			HashSet<string> resultNames = new HashSet<string>();
			List<Meta<T>> results = new List<Meta<T>>();
			DirectoryInfo propDir = GetPropertyDirectory(typeof(T), prop);
			foreach (DirectoryInfo dir in propDir.GetDirectories())
			{
				// each dir is named as a hash
				string hash = dir.Name;
				object valueToCheck = ReadProperty(typeof(T), prop, hash);
				if (predicate(valueToCheck))
				{
					if (!resultNames.Contains(hash))
					{
						resultNames.Add(hash);
						T instance = ReadByHash<T>(hash);
						Meta<T> meta = new Meta<T>(instance, this);
						if (!results.Contains(meta))
						{
							results.Add(meta);
						}
					}
				}
			}
			return results.Select<Meta<T>, T>(m => m.TypedData).ToArray();
		}

		public T ReadProperty<T>(PropertyInfo prop, long id)
		{
            return ObjectReader.ReadProperty<T>(prop, id);
		}

        public T ReadProperty<T>(PropertyInfo prop, ulong id)
        {
            return ObjectReader.ReadProperty<T>(prop, id);
        }

        public T ReadProperty<T>(PropertyInfo prop, string uuid)
		{
            return ObjectReader.ReadProperty<T>(prop, uuid);
		}

        protected object ReadProperty(Type type, PropertyInfo prop, string hash)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(type, prop);
			int version = Meta.GetHighestVersionNumber(propRoot, prop, hash);
			return ReadPropertyVersion(prop, hash, propRoot, version);
		}

		public T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version)
		{
            return ObjectReader.ReadPropertyVersion<T>(prop, hash, version);
		}

		private static object ReadPropertyVersion(PropertyInfo prop, string hash, DirectoryInfo propRoot, int version)
		{
			string readFromRootDir = Path.Combine(propRoot.FullName, hash);
			IpcMessage msg = IpcMessage.Get(version.ToString(), prop.PropertyType, readFromRootDir);
			return msg.Read<object>();
		}

		protected void DeleteProperty(Type type, PropertyInfo prop, string hash)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(type, prop);
			string deleteDir = Path.Combine(propRoot.FullName, hash);
			DirectoryInfo dir = new DirectoryInfo(deleteDir);
			if (dir.Exists)
			{
				dir.Delete(true);
			}
		}

        public void WriteProperty(PropertyInfo prop, object value)
        {
            WriteProperty(prop.DeclaringType, prop, value);
        }

        public void WriteProperty(Type type, PropertyInfo prop, object value)
		{
            if(prop.DeclaringType != value.GetType())
            {
                prop = value.GetType().GetProperty(prop.Name);
            }
			object propValue = prop.GetValue(value);
			if (propValue != null)
			{
				string idHash = Meta.GetIdHash(value, type);
				WriteProperty(type, prop, propValue, idHash);
				string uuidHash = GetUuidHash(value, type);
				WriteProperty(type, prop, propValue, uuidHash);
			}
		}

		public DirectoryInfo GetTypeDirectory(Type type)
		{
			DirectoryInfo dir = new DirectoryInfo(Path.Combine(RootDirectory, type.Name));
			if(!dir.Exists)
			{
				dir.Create();
			}
			return dir;
		}

        public DirectoryInfo GetPropertyDirectory(PropertyInfo prop)
        {
            return GetPropertyDirectory(prop.DeclaringType, prop);
        }

        public DirectoryInfo GetPropertyDirectory(Type type, PropertyInfo prop)
		{
			DirectoryInfo typeDirectory = GetTypeDirectory(type);
			DirectoryInfo dir = new DirectoryInfo(Path.Combine(typeDirectory.FullName, prop.Name));
			if (!dir.Exists)
			{
				dir.Create();
			}
			return dir;
		}

        protected virtual string GetUuidHash(object value, Type type)
		{
			string result = Meta.GetUuidHash("", Type.Missing.GetType());
			if (value != null)
			{
				result = Meta.GetUuidHash("", type);
				PropertyInfo uuidProp = value.GetType().GetProperty("Uuid");
				if (uuidProp != null)
				{
					string uuid = (string)uuidProp.GetValue(value);
					if (!string.IsNullOrEmpty(uuid))
					{
						result = Meta.GetUuidHash(uuid, type);
					}
				}
			}
			return result;
		}

		private void SubscribeToIpcMessageEvents(IpcMessage msg)
		{
			Subscribers.Each(logger =>
			{
				msg.Subscribe(logger);
			});
		}

        private void WriteProperty(Type type, PropertyInfo prop, object propValue, string hash)
        {
            DirectoryInfo propRoot = GetPropertyDirectory(type, prop);
            int version = Meta.GetNextVersionNumber(propRoot, prop, hash);
            string writeToRootDir = Path.Combine(propRoot.FullName, hash);
            IpcMessage msg = IpcMessage.Get(version.ToString(), prop.PropertyType, writeToRootDir);
            msg.Write(propValue);
        }

		private IpcMessage GetIdMessage(object data, Type type = null)
		{
			string idHash = Meta.GetIdHash(data, type);
			return GetHashMessage(data, idHash, type);
		}
		private IpcMessage GetUuidMessage(object data, Type type = null)
		{
			string uuidHash = GetUuidHash(data, type);
			return GetHashMessage(data, uuidHash, type);
		}

		private IpcMessage GetHashMessage(object data, string hash, Type type = null)
		{
            type = type ?? data.GetType();
            if (!IpcMessage.Exists(hash, type, RootDirectory, out IpcMessage idMessage))
			{
				idMessage = IpcMessage.Create(hash, type, RootDirectory);
			}
			return idMessage;
		}

		private void SetId(object value)
		{
			Meta meta = new Meta();
			meta.SetId(value, this);
		}

		private void SetUuid(object value)
		{
			Meta meta = new Meta();			
			Meta.SetUuid(value);
		}
		
		private object[] SerializableQuery(Type type, Func<object, bool> predicate)
		{
			List<object> results = new List<object>();
			HashSet<Meta> metaList = new HashSet<Meta>();
			DirectoryInfo typeDir = GetTypeDirectory(type);
			foreach (FileInfo file in typeDir.GetFiles().Where(f => f.HasNoExtension()))
			{
				object instance = file.FromBinaryFile(type);
				Meta meta = new Meta(instance, this);
				if (predicate(instance) && !metaList.Contains(meta))
				{
					metaList.Add(meta);
					results.Add(instance);
				}
			}
			return results.ToArray();
		}

		private T[] SerializableQuery<T>(Func<T, bool> predicate)
		{
			List<T> results = new List<T>();
			HashSet<Meta<T>> metaList = new HashSet<Meta<T>>();
			DirectoryInfo typeDir = GetTypeDirectory(typeof(T));
			foreach (FileInfo file in typeDir.GetFiles().Where(f => f.HasNoExtension()))
			{
				T instance = file.FromBinaryFile<T>();
				Meta<T> meta = new Meta<T>(instance, this);
				if (predicate(instance) && !metaList.Contains(meta))
				{
					metaList.Add(meta);
					results.Add(instance);
				}
			}
			return results.ToArray();
		}

		private T[] NonSerializableQuery<T>(Func<T, bool> predicate)
		{
			HashSet<string> hashes = LoadHashes(typeof(T));
			List<T> results = new List<T>();
			HashSet<Meta<T>> metaList = new HashSet<Meta<T>>();
			foreach (string hash in hashes)
			{
				T instance = ReadByHash<T>(hash);
				Meta<T> meta = new Meta<T>(instance, this);
				if (predicate(instance) && !metaList.Contains(meta))
				{
					metaList.Add(meta);
					results.Add(instance);
				}
			}

			return results.ToArray();
		}

		private object[] NonSerializableQuery(Type type, Func<object, bool> predicate)
		{
			HashSet<string> hashes = LoadHashes(type);
			List<object> results = new List<object>();
			HashSet<Meta> metaList = new HashSet<Meta>();
			foreach (string hash in hashes)
			{
				object instance = ReadByHash(type, hash);
                if(instance != null)
                {
                    Meta meta = new Meta(instance, this);
                    if (predicate(instance) && !metaList.Contains(meta))
                    {
                        metaList.Add(meta);
                        results.Add(instance);
                    }
                }
			}

			return results.ToArray();
		}
	}
}
