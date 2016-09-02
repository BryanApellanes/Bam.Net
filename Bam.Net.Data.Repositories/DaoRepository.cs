/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Logging;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A repository that will generate an underlying Dao
    /// for the types added.  Any values returned by a 
    /// call to Query will not be fully hydrated (child lists
    /// won't be populated).  To ensure full hydration of
    /// the values call Retrieve(id) or Retrieve(uuid).
    /// </summary>
    public class DaoRepository : Repository, IGeneratesDaoAssembly, IHasTypeSchemaTempPathProvider
    {
        /// <summary>
        /// Create an instance of DaoRepository
        /// </summary>
        /// <param name="tableNameProvider"></param>
        /// <param name="schemaTempPathProvider"></param>
        public DaoRepository(ITypeTableNameProvider tableNameProvider = null, Func<SchemaDefinition, TypeSchema, string> schemaTempPathProvider = null)
        {
            TypeDaoGenerator = new TypeDaoGenerator();
            TypeSchemaGenerator = new TypeSchemaGenerator();
            TypeDaoGenerator.SchemaWarning += (o, a) =>
            {
                FireEvent(SchemaWarning, a);
            };
            TypeDaoGenerator.GenerateDaoAssemblySucceeded += (o, a) =>
            {
                GenerateDaoAssemblyEventArgs args = (GenerateDaoAssemblyEventArgs)a;
                FireEvent(GenerateDaoAssemblySucceeded, args);
            };

            WarningsAsErrors = true;
            Logger = Log.Default;
        }
        public DaoRepository(Database database, ILogger logger = null, string schemaName = null)
            : this()
        {
            Database = database;
            Logger = logger ?? Log.Default;
            Subscribe(logger);
            SchemaName = schemaName;
        }

        protected TypeDaoGenerator TypeDaoGenerator
        {
            get; set;
        }

        protected TypeSchemaGenerator TypeSchemaGenerator
        {
            get; set;
        }

        /// <summary>
        /// The namespace to place generated classes into
        /// </summary>
        public string Namespace
        {
            get
            {
                return TypeDaoGenerator.Namespace;
            }
            set
            {
                TypeDaoGenerator.Namespace = value.Replace("Dao", "_Dao_");
            }
        }

        public bool KeepSource
        {
            get
            {
                return TypeDaoGenerator.KeepSource;
            }
            set
            {
                TypeDaoGenerator.KeepSource = value; 
            }
        }

        public Func<SchemaDefinition, TypeSchema, string> TypeSchemaTempPathProvider
        {
            get
            {
                return TypeDaoGenerator.TypeSchemaTempPathProvider;
            }
            set
            {
                TypeDaoGenerator.TypeSchemaTempPathProvider = value;
            }
        }

        public string SchemaName
        {
            get
            {
                return TypeDaoGenerator.SchemaName;
            }
            set
            {
                TypeDaoGenerator.SchemaName = value;
            }
        }

		public bool WarningsAsErrors
        {
            get { return TypeDaoGenerator.WarningsAsErrors; }
            set { TypeDaoGenerator.WarningsAsErrors = value; }
        }

		public Database Database { get; set; }

		[Verbosity(VerbosityLevel.Information)]
		public event EventHandler GenerateDaoAssemblySucceeded;		

		public SchemaDefinition SchemaDefinition
		{
			get
			{
				return TypeDaoGenerator.SchemaDefinitionCreateResult.SchemaDefinition;
			}
		}

        TypeSchema _typeSchema;
		public TypeSchema TypeSchema
		{
			get
			{
                if(_typeSchema == null)
                {
                    Initialize();
                    _typeSchema = TypeSchemaGenerator.CreateTypeSchema(StorableTypes);
                }
                return _typeSchema;
			}
		}
        [Verbosity(VerbosityLevel.Warning, MessageFormat = "Missing {PropertyType} property: {ClassName}.{PropertyName}")]
        public event EventHandler SchemaWarning;

        Assembly _daoAssembly;
        protected EnsureSchemaStatus SchemaStatus { get; set; }
        /// <summary>
        /// Generates a Dao Assembly for the underlying 
        /// storable types if it has not yet been generated
        /// </summary>
        /// <returns></returns>
		public Assembly EnsureDaoAssemblyAndSchema(bool useExisting = true)
        {            
            if (_daoAssembly == null)
            {
                _daoAssembly = GenerateDaoAssembly(useExisting);
            }

            Args.ThrowIfNull(Database, "Database");
            if (SchemaStatus == EnsureSchemaStatus.Invalid) 
            {
                MultiTargetLogger logger = new MultiTargetLogger();
                Subscribers.Each(l => logger.AddLogger(l));
                SchemaStatus = Database.TryEnsureSchema(_daoAssembly.GetTypes().First(type => type.HasCustomAttributeOfType<TableAttribute>()), logger);
            }

            return _daoAssembly;
        }

        public override void Subscribe(ILogger logger)
        {
            TypeDaoGenerator.Subscribe(logger);
            TypeSchemaGenerator.Subscribe(logger);
            base.Subscribe(logger);
        }

        public Assembly GenerateDaoAssembly(bool useExisting = true)
        {
            Initialize();
            _daoAssembly = TypeDaoGenerator.GetDaoAssembly(useExisting);
            return _daoAssembly;
        }

        bool isInitialized;
		readonly object _initLock = new object();
		public virtual void Initialize()
		{
            if (!isInitialized)
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
                        StorableTypes.Each(type => TypeDaoGenerator.AddType(type));
                    }
                }
            }
		}

        /// <summary>
        /// Convert the specified instance to it's dynamic
        /// json safe representation
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object ToDto(object instance)
        {
            Type daoType = GetDaoType(instance.GetType());
            Dao o = (Dao)daoType.Construct();
            o.CopyProperties(instance);
            return o.ToJsonSafe();
        }

        /// <summary>
        /// Add the specified type as a storable type.
        /// When the underlying schema is generated for the 
        /// specified type it will be analyzed for its 
        /// relationships to other types as necessary
        /// and those types will be included in the 
        /// resulting schema
        /// </summary>
        /// <param name="type"></param>
		public override void AddType(Type type)
		{
			if (type.GetProperty("Id") == null &&
				type.GetFirstProperyWithAttributeOfType<KeyAttribute>() == null)
			{
				throw new NoIdPropertyException(type);
			}
			base.AddType(type);
			_daoAssembly = null;
		}

		/// <summary>
		/// Creates (Saves) the specified instance of T.  While
		/// the parameter value specified will be updated with 
		/// the newly assigned Id, one should favor using the
		/// return value instead as it will be an augmented extension
		/// of T.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="toCreate"></param>
		/// <returns></returns>
		public override T Create<T>(T toCreate)
		{
			return (T)Create((object)toCreate);
		}

		public override object Create(object toCreate)
		{
			try
			{
				Initialize();
				Type daoType = GetDaoType(GetBaseType(toCreate.GetType()));
				Dao daoInstance = daoType.Construct<Dao>();
				daoInstance.ForceInsert = true;
				object poco = SetDaoInstancePropertiesAndSave(toCreate, daoInstance);
				return poco;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnCreateFailed(new RepositoryEventArgs(ex));
				return null;
			}
		}

		public override T Retrieve<T>(int id)
		{
			return Retrieve<T>((long)id);
		}

		public override T Retrieve<T>(long id)
		{
			return (T)Retrieve(typeof(T), id);
		}

		public override object Retrieve(Type objectType, long id)
		{
			try
			{
				Initialize();
				Dao daoInstance = GetDaoInstanceById(objectType, id);
				if (daoInstance != null)
				{
					return GetWrapperInstance(objectType, daoInstance);
				}
				return null;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnRetrieveFailed(new RepositoryEventArgs(ex));
				return null;
			}
		}

        public override T Retrieve<T>(string uuid)
        {
            return (T)Retrieve(typeof(T), uuid);
        }

		public override object Retrieve(Type objectType, string uuid)
		{
			try
			{
				Initialize();
				Dao daoInstance = GetDaoInstanceByUuid(objectType, uuid);
				if (daoInstance != null)
				{
					return GetWrapperInstance(objectType, daoInstance);
				}
				return null;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnRetrieveFailed(new RepositoryEventArgs(ex));
				return null;
			}
		}

		public override IEnumerable<T> RetrieveAll<T>()// where T: new()
		{
            WarnRetrieveAll(typeof(T));
			return RetrieveAll(typeof(T)).CopyAs<T>();
		}

		public override IEnumerable<object> RetrieveAll(Type dtoOrPocoType)
		{
			Args.ThrowIfNull(Database, "Database");

			Type pocoType = GetBaseType(dtoOrPocoType);
			Type daoType = GetDaoType(pocoType);
			MethodInfo getterMethod = daoType.GetMethod("LoadAll", new Type[] { typeof(Database) });
			return new List<object>((IEnumerable<object>)getterMethod.Invoke(null, new object[] { Database }));
		}
        
		public override IEnumerable<object> Query(string propertyName, object value)
		{
			return Query<object>(Bam.Net.Data.Query.Where(propertyName) == value);
		}
        
		public override IEnumerable<T> Query<T>(Func<T, bool> predicate)
		{
            WarnRetrieveAll<T>();
            return RetrieveAll(typeof(T)).CopyAs<T>().Where(predicate);
		}

		public override IEnumerable<object> Query(Type type, Func<object, bool> predicate)
		{
            WarnRetrieveAll(type);
            return RetrieveAll(type).CopyAs(type).Where(predicate);
		}

		public override IEnumerable<object> Query(dynamic query)
		{
			return Query<object>((QueryFilter)query);
		}

		public override IEnumerable<T> Query<T>(dynamic query) 
		{
			return Query<T>((QueryFilter)query);
		}

        public override IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters)
        {
            QueryFilter filter = CreateQueryFilter(queryParameters);
            return Query<T>(filter);
        }

        public override IEnumerable Query(Type type, Dictionary<string, object> queryParameters)
        {
            QueryFilter filter = CreateQueryFilter(queryParameters);
            return Query(type, filter);
        }

		/// <summary>
		/// Updates the repository instance that represents the specified 
		/// value.  
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="toUpdate"></param>
		/// <returns></returns>
		public override T Update<T>(T toUpdate)
		{
			return (T)Update((object)toUpdate);
		}

		public override object Update(object toUpdate)
		{
			try
			{
				Initialize();
				Type pocoType = toUpdate.GetType();
				Dao daoInstance = GetDaoInstanceById(pocoType, GetIdValue(toUpdate)); 
				object poco = SetDaoInstancePropertiesAndSave(toUpdate, daoInstance);
				return poco;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnUpdateFailed(new RepositoryEventArgs(ex));
				return null;
			}
		}

		public override bool Delete<T>(T toDelete)
		{
			return Delete((object)toDelete);
		}

		public override bool Delete(object toDelete)
		{
			try
			{
				Initialize();
				Type pocoType = toDelete.GetType();
				object daoInstance = GetDaoInstanceById(pocoType, GetIdValue(toDelete));
				if (daoInstance != null)
				{
					MethodInfo deleteMethod = daoInstance.GetType().GetMethod("Delete", new Type[] { typeof(Database) });
					deleteMethod.Invoke(daoInstance, new object[] { Database });
					return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				LastException = ex;
				OnDeleteFailed(EventArgs.Empty);
				return false;
			}
		}

        public IEnumerable<T> Query<T>(Expression<Func<T, bool>> expression) where T : class, new()
        {
            DaoExpressionFilter expressionFilter = new DaoExpressionFilter(Logger);
            return Query<T>(expressionFilter.Where<T>(expression));
        }

		#region IDaoRepository Members
        
		public override IEnumerable<T> Query<T>(QueryFilter query)
		{
			Type pocoType = typeof(T);            
            IEnumerable daoResults = Query(pocoType, query);
            return Wrap<T>(daoResults);
		}

        public override IEnumerable Query(Type pocoType, QueryFilter query)
        {
            Type daoType = GetDaoType(pocoType);
            MethodInfo whereMethod = daoType.GetMethod("Where", new Type[] { typeof(QueryFilter), typeof(Database) });
            IEnumerable daoResults = (IEnumerable)whereMethod.Invoke(null, new object[] { query, Database });
            foreach (object daoResult in daoResults)
            {
                yield return ((Dao)daoResult).ToJsonSafe();
            }
        }
        #endregion

        public IEnumerable<T> Wrap<T>(IEnumerable values) where T : new()
        {
            Type wrapperType = GetWrapperType<T>();
            foreach (object value in values)
            {
                yield return (T)value.CopyAs(wrapperType, this);
            }
        }

        public IEnumerable<T> Top<T>(int count, QueryFilter query) where T : new()
        {
            return Top(count, typeof(T), query).CopyAs<T>();
        }

        public IEnumerable Top(int count, Type pocoType, QueryFilter query)
        {
            Type daoType = GetDaoType(pocoType);
            MethodInfo topMethod = daoType.GetMethod("Top", new Type[] { typeof(int), typeof(QueryFilter), typeof(Database) });
            IEnumerable daoResults = (IEnumerable)topMethod.Invoke(null, new object[] { count, query, Database });
            foreach(object daoResult in daoResults)
            {
                yield return ((Dao)daoResult).ToJsonSafe();
            }
        }

		public Type GetDaoType(Type pocoType)
		{
			Assembly daoAssembly = EnsureDaoAssemblyAndSchema();
            Type baseType = GetBaseType(pocoType);
			Type daoType = daoAssembly.GetType("{0}.{1}"._Format(TypeDaoGenerator.DaoNamespace, baseType.Name));
			return daoType;
		}

        /// <summary>
        /// Get the wrapper type for the specified developer defined 
        /// dto of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
		public Type GetWrapperType<T>() where T : new()
		{
			return GetWrapperType(typeof(T));
		}

		/// <summary>
		/// Get the wrapper type for the specified poco type.
		/// Will not return null unless the specified pocoType
		/// is null.
		/// </summary>
		/// <param name="baseOrWrapperType"></param>
		/// <returns></returns>
		public Type GetWrapperType(Type baseOrWrapperType)
		{
			if (baseOrWrapperType == null || baseOrWrapperType.Name.EndsWith("Wrapper"))
			{
				return baseOrWrapperType;
			}

			Type daoType = GetDaoType(baseOrWrapperType);
			Type dto = daoType.Assembly.GetType("{0}.{1}Wrapper"._Format(TypeDaoGenerator.WrapperNamespace, baseOrWrapperType.Name));
			Type result = dto ?? baseOrWrapperType;
			return result;
		}

        /// <summary>
        /// Gets the base poco type by analyzing the naming convention
        /// of the specified wrapperType
        /// </summary>
        /// <param name="wrapperType"></param>
        /// <returns></returns>
		public Type GetBaseType(Type wrapperType)
		{
			string baseTypeName = wrapperType.Name.EndsWith("Wrapper") ? wrapperType.Name.Truncate("Wrapper".Length) : wrapperType.Name;
			Type baseType = TypeSchema.Tables.FirstOrDefault(t => t.Name.Equals(baseTypeName));
			Type result = baseType ?? wrapperType;
			return result;
		}

        public IEnumerable<T> Wrap<T>(IEnumerable<T> values)
        {
            foreach(T value in values)
            {
                yield return Wrap<T>(value);
            }
        }

        /// <summary>
        /// Wrap the specified base instance to enable lazy loading
        /// of List or array properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseInstance"></param>
        /// <returns></returns>
        public T Wrap<T>(T baseInstance)
        {
            return (T)Wrap(typeof(T), baseInstance);
        }

        public object Wrap(object instance)
        {
            return Wrap(instance.GetType(), instance);
        }

        /// <summary>
        /// Wrap the specified base instance to enable lazy loading 
        /// of List or array properties
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public object Wrap(Type baseType, object instance)
        {
            object result = ConstructWrapper(baseType);
            result.CopyProperties(instance);
            SetParentProperties(result);

            return result;
        }

        public T ConstructWrapper<T>()
		{
			return (T)ConstructWrapper(typeof(T));
		}

		public object ConstructWrapper(Type baseType)
		{
			Type wrapperType = GetWrapperType(baseType);
			ConstructorInfo ctor = wrapperType.GetConstructor(new Type[] { typeof(DaoRepository) });
			object result = null;
			if (ctor == null)
			{
				ctor = wrapperType.GetConstructor(Type.EmptyTypes);
				if (ctor == null)
				{
					Args.Throw<InvalidOperationException>(
						"The specified type {0} doesn't have a parameterless constructor and no constructor taking a single parameter of type {1}",
						baseType.FullName, typeof(DaoRepository).FullName);
				}

				result = ctor.Invoke(new object[] { });
			}
			else
			{
				result = ctor.Invoke(new object[] { this });
			}

			return result;
		}

		public Dao GetDaoInstance(object baseInstance) // required by generated code
		{
			long id = GetIdValue(baseInstance);
			Dao dao = GetDaoInstanceById(baseInstance.GetType(), id);
			return dao;
		}

		public bool SetChildDaoCollectionValues(object poco, Dao daoInstance)
		{
			List<TypeFk> fkDescriptors = TypeSchema.ForeignKeys.Where(tfk => tfk.PrimaryKeyType == GetBaseType(poco.GetType())).ToList();
			bool result = false;
			foreach (TypeFk fkDescriptor in fkDescriptors)
			{
				PropertyInfo childCollectionDaoProperty = GetChildCollectionDaoPropertyForTypeFk(fkDescriptor);
				IEnumerable values = (IEnumerable)fkDescriptor.CollectionProperty.GetValue(poco) ?? new object[] { };
				IAddable daoCollection = (IAddable)childCollectionDaoProperty.GetValue(daoInstance);
				foreach (object o in values)
				{
					Meta.SetUuid(o);
                    Meta.SetCuid(o);
					Dao dao = GetDaoType(GetBaseType(o.GetType())).Construct<Dao>();
					dao.CopyProperties(o);
					daoCollection.Add(dao);
					result = true;
				}
			}

			return result;
		}

		public bool SetXrefDaoCollectionValues(object poco, Dao daoInstance)
		{
			Type baseType = GetBaseType(poco.GetType());
			Type daoType = GetDaoType(baseType);
            IHasUpdatedXrefCollectionProperties xrefPropertyProvider = poco as IHasUpdatedXrefCollectionProperties;
			bool result = false;
			HashSet<string> handledProperties = new HashSet<string>();
			if (xrefPropertyProvider != null)
			{
				handledProperties = new HashSet<string>(xrefPropertyProvider.UpdatedXrefCollectionProperties.Keys.ToList());
				xrefPropertyProvider.UpdatedXrefCollectionProperties.Keys.Each(daoXrefPropertyName =>
				{
					PropertyInfo daoXrefProperty = daoType.GetProperty(daoXrefPropertyName);
					PropertyInfo pocoProperty = xrefPropertyProvider.UpdatedXrefCollectionProperties[daoXrefPropertyName];
                    SetXrefDaoCollectionValues(poco, daoInstance, daoXrefProperty, pocoProperty);
					result = true;
				});
			}

			TypeXref[] leftXrefs = TypeSchema.Xrefs.Where(xref => xref.Left.Equals(baseType)).ToArray();
			foreach (TypeXref leftXref in leftXrefs)
			{
				string daoXrefPropertyName = "{0}"._Format(leftXref.Right.Name).Pluralize();
				if (!handledProperties.Contains(daoXrefPropertyName))
				{
					PropertyInfo daoXrefProperty = daoType.GetProperty(daoXrefPropertyName);
					PropertyInfo pocoProperty = leftXref.RightCollectionProperty;
					SetXrefDaoCollectionValues(poco, daoInstance, daoXrefProperty, pocoProperty);
					result = true;
				}
			}

			TypeXref[] rightXrefs = TypeSchema.Xrefs.Where(xref => xref.Right.Equals(baseType)).ToArray();
			foreach (TypeXref rightXref in rightXrefs)
			{
				string daoXrefPropertyName = "{0}"._Format(rightXref.Left.Name).Pluralize();
				if (!handledProperties.Contains(daoXrefPropertyName))
				{
					PropertyInfo daoXrefProperty = daoType.GetProperty(daoXrefPropertyName);
					PropertyInfo pocoProperty = rightXref.LeftCollectionProperty;
					SetXrefDaoCollectionValues(poco, daoInstance, daoXrefProperty, pocoProperty);
					result = true;
				}
			}
			return result;
		}


		public IEnumerable<TChildType> ForeignKeyCollectionLoader<TChildType>(object poco) where TChildType : new() // this is used by generated code; JIT compiler can't tell
		{
			// get all the child types where the foreign key property value equals the parent id
			Type pocoChildType = typeof(TChildType);
			TypeFk fkDescriptor = TypeSchema.ForeignKeys.FirstOrDefault(tfk => tfk.ForeignKeyType == typeof(TChildType));

			if (fkDescriptor != null)
			{
				List<TChildType> results = new List<TChildType>();
				string foreignKeyName = fkDescriptor.ForeignKeyProperty.Name;
				long parentId = GetIdValue(poco);
				if (parentId <= 0)
				{
					Args.Throw<InvalidOperationException>("IdValue not found for specified parent instance: {0}",
						poco.PropertiesToString());
				}
				QueryFilter filter = Bam.Net.Data.Query.Where(foreignKeyName) == parentId;
				Type childDaoType = GetDaoType(typeof(TChildType));
				MethodInfo whereMethod = childDaoType.GetMethod("Where", new Type[] { typeof(QueryFilter), typeof(Database) });
				IEnumerable daoResults = (IEnumerable)whereMethod.Invoke(null, new object[] { filter, Database });

				foreach (object dao in daoResults)
				{
					Type wrapperType = GetWrapperType<TChildType>();
					TChildType value = wrapperType.Construct<TChildType>(this);
					value.CopyProperties(dao);
					results.Add(value);
				}

				return results;
			}
			return new List<TChildType>();
		}

		/// <summary>
		/// Sets the properties that represent PrimaryKeys if any
		/// </summary>
		/// <param name="dtoInstance"></param>
		public void SetParentProperties(object dtoInstance)
		{
			Type pocoType = GetBaseType(dtoInstance.GetType());
			foreach (TypeFk typeFk in TypeSchema.ForeignKeys.Where(fk => fk.ForeignKeyType == pocoType))
			{
				PropertyInfo parentInstanceProperty = pocoType.GetProperty(typeFk.PrimaryKeyType.Name);
				if (parentInstanceProperty != null && !parentInstanceProperty.GetGetMethod().IsVirtual)
				{
					object value = GetParentPropertyOfChild(dtoInstance, typeFk.PrimaryKeyType);
					parentInstanceProperty.SetValue(dtoInstance, value);
				}
			}
		}

		/// <summary>
		/// Get the instance of the parentType specified for the 
		/// specified dto instance
		/// </summary>
		/// <param name="dtoChild"></param>
		/// <param name="parentType"></param>
		/// <returns></returns>
		public object GetParentPropertyOfChild(object dtoChild, Type parentType)
		{
			Type dtoType = GetWrapperType(dtoChild.GetType());
			if (dtoType != null)
			{
				string primaryIdPropertyName = "{0}Id"._Format(parentType.Name);
				PropertyInfo primaryIdProperty = dtoType.GetProperty(primaryIdPropertyName);
				if (primaryIdProperty != null)
				{
					long idValue = (long)primaryIdProperty.GetValue(dtoChild);
					object parentDaoInstance = GetDaoInstanceById(parentType, idValue);
					if (parentDaoInstance != null)
					{
						object value = parentType.Construct();
						value.CopyProperties(parentDaoInstance);
						return value;
					}
				}
			}

			return null;
		}

        public T Construct<T>() where T : AuditRepoData, new()
        {
            T result = new T();
            result.Created = DateTime.UtcNow;
            result.Modified = result.Created;
            return result;
        }

		/// <summary>
		/// Get the PropertyInfo that represents the parent object instance for the specified
		/// TypeFk
		/// </summary>
		/// <param name="typeFk"></param>
		/// <returns></returns>
		protected internal PropertyInfo GetParentDaoPropertyOfChildForTypeFk(TypeFk typeFk)
		{
			// ParentType.Name Of ForeignKeyProperty.Name
			Type foreignKeyDaoType = GetDaoType(typeFk.ForeignKeyType);
			string propertyName = string.Format("{0}Of{1}", foreignKeyDaoType.Name, typeFk.ForeignKeyProperty.Name);
			PropertyInfo parentPropertyOfChildForTypeFk = foreignKeyDaoType.GetProperty(propertyName);
			return parentPropertyOfChildForTypeFk;
		}

		/// <summary>
		/// Get the PropertyInfo that represents the child collection for the specified 
		/// TypeFk
		/// </summary>
		/// <param name="typeFk"></param>
		/// <returns></returns>
		protected internal PropertyInfo GetChildCollectionDaoPropertyForTypeFk(TypeFk typeFk)
		{
			Type primaryDaoType = GetDaoType(typeFk.PrimaryKeyType);
			Type foreignKeyDaoType = GetDaoType(typeFk.ForeignKeyType);
			string propertyName = string.Format("{0}By{1}", foreignKeyDaoType.Name.Pluralize(), typeFk.ForeignKeyProperty.Name);
			PropertyInfo childCollectionPropertyForTypeFk = primaryDaoType.GetProperty(propertyName);
			return childCollectionPropertyForTypeFk;
		}

        protected Dao GetDaoInstanceById(Type baseOrWrapperType, long id)
        {
            return GetDaoInstanceByMethod("GetById", baseOrWrapperType, (object)id);
        }

        private object GetWrapperInstance(Type objectType, Dao daoInstance)
		{
			object result = ConstructWrapper(objectType);
			result.CopyProperties(daoInstance);
			SetParentProperties(result);

            return result;
		}

		private Dao GetDaoInstanceById<T>(long id) where T : new()
		{
			return GetDaoInstanceById(typeof(T), id);
		}

		private Dao GetDaoInstanceByUuid(Type baseOrWrapperType, string uuid)
		{
			return GetDaoInstanceByMethod("GetByUuid", baseOrWrapperType, (object)uuid);
		}

		private Dao GetDaoInstanceByMethod(string methodName, Type baseOrWrapperType, object parameter)
		{
			Type pocoType = GetBaseType(baseOrWrapperType);
			Type daoType = GetDaoType(pocoType);
			MethodInfo getterMethod = daoType.GetMethod(methodName, new Type[] { parameter.GetType(), typeof(Database) });
			object daoResult = getterMethod.Invoke(null, new object[] { parameter, Database });
			if (daoResult == null)
			{
				return null;
			}
			return (Dao)daoResult;
		}

		private void SaveDaoInstance<T>(object daoInstance) where T : new()
		{
			SaveDaoInstance(typeof(T), daoInstance);
		}

		protected void SaveDaoInstance(Type dtoOrPocoType, object daoInstance)
		{
			MethodInfo saveMethod = GetDaoType(dtoOrPocoType).GetMethod("Save", new Type[] { typeof(Database) });
            try
            {
                saveMethod.Invoke(daoInstance, new object[] { Database });
            }
            catch (Exception ex)
            {
                LogAndThrow(ex, Logger);
            }
		}

		private Type GetDaoType<T>() where T : new()
		{
			return GetDaoType(typeof(T));
		}

		private void SetXrefDaoCollectionValues(object pocoOrPocoParent, Dao daoInstance, PropertyInfo daoXrefProperty, PropertyInfo pocoProperty)
		{
			IEnumerable values = (IEnumerable)pocoProperty.GetValue(pocoOrPocoParent);
			if (values != null)
			{
				IAddable xrefDaoCollection = (IAddable)daoXrefProperty.GetValue(daoInstance);
				foreach (object o in values)
				{
					Meta.SetUuid(o);
                    Meta.SetCuid(o);
					Dao dao = GetDaoType(o.GetType()).Construct<Dao>();
					dao.CopyProperties(o);
					xrefDaoCollection.Add(dao);
				}
			}
		}

		protected object SetDaoInstancePropertiesAndSave(object poco, Dao daoInstance)
		{
			Meta.SetUuid(poco);
            Meta.SetCuid(poco);
			daoInstance.CopyProperties(poco);
			daoInstance.Property("Database", Database);
			Type pocoType = GetBaseType(poco.GetType());
			SaveDaoInstance(pocoType, daoInstance);
            bool saveAgain = false;
			if (SetChildDaoCollectionValues(poco, daoInstance))
			{
                saveAgain = true;
			}
            if(SetXrefDaoCollectionValues(poco, daoInstance))
            {
                saveAgain = true;
            }
            if (saveAgain)
            {
                daoInstance.ForceUpdate = true;
                SaveDaoInstance(pocoType, daoInstance);
            }
			object dto = ConstructWrapper(pocoType);
			dto.CopyProperties(daoInstance);
			poco.CopyProperties(dto);
			return dto;
		}
		
        private void WarnRetrieveAll<T>() where T : class, new()
        {
            Type type = typeof(T);
            WarnRetrieveAll(type);
        }

        private void WarnRetrieveAll(Type type)
        {
            string msgSignature = "Use of this method ({0}) can have a potential detrimental performance hit because it will retrieve all records for type ({1})";
            string[] messageArgs = new string[] { "RetrieveAll", type.Name };
            Logger.AddEntry(msgSignature, LogEventType.Warning, messageArgs);
            if (WarningsAsErrors)
            {
                string message = string.Format(msgSignature, messageArgs);
                message = string.Format("{0}\r\n\r\nTo suppress this exception set WarningsAsErrors to false", message);
                Args.Throw<InvalidOperationException>(message);
            }
        }
        
        private static QueryFilter CreateQueryFilter(Dictionary<string, object> queryParameters)
        {
            Args.ThrowIf<ArgumentException>(queryParameters.Count == 0, "No query parameters specified");

            string first = queryParameters.Keys.First();
            QueryFilter filter = new QueryFilter(first) == queryParameters[first];
            if (queryParameters.Keys.Count > 1)
            {
                queryParameters.Keys.Rest(1, property =>
                {
                    filter.And(new QueryFilter(property) == queryParameters[property]);
                });
            }
            return filter;
        }

        private static void LogAndThrow(Exception ex, ILogger logger)
        {
            logger = logger ?? Log.Default;
            string message = ex.Message;
            string innerMessage = "NA";
            string signature = "ExceptionMessage::{0}::InnerExceptionMessage::{1}";
            Exception e = ex;            
            if(ex.InnerException != null)
            {
                e = ex.InnerException;
                innerMessage = ex.InnerException.Message;
            }
            logger.AddEntry(signature, e, message, innerMessage);
        }

	}
}
