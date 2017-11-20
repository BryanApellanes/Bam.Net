/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using Bam.Net;

namespace Bam.Net.Data.Repositories
{
	/// <summary>
	/// A class responsible for saving and retrieving
	/// Poco objects.  Currently not designed for performance
	/// and is primarily used for backups.  This "should"
	/// be changed in the future to improve performance.  Uses
    /// a BinaryFormatter to serialize object instances to
    /// disk so those objects should not be complex types
    /// and must be marked Serializable.
	/// </summary>
	public class ObjectRepository : Repository
	{
		public ObjectRepository() 
		{
			ObjectReaderWriter = Repositories.ObjectReaderWriter.Default;
			MetaProvider = Repositories.MetaProvider.Default;
			BlockOnChildWrites = false;
			TypeSchemaGenerator = new TypeSchemaGenerator();            
		}

		public ObjectRepository(string rootDirectory)
			: this()
		{
			ObjectReaderWriter = new Repositories.ObjectReaderWriter(rootDirectory);
			BlockOnChildWrites = false;
		}

		bool isInitialized;
		readonly object _initLock = new object();
		protected void Initialize()
		{
			lock (_initLock)
			{
				if (!isInitialized)
				{
					if (!StorableTypes.Any())
					{
						throw new InvalidOperationException("No types were specified.  Call AddType for each type to store.");
					}
					isInitialized = true;
					SchemaDefinitionCreateResult = TypeSchemaGenerator.CreateSchemaDefinition(StorableTypes);
				}
			}
		}

		public override T Create<T>(T toCreate)
		{
			ObjectReaderWriter.Write(typeof(T), toCreate);
			SaveCollections(toCreate);
			return toCreate;
		}
        public override object Create(Type type, object toCreate)
        {
            ObjectReaderWriter.Write(type, toCreate);
            SaveCollections(toCreate);
            return toCreate;
        }
        public override object Create(object toCreate)
		{
			ObjectReaderWriter.Write(toCreate.GetType(), toCreate);
			SaveCollections(toCreate);
			return toCreate;
		}

		public override T Update<T>(T toUpdate)
		{
			ObjectReaderWriter.Write(typeof(T), toUpdate);
			SaveCollections(toUpdate);
			return toUpdate;
		}

        public override object Update(Type type, object toUpdate)
        {
            ObjectReaderWriter.Write(type, toUpdate);
            SaveCollections(toUpdate);
            return toUpdate;
        }

        public override object Update(object toUpdate)
		{
			ObjectReaderWriter.Write(toUpdate.GetType(), toUpdate);
			SaveCollections(toUpdate);
			return toUpdate;
		}

		public override T Retrieve<T>(int id)
		{
			return Retrieve<T>((long)id);
		}

		public override T Retrieve<T>(long id)
		{
			T value = ObjectReaderWriter.Read<T>(id);
            if(value != null)
            {
                LoadXrefCollectionValues(value);
            }
			return value;
		}

		public override object Retrieve(Type objectType, long id)
		{
			object obj = ObjectReaderWriter.Read(objectType, id);
            if(obj != null)
            {
                LoadXrefCollectionValues(obj);
            }
			return obj;
		}

        public override T Retrieve<T>(string uuid)
        {
            return (T)Retrieve(typeof(T), uuid);
        }

        public override object Retrieve(Type objectType, string uuid)
		{
			object obj = ObjectReaderWriter.Read(objectType, uuid);
            if (obj != null)
            {
                LoadXrefCollectionValues(obj);
            }
			return obj;
		}

		public override IEnumerable<T> RetrieveAll<T>() 
		{
			return this.Query<T>(t => ((object)t).Property<long>("Id") > 0);
		}

        public override void BatchRetrieveAll(Type type, int batchSize, Action<IEnumerable<object>> processor)
        {
            List<object> batch = new List<object>();
            foreach(object obj in RetrieveAll(type))
            {
                batch.Add(obj);
                if(batch.Count == batchSize)
                {
                    processor(batch);
                    batch = new List<object>();
                }
            }
        }

		public override IEnumerable<object> RetrieveAll(Type type)
		{
			return Query(type, t => ((object)t).Property<long>("Id") > 0);
		}

		public override IEnumerable<object> Query(string propertyName, object value)
		{
			return Query(DefaultType, o => o.Property(propertyName).Equals(value));
		}

		public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
		{
			return ObjectReaderWriter.Query(type, predicate);
		}

