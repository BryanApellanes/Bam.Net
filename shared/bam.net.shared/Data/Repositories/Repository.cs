/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.Logging;
using System.Collections;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
	public abstract class Repository : Loggable, IRepository
	{
		public Repository()
		{
			_storableTypes = new HashSet<Type>();
            RequireUuid = true;
            RequireCuid = false;
            RepoDataHydrator = Repositories.RepoDataHydrator.DefaultRepoDataHydrator;
        }
        
        public bool RequireUuid { get; set; }
        public bool RequireCuid { get; set; }

        public IRepoDataHydrator RepoDataHydrator { get; set; }

        #region IRepository Members

        Type _defaultType;
        /// <summary>
        /// If a non typed query is executed the DefaultType
        /// can be used by specific Repository implementations
        /// to advise which Type to query and return
        /// </summary>
        public Type DefaultType
        {
            get
            {
                if (_defaultType == null)
                {
                    _defaultType = StorableTypes.FirstOrDefault();
                }
                return _defaultType;
            }
            set
            {
                _defaultType = value;
            }
        }
        HashSet<Type> _storableTypes;

		public IEnumerable<Type> StorableTypes
		{
			get
			{
				return _storableTypes;
			}
		}

        public virtual bool TryHydrate(RepoData data)
        {
            return RepoDataHydrator?.TryHydrate(data, this) ?? true;
        }

        /// <summary>
        /// Add the specified type as a type that
        /// can be persisted by this repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
		public virtual void AddType<T>()
		{
			this.AddType(typeof(T));
		}

        public void AddTypes(params Type[] types)
        {
            types.Each(type => AddType(type));
        }

        /// <summary>
        /// Add the specified types as types that
        /// can be persisted by this repository
        /// </summary>
        /// <param name="types"></param>
		public void AddTypes(IEnumerable<Type> types)
		{
            types.Each(type => AddType(type));
        }

        /// <summary>
        /// Add the specified type as a type that
        /// can be persisted by this repository
        /// </summary>
        /// <param name="type"></param>
		public virtual void AddType(Type type)
		{
			_storableTypes.Add(type);
            if(!_storableTypes.Contains(typeof(CompositeKeyMap)) && type.GetProperties().Where(pi=> pi.HasCustomAttributeOfType<CompositeKeyAttribute>()).Any())
            {
                _storableTypes.Add(typeof(CompositeKeyMap));
            }
		}

        /// <summary>
        /// Add all the types from the specified assembly
        /// that are in the specified nameSpace
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="nameSpace"></param>
		public void AddNamespace(Assembly assembly, string nameSpace)
		{
			AddTypes(assembly.GetTypes().Where(t => t.Namespace != null && t.Namespace.Equals(nameSpace) && !t.IsAbstract));
		}

        public Task<T> SaveAsync<T>(T toSave) where T : class, new()
        {
            return Task.Run(() => Save<T>(toSave));
        }

        public T Save<T>(T toSave) where T : class, new()
		{
			return (T)Save((object)toSave); // casting so that the implementation uses the actual type and not a base type
		}

        /// <summary>
        /// Add all the types in the same namespace
        /// as the specified type
        /// </summary>
        /// <param name="type"></param>
        public void AddNamespace(Type type)
        {
            AddNamespace(type.Assembly, type.Namespace);
        }

        public object Save(object toSave)
        {
            Args.ThrowIfNull(toSave, "toSave");
            return Save(toSave.GetType(), toSave);
        }

        /// <summary>
        /// The event that fires before an update is 
        /// made by calling Save.  Will not fire on
        /// calls direct to Update
        /// </summary>
        public event EventHandler Updating;
        /// <summary>
        /// The event that fires after an update is
        /// made by calling Save.  Will not fire on
        /// calls direct to Update
        /// </summary>
        public event EventHandler Updated;
        /// <summary>
        /// The event that fires before create is called by
        /// Save.  Will not fire on
        /// calls direct to Update
        /// </summary>
        public event EventHandler Creating;

        /// <summary>
        /// The event that fires after create is called by
        /// Save.  Will not fire on
        /// calls direct to Update
        /// </summary>
        public event EventHandler Created;

        /// <summary>
        /// Calls update for the specified object toSave if
        /// it has Id greater than 0 otherwise calls Create
        /// </summary>
        /// <param name="toSave"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object Save(Type type, object toSave)
		{
            SetMeta(toSave);
			ulong? id = GetIdValue(toSave);
            toSave.Property("Modified", DateTime.UtcNow, false);
			object result = null;
			if (id.HasValue && id.Value != 0)
			{
                FireEvent(Updating, new RepositoryEventArgs(toSave, type));                
				result = Update(type, toSave);
                FireEvent(Updated, new RepositoryEventArgs(result, type));
            }
			else
			{
                FireEvent(Creating, new RepositoryEventArgs(toSave, type));
				result = Create(type, toSave);
                FireEvent(Created, new RepositoryEventArgs(result, type));
			}

            if(result is RepoData data)
            {
                data.IsPersisted = true;
                data.Repository = this;
                return data;
            }

			return result;
		}
        public virtual IEnumerable SaveCollection(IEnumerable values)
        {
            foreach(object value in values)
            {
                yield return Save(value);
            }
        }
        public virtual IEnumerable<T> SaveCollection<T>(IEnumerable<T> values) where T : class, new()
        {
            foreach(T value in values)
            {
                yield return Save<T>(value);
            }
        }

        public virtual object Retrieve(string typeIdentifier, string instanceIdentifier)
        {
            Type type = Type.GetType(typeIdentifier, true);
            Args.ThrowIfNull(type, "type");
            return Retrieve(type, instanceIdentifier);
        }

        public abstract T Create<T>(T toCreate) where T : class, new();

		public abstract object Create(object toCreate);
        public abstract object Create(Type type, object toCreate);
        public abstract T Retrieve<T>(int id) where T : class, new();
		
		public abstract T Retrieve<T>(long id) where T : class, new();
        public abstract T Retrieve<T>(ulong id) where T : class, new();
        public abstract T Retrieve<T>(string uuid) where T : class, new();
		public abstract IEnumerable<T> RetrieveAll<T>() where T : class, new();
		public abstract IEnumerable<object> RetrieveAll(Type type);
        public abstract void BatchRetrieveAll(Type dtoOrPocoType, int batchSize, Action<IEnumerable<object>> processor);
        public abstract object Retrieve(Type objectType, long id);
        public abstract object Retrieve(Type objectType, ulong id);
        public abstract object Retrieve(Type objectType, string uuid);
        public abstract IEnumerable<object> Query(string propertyName, object propertyValue);
        /// <summary>
        /// Execute query against the underlying SourceRepository.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> Query(dynamic query)
        {
            Type type = ReflectionExtensions.Property(query, "Type", false);
            if (type == null)
            {
                throw new InvalidOperationException("Type not specified, use { Type = typeof(<typeToQuery>) }");
            }
            Dictionary<string, object> parameters = Bam.Net.Extensions.ToDictionary(query);
            parameters.Remove("Type");
            return Query(type, parameters);
        }
        public abstract IEnumerable<T> Query<T>(Func<T, bool> query) where T : class, new();
        public abstract IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters) where T : class, new();
        public abstract IEnumerable<object> Query(Type type, Dictionary<string, object> queryParameters);
		public abstract IEnumerable<object> Query(Type type, Func<object, bool> predicate);
		public virtual IEnumerable<T> Query<T>(dynamic query) where T : class, new()
        {
            IEnumerable<object> results = Query(typeof(T), Bam.Net.Extensions.ToDictionary(query));
            return results.CopyAs<T>();
        }
        public virtual IEnumerable<object> Query(Type type, dynamic query)
        {
            return Query(type, Extensions.ToDictionary(query));
        }
		public abstract T Update<T>(T toUpdate) where T : new();
		public abstract object Update(object toUpdate);
        public abstract object Update(Type type, object toUpdate);
		public abstract bool Delete<T>(T toDelete) where T : new();
		public abstract bool Delete(object toDelete);
        public abstract bool Delete(Type type, object toDelete);
		#endregion
        public virtual bool DeleteWhere<T>(dynamic filter)
        {
            return DeleteWhere(typeof(T), filter);
        }
        public virtual bool DeleteWhere(Type type, dynamic filter)
        {
            return DeleteWhere(type, filter, out int count);
        }
        public virtual bool DeleteWhere(Type type, dynamic filter, out int count)
        {
            try
            {
                IEnumerable<object> toDelete = Query(type, filter);
                count = 0;
                if(toDelete != null)
                {
                    object[] arr = toDelete.ToArray();
                    count = arr.Length;
                    arr.Each(o => Delete(o));
                }
                return count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                count = -1;
                Logger.AddEntry("Error deleting values of type {0}: \r\nfilter={1}:: {2}", ex, type.Name, filter?.PropertiesToString(), ex.Message);
                return false;
            }
        }
        public ILogger Logger { get; set; }

		public Exception LastException { get; protected set; }

		[Verbosity(VerbosityLevel.Error)]
		public event EventHandler CreateFailed;

		protected void OnCreateFailed(EventArgs args)
		{
			FireEvent(CreateFailed, args);
		}

		[Verbosity(VerbosityLevel.Error)]
		public event EventHandler RetrieveFailed;

		protected void OnRetrieveFailed(EventArgs args)
		{
			FireEvent(RetrieveFailed, args);
		}

		[Verbosity(VerbosityLevel.Error)]
		public event EventHandler UpdateFailed;

		protected void OnUpdateFailed(EventArgs args)
		{
			FireEvent(UpdateFailed, args);
		}

		[Verbosity(VerbosityLevel.Error)]
		public event EventHandler DeleteFailed;
		protected void OnDeleteFailed(EventArgs args)
		{
			FireEvent(DeleteFailed, args);
		}

        protected void SetMeta(object instance)
        {
            if (RequireUuid)
            {
                Meta.SetUuid(instance);
            }
            if (RequireCuid)
            {
                Meta.SetCuid(instance);
            }
        }

        protected internal static PropertyInfo GetKeyProperty(Type type)
		{
			return Meta.GetKeyProperty(type);
		}

		protected internal static PropertyInfo GetKeyProperty<T>()
		{
			return GetKeyProperty(typeof(T));
		}

		protected internal ulong? GetIdValue(object value)
		{
			return Meta.GetId(value);
		}

        public abstract IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();

        public abstract IEnumerable<object> Query(Type type, QueryFilter query);
    }
}
