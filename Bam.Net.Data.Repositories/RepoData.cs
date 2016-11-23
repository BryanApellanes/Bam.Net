/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCuid;
using Bam.Net.Data;
using Bam.Net.Data.Dynamic;

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
        public RepoData()
        {
            Created = DateTime.UtcNow;
        }
		public long Id { get; set; }
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
        public string CreatedBy { get; set; }
		public DateTime? Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public DateTime? Deleted { get; set; }
        /// <summary>
        /// Ensure the current RepoData instance has been 
        /// persisted to the specified repo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <returns></returns>
        public T EnsurePersisted<T>(IRepository repo) where T: RepoData, new()
        {
            T instance = repo.Retrieve<T>(Uuid);
            if(instance == null)
            {
                instance = repo.Save((T)this);
            }
            return instance;
        }

        /// <summary>
        /// Ensures that an instance of the current RepoData
        /// has been saved to the specified repo where the 
        /// specified properties equal the values of those
        /// properties on this instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public T EnsureSingle<T>(IRepository repo, params string[] propertyName) where T : RepoData, new()
        {
            return EnsureSingle<T>(repo, "Anonymous", propertyName);
        }

        /// <summary>
        /// Ensures that an instance of the current RepoData
        /// has been saved to the specified repo where the 
        /// specified properties equal the values of those
        /// properties on this instance.  Will cause the 
        /// Id of this instance to be reset if a representative
        /// value is not found in the repo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public T EnsureSingle<T>(IRepository repo, string modifiedBy, params string[] propertyNames) where T: RepoData, new()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            propertyNames.Each(new { Parameters = parameters, Instance = this }, (ctx, pn) =>
            {
                ctx.Parameters.Add(pn, Reflect.Property(ctx.Instance, pn));
            });
            T instance = repo.Query<T>(parameters).FirstOrDefault();
            if(instance == null) // wasn't saved/found, should reset Id so the repo will Create
            {
                Id = -1;
                ModifiedBy = modifiedBy;
                Modified = DateTime.UtcNow;
                instance = repo.Save((T)this);
            }
            return instance;
        }

        private void ValidatePropertyNamesOrDie(params string[] propertyNames)
        {
            propertyNames.Each(new { Instance = this }, (ctx, pn) =>
            {
                Args.ThrowIf(!Reflect.HasProperty(ctx.Instance, pn), "Specified property ({0}) was not found on instance of type ({1})", pn, ctx.Instance.GetType().Name);
            });
        }
	}
}