		public override IEnumerable<T> Query<T>(Func<T, bool> query) 
		{
			IEnumerable<T> results = ObjectReaderWriter.Query<T>(query);
			LoadXrefCollectionValues<T>(results);
			return results;
		}

		public IEnumerable<T> Query<T>(string propertyName, object value)
		{
			IEnumerable<T> results = ObjectReaderWriter.QueryProperty<T>(propertyName, value);
			LoadXrefCollectionValues<T>(results);
			return results;
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            List<T> results = new List<T>();
            T[] queryResults = ObjectReaderWriter.Query<T>(o =>
            {
                return WherePropertiesEqual(queryParameters, o);
            });
            results.AddRange(queryResults);
            LoadXrefCollectionValues<T>(results);
            return results;
        }

        public override IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters)
        {
            List<object> results = new List<object>();
            object[] queryResults = ObjectReaderWriter.Query(type, o =>
            {
                return WherePropertiesEqual(queryParameters, o);
            });
            results.AddRange(queryResults);
            results.Each(o => LoadXrefCollectionValues(o));
            return results;
        }

		public IEnumerable<T> Query<T>(string propertyName, Func<object, bool> predicate)
		{
			IEnumerable<T> results = ObjectReaderWriter.QueryProperty<T>(propertyName, predicate);
			LoadXrefCollectionValues<T>(results);
			return results;
		}

        public override IEnumerable<T> Query<T>(QueryFilter query)
        {
            return Query<T>(query.ToDictionary());
        }

        public override IEnumerable<object> Query(Type type, QueryFilter query)
        {
            return Query(type, query.ToDictionary());
        }

        public override bool Delete<T>(T toDelete)
		{
			return ObjectReaderWriter.Delete(toDelete);
		}
        public override bool Delete(Type type, object toDelete)
        {
            return Delete(toDelete);
        }
        public override bool Delete(object toDelete)
		{
			return ObjectReaderWriter.Delete(toDelete);
		}

		public SchemaDefinitionCreateResult SchemaDefinitionCreateResult
		{
			get;
			private set;
		}

		/// <summary>
		/// Sets the properties that represent PrimaryKeys if any
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="child"></param>
		public void SetParentProperties(object parent, object child)
		{
            TypeSchemaPropertyManager.SetParentProperties(parent, child);			
		}
		
		public void LoadXrefCollectionValues<T>(IEnumerable<T> results)
		{
			foreach (T obj in results)
			{
				LoadXrefCollectionValues(obj);
			}
		}
		
		public void LoadXrefCollectionValues(object data)
		{
			Type type = data.GetType();						
			Meta meta = data as Meta ?? new Meta(data, ObjectReaderWriter);
			Type listType = typeof(List<>);
			string dataHash = meta.Hash;

			List<Meta> leftMeta = new List<Meta>();
			TypeXref[] leftXrefs = TypeSchema.Xrefs.Where(xref => xref.Left.Equals(type)).ToArray();
			foreach(TypeXref leftXref in leftXrefs)
			{
				Type genericRight = listType.MakeGenericType(leftXref.Right);
                IList listInstance = genericRight.Construct<IList>();

				XrefInfo[] xrefs = ObjectReaderWriter.Query<XrefInfo>(xref => xref.LeftHash.Equals(dataHash));
				foreach (XrefInfo xref in xrefs)
				{
					object value = ObjectReaderWriter.ReadByHash(Type.GetType(xref.RightType), xref.RightHash);
					Meta valueMeta = new Meta(value, ObjectReaderWriter);
					if (!leftMeta.Contains(valueMeta))
					{
						leftMeta.Add(valueMeta);
						listInstance.Add(value);
					}
				}
				if(leftXref.RightCollectionProperty.PropertyType.IsArray)
				{
					Array arr = Array.CreateInstance(leftXref.Right, listInstance.Count);
					listInstance.CopyTo(arr, 0);
					leftXref.RightCollectionProperty.SetValue(data, arr);
				}
				else
				{
					leftXref.RightCollectionProperty.SetValue(data, listInstance);
				}
			}

			List<Meta> rightMeta = new List<Meta>();
			TypeXref[] rightXrefs = TypeSchema.Xrefs.Where(xref => xref.Right.Equals(type)).ToArray();	
			foreach (TypeXref rightXref in rightXrefs)
			{				
				Type genericLeft = listType.MakeGenericType(rightXref.Left);
                IList listInstance = genericLeft.Construct<IList>();

				XrefInfo[] xrefs = ObjectReaderWriter.Query<XrefInfo>(xref => xref.RightHash.Equals(dataHash));
				foreach (XrefInfo xref in xrefs)
				{
					object value = ObjectReaderWriter.ReadByHash(Type.GetType(xref.LeftType), xref.LeftHash);
					Meta valueMeta = new Meta(value, ObjectReaderWriter);
					if (!rightMeta.Contains(valueMeta))
					{
						rightMeta.Add(valueMeta);
						listInstance.Add(value);
					}
				}

				if(rightXref.LeftCollectionProperty.PropertyType.IsArray)
				{
					Array arr = Array.CreateInstance(rightXref.Left, listInstance.Count);
					listInstance.CopyTo(arr, 0);
					rightXref.LeftCollectionProperty.SetValue(data, arr);
				}
				else
				{
					rightXref.LeftCollectionProperty.SetValue(data, listInstance);
				}
			}
		}

