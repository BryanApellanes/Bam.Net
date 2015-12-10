/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using Bam.Net;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
	[Serializable]
	public class Meta<T>: Meta
	{
		public Meta() : base() { }
		public Meta(T data, IObjectReaderWriter objectReaderWriter) : base(data, objectReaderWriter) { }

		public T TypedData
		{
			get
			{
				return (T)Data;
			}
			set
			{
				Data = value;
			}
		}

		public static implicit operator T(Meta<T> meta)
		{
			return meta.TypedData;
		}
	}

	[Serializable]
	public class Meta
	{
		public Meta()
		{
			this.RequireIdProperty = true;
		}
		public Meta(object data, IObjectReaderWriter objectReaderWriter, bool setMeta = true)
		{
			this.RequireIdProperty = true;
			this.ObjectReaderWriter = objectReaderWriter;
			if(setMeta)
			{
				this.SetMeta(data);
			}
		}

		IObjectReaderWriter _objectReaderWriter;
		object _objectReaderWriterLock = new object();
		public IObjectReaderWriter ObjectReaderWriter
		{
			get
			{
				return _objectReaderWriterLock.DoubleCheckLock(ref _objectReaderWriter, () => new ObjectReaderWriter(".\\"));
			}
			set
			{
				_objectReaderWriter = value;
			}
		}

		public Type Type
		{
			get
			{
				return Data.GetType();
			}
		}

		public object Data { get; internal set; }

		public bool IsSerializable
		{
			get
			{
				if (Data != null)
				{
					return Data.GetType().HasCustomAttributeOfType<SerializableAttribute>();
				}

				return false;
			}
		}

		public T ReadProperty<T>(PropertyInfo propInfo)
		{
			if (propInfo != null)
			{
				T result = this.ObjectReaderWriter.ReadProperty<T>(propInfo, Uuid);
				return result;
			}

			return default(T);
		}

		public T ReadPropertyVersion<T>(PropertyInfo propInfo, int version)
		{
			if (propInfo != null)
			{
				T result = this.ObjectReaderWriter.ReadPropertyVersion<T>(propInfo, Hash, version);
				return result;
			}

			return default(T);
		}

		public void WriteProperty(PropertyInfo propInfo, object propertyValue)
		{
			if (propInfo != null)
			{
				propInfo.SetValue(Data, propertyValue);
				this.ObjectReaderWriter.Write(Data);
			}
		}

		public bool RequireIdProperty
		{
			get;
			set;
		}

		/// <summary>
		/// Returns UuidHash
		/// </summary>
		public string Hash
		{
			get
			{
				return GetHash();
			}
		}

		public long Id
		{
			get
			{
				if (Data != null)
				{
					return GetId(Data, RequireIdProperty);
				}
				return 0;
			}
		}

		public string Uuid
		{
			get
			{
				return GetUuid(Data);
			}
			set
			{
				SetUuid(Data, value);
			}
		}

		public string IdHash
		{
			get
			{
				return GetIdHash();
			}
		}

		public string UuidHash
		{
			get
			{
				return GetUuidHash();
			}
		}

		public MetaProperty Property(string propertyName)
		{
			PropertyInfo prop = Data.GetType().GetProperty(propertyName);
			if (prop != null)
			{
				MetaProperty property = new MetaProperty(this, prop);
				return property;
			}

			return null;
		}

		public Meta Property(string propertyName, object value)
		{
			Property(propertyName).SetValue(value);
			return this;
		}

		public void SetMeta(object data)
		{
			Meta meta = data as Meta;
			if (meta != null)
			{
				data = meta.Data;
			}
			Data = data;
			SetMeta();
		}

		public static bool AreEqual(object one, object two)
		{
			return UuidsAreEqual(one, two);
		}

		public static bool IdsAreEqual(object one, object two)
		{
			return Meta.GetId(one) == Meta.GetId(two);
		}

		public static bool UuidsAreEqual(object one, object two)
		{
			return Meta.GetUuid(one).Equals(Meta.GetUuid(two));
		}

		public override bool Equals(object obj)
		{
			return AreEqual(this, obj);
		}

		public override int GetHashCode()
		{
			return Uuid.GetHashCode();
		}

		public virtual void SetMeta()
		{
			if(Data != null)
			{
				if(GetId(Data) <= 0)
				{
					SetId(Data);
				}
				if (string.IsNullOrEmpty(GetUuid(Data)))
				{
					SetUuid(Data);
				}
			}
		}

		public static void SetAuditFields(object value)
		{
			Args.ThrowIfNull(value, "value");
			try
			{
				string created = "Created";
				string modified = "Modified";
				DateTime now = DateTime.UtcNow;
				DateTime? createdDate = value.Property<DateTime?>(created);
				if (createdDate == null)
				{
					value.Property(created, now);
				}
				value.Property(modified, now);
			}
			catch (Exception ex)
			{
				Log.AddEntry("An error occurred trying to set audit fields on object of type {0}"._Format(value.GetType().Name), ex);
			}
		}

		protected string GetHash()
		{		
			string hash = GetUuidHash();
			if (string.IsNullOrEmpty(hash)) //TODO: review this for validity
			{
				hash = GetIdHash();
			}

			return hash;
		}

		protected string GetIdHash()
		{
			Type type = Data == null ? Type.Missing.GetType() : Data.GetType();
			string hash = "{0}::{1}"._Format(Id, type.FullName).Md5();
			return hash;
		}
		
		protected string GetUuidHash()
		{
			Type type = Data == null ? Type.Missing.GetType() : Data.GetType();
			string hash = "{0}::{1}"._Format(Uuid, type.FullName).Md5();
			return hash;
		}

		protected internal static string GetUuid(object data, bool throwIfUuidPropertyMissing = false)
		{
			string result = string.Empty;
			if (data != null)
			{
				Type dataType = data.GetType();
				PropertyInfo uuidProp = dataType.GetProperty("Uuid");
				if (uuidProp != null)
				{
					result = (string)uuidProp.GetValue(data);
				}
				else if (throwIfUuidPropertyMissing)
				{
					Args.Throw<InvalidOperationException>("The specified object of type {0} doesn't have a Uuid property", dataType.Name);
				}
			}
			return result;
		}

		protected internal static bool HasKeyProperty(object data, out PropertyInfo prop)
		{
			prop = GetKeyProperty(data.GetType(), false);
			return prop != null;
		}

		protected internal static bool IsNew(object data)
		{
			return !IdIsSet(data) && !UuidIsSet(data);
		}

		protected internal static bool IdIsSet(object data)
		{
			bool result = false;
			PropertyInfo key;
			if (HasKeyProperty(data, out key))
			{
				result = (long)key.GetValue(data) > 0;
			}

			return result;
		}

		protected internal static bool UuidIsSet(object data)
		{
			bool result = false;
			PropertyInfo uuidProp = data.GetType().GetProperty("Uuid");
			if (uuidProp != null)
			{
				result = !string.IsNullOrEmpty((string)uuidProp.GetValue(data));
			}

			return result;
		}

		protected internal static PropertyInfo GetKeyProperty(Type type, bool throwIfNoIdProperty = true)
		{
			PropertyInfo keyProp = type.GetFirstProperyWithAttributeOfType<KeyAttribute>();
			if (keyProp == null)
			{
				keyProp = type.GetProperty("Id");
			}

			if (keyProp == null && throwIfNoIdProperty)
			{
				throw new NoIdPropertyException(type);
			}
			return keyProp;
		}
		
		protected internal static long GetId(object value, bool throwIfNoIdProperty = true)
		{
			PropertyInfo pocoProp = GetKeyProperty(value.GetType(), throwIfNoIdProperty);
			object idValue = pocoProp.GetValue(value);
			return idValue == null ? 0 : (long)idValue;
		}
		
		/// <summary>
		/// Sets the Id property of the specified value to the 
		/// next Id for it's type if it
		/// has not yet been set.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="objectReaderWriter"></param>
		internal void SetId(object value, IObjectReaderWriter objectReaderWriter = null)
		{
			objectReaderWriter = objectReaderWriter ?? this.ObjectReaderWriter;
			Type type = value.GetType();
			PropertyInfo idProp = type.GetProperty("Id");
			if (idProp != null)
			{
				long id = (long)idProp.GetValue(value);
				if (id == 0)
				{
					long retrievedId = GetNextId(type, objectReaderWriter);

					idProp.SetValue(value, retrievedId);
				}
			}
		}

		protected internal long GetNextId(Type type, IObjectReaderWriter objectReaderWriter = null)
		{
			objectReaderWriter = objectReaderWriter ?? this.ObjectReaderWriter;
			DirectoryInfo dir = new DirectoryInfo(Path.Combine(objectReaderWriter.RootDirectory, type.Name));
			IpcMessage msg = IpcMessage.Get("meta.id", typeof(MetaId), dir.FullName);
			MetaId metaId = msg.Read<MetaId>();
			if (metaId == null)
			{
				msg.Write(new MetaId { Value = 0 });
			}
			long retrievedId = ++msg.Read<MetaId>().Value;
			msg.Write(new MetaId { Value = retrievedId });
			return retrievedId;
		}

		/// <summary>
		/// Sets the Uuid property of the specified data if
		/// it has not already been set
		/// </summary>
		/// <param name="data"></param>
		/// <param name="uuid"></param>
		internal static void SetUuid(object data, string uuid = "")
		{
			Type type = data.GetType();
			PropertyInfo uuidProp = type.GetProperty("Uuid");
			if (uuidProp != null)
			{
				if (string.IsNullOrEmpty(uuid))
				{
					uuid = (string)uuidProp.GetValue(data);
				}
				Guid guid;
				if (!Guid.TryParse(uuid, out guid) || string.IsNullOrEmpty(uuid))
				{
					guid = Guid.NewGuid();
				}

				uuidProp.SetValue(data, guid.ToString());
			}
		}

		protected internal static int GetNextVersionNumber(DirectoryInfo propRoot, PropertyInfo prop, string hash)
		{
			int num = 0;
			int highest = GetHighestVersionNumber(propRoot, prop, hash);
			num = highest + 1;
			return num;
		}

		protected internal int GetHighestVersionNumber(PropertyInfo prop, string hash)
		{
			return GetHighestVersionNumber(this.ObjectReaderWriter.GetPropertyDirectory(prop), prop, hash);
		}

		protected internal static int GetHighestVersionNumber(DirectoryInfo propRoot, PropertyInfo prop, string hash)
		{
			int highest = 0;
			if (!propRoot.Exists)
			{
				propRoot.Create();
			}
			List<FileInfo> files = GetPropertyFiles(propRoot, prop, hash);
			if (files.Count > 0)
			{
				files.Sort((one, two) => two.Name.CompareTo(one.Name));
				int.TryParse(files[0].Name, out highest);
			}
			return highest;
		}
		
		protected internal int[] GetVersions(PropertyInfo prop, string hash)
		{
			return GetVersions(this.ObjectReaderWriter.GetPropertyDirectory(prop), prop, hash);
		}

		protected internal static int[] GetVersions(DirectoryInfo propertyDirectory, PropertyInfo prop, string hash)
		{
			List<FileInfo> files = GetPropertyFiles(propertyDirectory, prop, hash);
			return files.Select(f => int.Parse(f.Name)).ToArray();
		}

		protected internal Dictionary<int, DateTime> GetVersionDates(PropertyInfo prop, string hash)
		{
			Dictionary<int, DateTime> results = new Dictionary<int, DateTime>();
			foreach(FileInfo file in GetPropertyFiles(prop, hash))
			{
				results.Add(int.Parse(file.Name), file.LastWriteTime);
			}
			return results;
		}

		protected internal List<FileInfo> GetPropertyFiles(PropertyInfo prop, string hash)
		{
			return GetPropertyFiles(this.ObjectReaderWriter.GetPropertyDirectory(prop), prop, hash);
		}

		protected internal static List<FileInfo> GetPropertyFiles(DirectoryInfo propertyDirectory, PropertyInfo prop, string hash)
		{
			DirectoryInfo propRootForHash = new DirectoryInfo(Path.Combine(propertyDirectory.FullName, hash, prop.PropertyType.Name));
			if (!propRootForHash.Exists)
			{
				propRootForHash.Create();
			}
			List<FileInfo> files = propRootForHash.GetFiles().Where(f => f.HasNoExtension()).ToList();
			return files;
		}
	}
}
