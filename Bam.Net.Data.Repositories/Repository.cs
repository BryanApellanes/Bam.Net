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
	public abstract class Repository : Loggable, IRepository
	{
		public Repository()
		{
			_storableTypes = new HashSet<Type>();
            RequireUuid = true;
            RequireCuid = false;
        }

        public bool RequireUuid { get; set; }
        public bool RequireCuid { get; set; }

        #region IRepository Members

        HashSet<Type> _storableTypes;

		public IEnumerable<Type> StorableTypes
		{
			get
			{
				return _storableTypes;
			}
		}

		public virtual void AddType<T>()
		{
			AddType(typeof(T));
		}

		public void AddTypes(IEnumerable<Type> types)
		{
			types.Each(type =>
			{
				AddType(type);
			});
		}

		public virtual void AddType(Type type)
		{
			_storableTypes.Add(type);
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
			return (T)Save((object)toSave);
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
        /// <summary>
        /// Calls update for the specified object toSave if
        /// it has Id greater than 0 otherwise calls Create
        /// </summary>
        /// <param name="toSave"></param>
        /// <returns></returns>
        public object Save(object toSave)
		{
            SetMeta(toSave);
			long id = GetIdValue(toSave);
			object result = null;
			if (id > 0)
			{
				result = Update(toSave);
			}
			else
			{
				result = Create(toSave);
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
        public abstract T Create<T>(T toCreate) where T : class, new();

		public abstract object Create(object toCreate);

        public abstract T Retrieve<T>(int id) where T : class, new();
		
		public abstract T Retrieve<T>(long id) where T : class, new();
        public abstract T Retrieve<T>(string uuid) where T : class, new();
		public abstract IEnumerable<T> RetrieveAll<T>() where T : class, new();
		public abstract IEnumerable<object> RetrieveAll(Type type);
		public abstract object Retrieve(Type objectType, long id);
		public abstract object Retrieve(Type objectType, string uuid);
        public abstract IEnumerable<object> Query(string propertyName, object propertyValue);
		public abstract IEnumerable<object> Query(dynamic query);
        public abstract IEnumerable<T> Query<T>(Func<T, bool> query) where T : class, new();
        public abstract IEnumerable<T> Query<T>(Dictionary<string, object> queryParameters) where T : class, new();
        public abstract IEnumerable Query(Type type, Dictionary<string, object> queryParameters);
		public abstract IEnumerable<object> Query(Type type, Func<object, bool> predicate);
		public abstract IEnumerable<T> Query<T>(dynamic query) where T : class, new();
		public abstract T Update<T>(T toUpdate) where T : new();
		public abstract object Update(object toUpdate);
		public abstract bool Delete<T>(T toDelete) where T : new();
		public abstract bool Delete(object toDelete);
		#endregion

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

		protected internal static long GetIdValue(object value)
		{
			return Meta.GetId(value);
		}

        public abstract IEnumerable<T> Query<T>(QueryFilter query) where T : class, new();

        public abstract IEnumerable Query(Type type, QueryFilter query);
    }
}
