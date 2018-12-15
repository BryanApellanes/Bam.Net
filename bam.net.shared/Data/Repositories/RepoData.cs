using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An abstract base class defining common
    /// properties for any object you may wish to 
    /// save in a Repository
    /// </summary>
    [Serializable]
	public abstract class RepoData
	{
		public ulong Id { get; set; }
        private DateTime? _created;

        /// <summary>
        /// The time that the Created property
        /// was first referenced prior to persisting
        /// the object instance
        /// </summary>
        public DateTime? Created
        {
            get
            {
                if (_created == null)
                {
                    _created = DateTime.UtcNow;
                }
                return _created;
            }
            set { _created = value; }
        }
        string _uuid;
        public string Uuid
        {
            get
            {
                if (string.IsNullOrEmpty(_uuid))
                {
                    _uuid = Guid.NewGuid().ToString();
                }
                return _uuid;
            }
            set
            {
                _uuid = value;
            }
        }
        string _cuid;
        public string Cuid
        {
            get
            {
                if (string.IsNullOrEmpty(_cuid))
                {
                    _cuid = NCuid.Cuid.Generate();
                }
                return _cuid;
            }
            set
            {
                _cuid = value;
            }
        }

        /// <summary>
        /// Does a query for an instance of the specified
        /// generic type T having properties who's values
        /// match those of the current instance; may return null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual T QueryFirstOrDefault<T>(IRepository repo, params string[] propertyNames) where T : RepoData, new()
        {
            ValidatePropertyNamesOrDie(propertyNames);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            propertyNames.Each(new { Parameters = parameters, Instance = this }, (ctx, pn) =>
            {
                ctx.Parameters.Add(pn, ReflectionExtensions.Property(ctx.Instance, pn));
            });
            T instance = repo.Query<T>(parameters).FirstOrDefault();
            return instance;
        }

        public override bool Equals(object obj)
        {
            if (obj is RepoData o)
            {
                return o.Uuid.Equals(Uuid) && o.Cuid.Equals(Cuid);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Uuid.GetHashCode() + Cuid.GetHashCode();
        }

        public virtual RepoData Save(IRepository repo)
        {
            return (RepoData)repo.Save((object)this);
        }

        public bool GetIsPersisted()
        {
            return IsPersisted;
        }

        public bool GetIsPersisted(out IRepository repo)
        {
            repo = Repository;
            return IsPersisted;
        }

        protected void ValidatePropertyNamesOrDie(params string[] propertyNames)
        {
            propertyNames.Each(new { Instance = this }, (ctx, pn) =>
            {
                Args.ThrowIf(!ReflectionExtensions.HasProperty(ctx.Instance, pn), "Specified property ({0}) was not found on instance of type ({1})", pn, ctx.Instance.GetType().Name);
            });
        }
        
        protected internal bool IsPersisted { get; set; }
        protected internal IRepository Repository { get; set; } // gets set by Repository.Save
    }
}
