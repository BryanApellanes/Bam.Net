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
	/// the properties in crawlable files
	/// </summary>
	[Serializable]
	public class ObjectReaderWriter: Loggable, IObjectReaderWriter
	{
		public ObjectReaderWriter(string rootDirectory)
			: base()
		{
			RootDirectory = rootDirectory;
            BackgroundThreadQueue = new BackgroundThreadQueue<object> { Process = Write };
		}
		static IObjectReaderWriter _objectReaderWriter;
		static object _readerWriterLock = new object();
		public static IObjectReaderWriter Default
		{
			get
			{
				return _readerWriterLock.DoubleCheckLock(ref _objectReaderWriter, () => new ObjectReaderWriter(".\\ObjectRepositoryData"));
			}
			set
			{
				_objectReaderWriter = value;
			}
		}
		public int MaxRetries { get; set; }

		public T Read<T>(long id)
		{
			string idHash = GetIdHash(id, typeof(T));
			return ReadByHash<T>(idHash);
		}
		
		public object Read(Type type, long id)
		{
			string idHash = GetIdHash(id, type);
			return ReadByHash(type, idHash);
		}

		public T Read<T>(string uuid)
		{
			string uuidHash = GetUuidHash(uuid, typeof(T));
			return ReadByHash<T>(uuidHash);
		}
		
		public object Read(Type type, string uuid)
		{
			string uuidHash = GetUuidHash(uuid, type);
			return ReadByHash(type, uuidHash);
		}

		public virtual T ReadByHash<T>(string hash)
		{
			return (T)ReadByHash(typeof(T), hash);
		}

		public virtual object ReadByHash(Type type, string hash)
		{
			if (type.HasCustomAttributeOfType<SerializableAttribute>())
			{
				return ReadSerializableTypeByHash(type, hash);
			}
			else
			{
				return ReadNonSerializableTypeByHash(type, hash);
			}
		}

		protected virtual object ReadNonSerializableTypeByHash(Type type, string hash)
		{
			object result = type.Construct();
			foreach (PropertyInfo prop in type.GetProperties())
			{
				prop.SetValue(result, ReadProperty(prop, hash));
			}

			return result;
		}

		protected virtual object ReadSerializableTypeByHash(Type type, string hash)
		{
			IpcMessage msg = IpcMessage.Get(hash, type, RootDirectory);
			SubscribeToIpcMessageEvents(msg);
			return msg.Read<object>();
		}

        public BackgroundThreadQueue<object> BackgroundThreadQueue
        {
            get;
            set;
        }
        
		public int WriteQueueCount
		{
			get
			{
                return BackgroundThreadQueue.WriteQueueCount;
			}
		}

		Queue<object> _writeQueue = new Queue<object>();
		object _writeQueueLock = new object();
		public void Enqueue(object data)
		{
            BackgroundThreadQueue.Enqueue(data);
		}
        
		[Verbosity(VerbosityLevel.Warning, MessageFormat = "IpcMessage Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler WriteObjectFailed;
		
		public virtual void Write(object data)
		{
			Write(data, 0);
		}

		private void Write(object data, int retryCount)
		{
			Meta meta = GetMeta(ref data);

			if (meta.IsSerializable)
			{
				IpcMessage idMessage = GetIdMessage(data);
				IpcMessage uuidMessage = GetUuidMessage(data);
				SubscribeToIpcMessageEvents(idMessage);
				SubscribeToIpcMessageEvents(uuidMessage);

				if (!idMessage.Write(data) || !uuidMessage.Write(data))
				{
					if (retryCount < MaxRetries)
					{
						Write(data, ++retryCount);
					}
					else
					{
						FireEvent(WriteObjectFailed, EventArgs.Empty);
					}
				}
				else
				{
					TryWriteObjectProperties(data);
				}
			}
			else
			{
				TryWriteObjectProperties(data);			
			}

			WriteHash(meta);
		}

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

		protected void DeleteHash(object data)
		{
			Meta meta = GetMeta(ref data);
			DeleteHash(meta);
		}

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

		protected Meta GetMeta(ref object data)
		{
			Meta meta = data as Meta;
			if (meta != null)
			{
				data = meta.Data;
			}
			else
			{
				SetId(data);
				SetUuid(data);
				meta = new Meta(data, this);
			}
			return meta;
		}

		public virtual bool Delete(object data)
		{
			try
			{

				if (data == null)
					return false;
				Type type = data.GetType();
				string idHash = GetIdHash(data);
				IpcMessage.Delete(idHash, type, RootDirectory);
				string uuidHash = GetUuidHash(data);
				IpcMessage.Delete(uuidHash, type, RootDirectory);
				DeleteObjectProperties(data);
				DeleteHash(data);
				return true;
			}
			catch (Exception ex)
			{
				Message = ex.Message;
				OnDeleteFailed();
				return false;
			}
		}

		public string RootDirectory { get; set; }

		public string Message { get; set; }

		[Verbosity(VerbosityLevel.Warning, MessageFormat = "Properties Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler WriteObjectPropertiesFailed;


		[Verbosity(VerbosityLevel.Warning, MessageFormat = "Properties Failed:: RootDirectory={RootDirectory}\r\nMessage={Message}")]
		public event EventHandler DeleteFailed;

		protected void OnDeleteFailed()
		{
			if (DeleteFailed != null)
			{
				DeleteFailed(this, EventArgs.Empty);
			}
		}

		protected bool TryWriteObjectProperties(object value, int retryCount = 0)
		{
			try
			{
				WriteObjectProperties(value);
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
					TryWriteObjectProperties(value, ++retryCount);
				}
				else
				{
					FireEvent(WriteObjectPropertiesFailed, EventArgs.Empty);
				}
			}
			
			return false;
		}
		
		protected void WriteObjectProperties(object value)
		{
			Type type = value.GetType();
			foreach(PropertyInfo prop in type.GetProperties())
			{
				WriteProperty(prop, value);
			}
		}

		protected void DeleteObjectProperties(object value)
		{
			Type type = value.GetType();
			string idHash = GetIdHash(value);
			string uuidHash = GetUuidHash(value);
			foreach (PropertyInfo prop in type.GetProperties())
			{
				DeleteProperty(prop, idHash);
				DeleteProperty(prop, uuidHash);
			}
		}

		public object[] Query(Type type, Func<object, bool> predicate)
		{
			if (type.HasCustomAttributeOfType<SerializableAttribute>())
			{
				return SerializableQuery(type, predicate);
			}
			else
			{
				return NonSerializableQuery(type, predicate);
			}
		}

		public T[] Query<T>(Func<T, bool> predicate)
		{
			if(typeof(T).HasCustomAttributeOfType<SerializableAttribute>())
			{
				return SerializableQuery<T>(predicate);
			}
			else
			{
				return NonSerializableQuery<T>(predicate);
			}
		}

		public T[] QueryProperty<T>(string propertyName, object value)
		{
			return QueryProperty<T>(propertyName, (o) => o.Equals(value) || o == value);
		}

		public T[] QueryProperty<T>(string propertyName, Func<object, bool> predicate)
		{
			PropertyInfo prop = typeof(T).GetProperty(propertyName);
			return QueryProperty<T>(prop, predicate);
		}

		public T[] QueryProperty<T>(PropertyInfo prop, Func<object, bool> predicate)
		{
			HashSet<string> resultNames = new HashSet<string>();
			List<Meta<T>> results = new List<Meta<T>>();
			DirectoryInfo propDir = GetPropertyDirectory(prop);
			foreach (DirectoryInfo dir in propDir.GetDirectories())
			{
				// each dir is named as a hash
				string hash = dir.Name;
				object valueToCheck = ReadProperty(prop, hash);
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

		public object[] QueryProperty(PropertyInfo prop, Func<object, bool> predicate)
		{
			Type type = prop.DeclaringType;
			HashSet<string> resultNames = new HashSet<string>();
			List<Meta> results = new List<Meta>();
			DirectoryInfo propDir = GetPropertyDirectory(prop);
			foreach (DirectoryInfo dir in propDir.GetDirectories())
			{
				// each dir is named as a hash
				string hash = dir.Name;
				object valueToCheck = ReadProperty(prop, hash);
				if (predicate(valueToCheck))
				{
					if (!resultNames.Contains(hash))
					{
						resultNames.Add(hash);
						object instance = ReadByHash(type, hash);
						Meta meta = new Meta(instance, this);
						if (!results.Contains(meta))
						{
							results.Add(meta);
						}
					}
				}
			}
			return results.Select<Meta, object>(m => m.Data).ToArray();
		}

		public T ReadProperty<T>(PropertyInfo prop, long id)
		{
			return (T)ReadProperty(prop, id);
		}

		protected object ReadProperty(PropertyInfo prop, long id)
		{
			string hash = GetIdHash(id, prop.DeclaringType);
			return ReadProperty(prop, hash);
		}

		public T ReadProperty<T>(PropertyInfo prop, string uuid)
		{
			string hash = GetUuidHash(uuid, typeof(T));
			return ReadPropertyByHash<T>(prop, hash);
		}

		protected T ReadPropertyByHash<T>(PropertyInfo prop, string hash)
		{
			return (T)ReadProperty(prop, hash);
		}

		protected object ReadProperty(PropertyInfo prop, string hash)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(prop);
			int version = GetHighestVersionNumber(propRoot, prop, hash);
			return ReadPropertyVersion(prop, hash, propRoot, version);
		}

		public T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(prop);
			return (T)ReadPropertyVersion(prop, hash, propRoot, version);
		}

		private static object ReadPropertyVersion(PropertyInfo prop, string hash, DirectoryInfo propRoot, int version)
		{
			string readFromRootDir = Path.Combine(propRoot.FullName, hash);
			IpcMessage msg = IpcMessage.Get(version.ToString(), prop.PropertyType, readFromRootDir);
			return msg.Read<object>();
		}

		protected void DeleteProperty(PropertyInfo prop, string hash)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(prop);
			string deleteDir = Path.Combine(propRoot.FullName, hash);
			DirectoryInfo dir = new DirectoryInfo(deleteDir);
			if (dir.Exists)
			{
				dir.Delete(true);
			}
		}

		public void WriteProperty(PropertyInfo prop, object value)
		{
			object propValue = prop.GetValue(value);
			if (propValue != null)
			{
				string idHash = GetIdHash(value);
				WriteProperty(prop, propValue, idHash);
				string uuidHash = GetUuidHash(value);
				WriteProperty(prop, propValue, uuidHash);
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
			DirectoryInfo typeDirectory = GetTypeDirectory(prop.DeclaringType);
			DirectoryInfo dir = new DirectoryInfo(Path.Combine(typeDirectory.FullName, prop.Name));
			if (!dir.Exists)
			{
				dir.Create();
			}
			return dir;
		}
		
		protected virtual string GetUuidHash(object value)
		{
			string result = GetUuidHash("", Type.Missing.GetType());
			if (value != null)
			{
				Type type = value.GetType();
				result = GetUuidHash("", type);
				PropertyInfo uuidProp = type.GetProperty("Uuid");
				if (uuidProp != null)
				{
					string uuid = (string)uuidProp.GetValue(value);
					if (!string.IsNullOrEmpty(uuid))
					{
						result = GetUuidHash(uuid, type);
					}
				}
			}
			return result;
		}

		protected virtual string GetUuidHash(string uuid, Type type)
		{
			string hash = "{0}::{1}"._Format(uuid, type.FullName).Md5();
			return hash;
		}

		protected virtual string GetIdHash(object value, Type type = null)
		{
			type = type ?? value.GetType();
			long id = Meta.GetId(value);
			return GetIdHash(id, type);
		}

		protected virtual string GetIdHash(long id, Type type)
		{
			string hash = "{0}::{1}"._Format(id, type.FullName).Md5();
			return hash;
		}

		protected internal List<FileInfo> GetPropertyFiles(PropertyInfo prop, string hash)
		{
			DirectoryInfo propRoot = GetPropertyDirectory(prop);
			return GetPropertyFiles(propRoot, prop, hash);
		}

		protected internal int GetNextVersionNumber(DirectoryInfo propRoot, PropertyInfo prop, string hash)
		{
			return Meta.GetNextVersionNumber(propRoot, prop, hash);
		}

		protected internal static int GetHighestVersionNumber(DirectoryInfo propRoot, PropertyInfo prop, string hash)
		{
			return Meta.GetHighestVersionNumber(propRoot, prop, hash);
		}
		
		private List<FileInfo> GetPropertyFiles(DirectoryInfo propRoot, PropertyInfo prop, string hash)
		{
			DirectoryInfo propRootForHash = new DirectoryInfo(Path.Combine(propRoot.FullName, hash, prop.PropertyType.Name));
			if (!propRootForHash.Exists)
			{
				propRootForHash.Create();
			}
			List<FileInfo> files = propRootForHash.GetFiles().Where(f => f.HasNoExtension()).ToList();
			return files;
		}

		private void SubscribeToIpcMessageEvents(IpcMessage msg)
		{
			Subscribers.Each(logger =>
			{
				msg.Subscribe(logger);
			});
		}

		private void WriteProperty(PropertyInfo prop, object propValue, string hash)
		{
			// TODO: add a check for supported datatypes see Bam.Net.Data.Schema.DataTypes
			DirectoryInfo propRoot = GetPropertyDirectory(prop);
			int version = GetNextVersionNumber(propRoot, prop, hash);
			string writeToRootDir = Path.Combine(propRoot.FullName, hash);
			IpcMessage msg = IpcMessage.Get(version.ToString(), prop.PropertyType, writeToRootDir);
			msg.Write(propValue);
		}

		private IpcMessage GetIdMessage(object data)
		{
			string idHash = GetIdHash(data);
			return GetHashMessage(data, idHash);
		}
		private IpcMessage GetUuidMessage(object data)
		{
			string uuidHash = GetUuidHash(data);
			return GetHashMessage(data, uuidHash);
		}

		private IpcMessage GetHashMessage(object data, string hash)
		{
			IpcMessage idMessage;
			Type type = data.GetType();
			if (!IpcMessage.Exists(hash, type, RootDirectory, out idMessage))
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
				Meta meta = new Meta(instance, this);
				if (predicate(instance) && !metaList.Contains(meta))
				{
					metaList.Add(meta);
					results.Add(instance);
				}
			}

			return results.ToArray();
		}
	}
}
