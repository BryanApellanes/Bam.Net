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
using NCuid;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Bam.Net.Data.Repositories.Meta" />
    [Serializable]
	public class Meta<T>: Meta
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Meta{T}"/> class.
        /// </summary>
        public Meta() : base()
        {
            Type = typeof(T);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta{T}"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="objectReaderWriter">The object reader writer.</param>
        public Meta(T data, IObjectPersister objectReaderWriter) : base(data, objectReaderWriter)
        {
            Type = typeof(T);
        }

        /// <summary>
        /// Gets or sets the typed data.
        /// </summary>
        /// <value>
        /// The typed data.
        /// </value>
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

        /// <summary>
        /// Performs an implicit conversion from <see cref="Meta{T}"/> to T.
        /// </summary>
        /// <param name="meta">The meta.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static implicit operator T(Meta<T> meta)
		{
			return meta.TypedData;
		}
	}

    /// <summary>
    /// Provides meta data about peristed or persistable objects.
    /// </summary>
    /// <seealso cref="Bam.Net.Data.Repositories.Meta" />
    [Serializable]
	public partial class Meta
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        public Meta()
		{
			RequireIdProperty = true;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="Meta"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="objectReaderWriter">The object reader writer.</param>
        /// <param name="setMeta">if set to <c>true</c> [set meta].</param>
        public Meta(object data, IObjectPersister objectReaderWriter, bool setMeta = true)
		{
			RequireIdProperty = true;
			ObjectPersister = objectReaderWriter;
			if(setMeta)
			{
				SetMeta(data);
			}
		}

        Type _type;
		public Type Type
		{
			get
			{
				return _type ?? Data?.GetType();
			}
            set
            {
                _type = value;
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
				T result = ObjectPersister.ReadProperty<T>(propInfo, Uuid);
				return result;
			}

			return default(T);
		}

		public T ReadPropertyVersion<T>(PropertyInfo propInfo, int version)
		{
			if (propInfo != null)
			{
				T result = ObjectPersister.ReadPropertyVersion<T>(propInfo, Hash, version);
				return result;
			}

			return default(T);
		}

		public void WriteProperty(PropertyInfo propInfo, object propertyValue)
		{
			if (propInfo != null)
			{
				propInfo.SetValue(Data, propertyValue);
				ObjectPersister.Write(Type, Data);
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

		public ulong Id
		{
			get
			{
				if (Data != null)
				{
					return GetId(RequireIdProperty).Value;
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
                SetCuid(Data);
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
			if (string.IsNullOrEmpty(hash)) 
			{
				hash = GetIdHash();
			}

			return hash;
		}

		protected string GetIdHash()
		{
			Type type = Type ?? Type.Missing.GetType();
            string hash = "{0}::{1}"._Format(Id, type.FullName).Md5();
			return hash;
		}
		
		protected string GetUuidHash()
		{
            Type type = Type ?? Type.Missing.GetType();
            return GetUuidHash(Uuid, type);
		}
    
        public static string GetUuidHash(string uuid, Type type)
        {
            return "{0}::{1}"._Format(uuid, type.FullName).Md5();
        }

        public static string GetUuidHash(object value, Type type)
        {
            string result = GetUuidHash("", Type.Missing.GetType());
            if (value != null)
            {
                result = GetUuidHash("", type);
                PropertyInfo uuidProp = value.GetType().GetProperty("Uuid");
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

        public static string GetIdHash(object value, Type type = null)
        {
            type = type ?? value.GetType();
            ulong id = Meta.GetId(value).Value;
            return GetIdHash(id, type);
        }

        public static string GetIdHash(long id, Type type)
        {
            return "{0}::{1}"._Format(id, type.FullName).Md5();
        }

        public static string GetIdHash(ulong id, Type type)
        {
            return "{0}::{1}"._Format(id, type.FullName).Md5();
        }

        protected internal static string GetUuid(object data, bool throwIfUuidPropertyMissing = false)
        {
            return GetPropValue(data, "Uuid", throwIfUuidPropertyMissing);
        }

        protected internal static string GetCuid(object data, bool throwIfCuidPropertyMissing = false)
        {
            return GetPropValue(data, "Cuid", throwIfCuidPropertyMissing);
        }

        protected internal static string GetPropValue(object data, string propName, bool throwIfPropertyMissing = false)
		{
			string result = string.Empty;
			if (data != null)
			{
				Type dataType = data.GetType();
				PropertyInfo prop = dataType.GetProperty(propName);
				if (prop != null)
				{
					result = (string)prop.GetValue(data);
				}
				else if (throwIfPropertyMissing)
				{
					Args.Throw<InvalidOperationException>("The specified object of type {0} doesn't have a {1} property", dataType.Name, propName);
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

            if(keyProp != null && keyProp.PropertyType != typeof(ulong) && keyProp.PropertyType != typeof(ulong?))
            {
                throw new InvalidIdPropertyTypeException(keyProp);
            }
			return keyProp;
		}

		protected virtual ulong? GetId(bool throwIfNoIdProperty = true)
        {
            return GetId(Data, throwIfNoIdProperty);
        }

        protected internal static ulong? GetId(object value, bool throwIfNoIdProperty = true)
        {
            PropertyInfo pocoProp = GetKeyProperty(value.GetType(), throwIfNoIdProperty);
            object idValue = pocoProp.GetValue(value);   
            return (ulong?)idValue;
        }
		
		/// <summary>
		/// Sets the Id property of the specified value to the 
		/// next Id for it's type if it
		/// has not yet been set.
		/// </summary>
		/// <param name="value"></param>
		/// <param name="objectReaderWriter"></param>
		internal void SetId(object value, IObjectPersister objectReaderWriter = null)
		{
			objectReaderWriter = objectReaderWriter ?? this.ObjectPersister;
			Type type = value.GetType();
			PropertyInfo idProp = type.GetProperty("Id");
			if (idProp != null)
			{
				ulong id = (ulong)idProp.GetValue(value);
                if (id == 0)
                {
                    ulong retrievedId = GetNextId(type, objectReaderWriter);

                    idProp.SetValue(value, retrievedId);
                }
			}
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
                if (!Guid.TryParse(uuid, out Guid guid) || string.IsNullOrEmpty(uuid))
                {
                    guid = Guid.NewGuid();
                }
                uuidProp.SetValue(data, guid.ToString());
			}
		}

        internal static void SetCuid(object data, string cuid = "", RandomSource randomSource = RandomSource.Simple)
        {
            Type type = data.GetType();
            PropertyInfo cuidProp = type.GetProperty("Cuid");
            if(cuidProp != null)
            {
                if (string.IsNullOrEmpty(cuid))
                {
                    cuid = (string)cuidProp.GetValue(data);
                }
                if (string.IsNullOrEmpty(cuid))
                {
                    cuid = Cuid.Generate(randomSource);
                }
                cuidProp.SetValue(data, cuid);
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
			return GetHighestVersionNumber(this.ObjectPersister.GetPropertyDirectory(prop), prop, hash);
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
			return GetVersions(this.ObjectPersister.GetPropertyDirectory(prop), prop, hash);
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
			return GetPropertyFiles(this.ObjectPersister.GetPropertyDirectory(prop), prop, hash);
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