		bool _blockOnChildWrites;
		public bool BlockOnChildWrites
		{
			get
			{
				return _blockOnChildWrites;
			}
			set
			{
				_blockOnChildWrites = value;
				if (_blockOnChildWrites)
				{
					ChildWriter = ObjectReaderWriter.Write;
				}
				else
				{
					ChildWriter = ObjectReaderWriter.Enqueue;
				}
			}
		}

		protected Action<Type, object> ChildWriter { get; set; }
        TypeSchemaPropertyManager _typeSchemaPropertyManager;
        object _typeSchemaPropertyManagerLock = new object();
        protected TypeSchemaPropertyManager TypeSchemaPropertyManager
        {
            get
            {
                return _typeSchemaPropertyManagerLock.DoubleCheckLock(ref _typeSchemaPropertyManager, () => new TypeSchemaPropertyManager(TypeSchema));
            }
        }
            
		protected internal IObjectReaderWriter ObjectReaderWriter { get; set; }
		protected internal IMetaProvider MetaProvider { get; set; }
		protected TypeSchemaGenerator TypeSchemaGenerator { get; private set; }
        object _typeSchemaLock = new object();
		protected internal TypeSchema TypeSchema
		{
			get
			{
                lock (_typeSchemaLock)
                {
                    if (SchemaDefinitionCreateResult == null)
                    {
                        Initialize();
                    }
                    return SchemaDefinitionCreateResult.TypeSchema;
                }
			}
		}

		protected void SaveCollections(object parent)
		{
			SaveChildCollections(parent);
			SaveXrefCollections(parent);
		}

		protected void SaveChildCollections(object parent)
		{
            TypeSchemaPropertyManager.SaveCollections(parent, ChildWriter);
		}

		protected void SaveXrefCollections(object parent)
		{
            Type type = parent.GetType();
            TypeXref[] leftXrefs = TypeSchema.Xrefs.Where(xref => xref.Left.Equals(type)).ToArray();
            foreach (TypeXref leftXref in leftXrefs)
            {
                IEnumerable collection = (IEnumerable)leftXref.RightCollectionProperty.GetValue(parent);
                if (collection != null)
                {
                    foreach (object obj in collection)
                    {
                        ChildWriter(leftXref.RightCollectionProperty.GetEnumerableType(), new Meta(obj, ObjectReaderWriter));
                        XrefInfo xrefInfo = new XrefInfo(parent, obj, MetaProvider.GetMeta(parent).Hash, MetaProvider.GetMeta(obj).Hash);
                        ChildWriter(typeof(XrefInfo), xrefInfo);
                    }
                }
            }

            TypeXref[] rightXrefs = TypeSchema.Xrefs.Where(xref => xref.Right.Equals(type)).ToArray();
            foreach (TypeXref rightXref in rightXrefs)
            {
                IEnumerable collection = (IEnumerable)rightXref.LeftCollectionProperty.GetValue(parent);
                if (collection != null)
                {
                    foreach (object obj in collection)
                    {
                        ChildWriter(rightXref.LeftCollectionProperty.GetEnumerableType(), new Meta(obj, ObjectReaderWriter));
                        XrefInfo xrefInfo = new XrefInfo(obj, parent, MetaProvider.GetMeta(obj).Hash, MetaProvider.GetMeta(parent).Hash);
                        ChildWriter(typeof(XrefInfo), xrefInfo);
                    }
                }
            }
        }

        private static bool WherePropertiesEqual<T>(Dictionary<string, object> queryParameters, T o) where T : class, new()
        {
            bool result = true;
            foreach (string key in queryParameters.Keys)
            {
                object val = o.Property(key);
                if ((val != null && !val.Equals(queryParameters[key]) ||
                    (val == null && queryParameters[key] != null)))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
    }
}
