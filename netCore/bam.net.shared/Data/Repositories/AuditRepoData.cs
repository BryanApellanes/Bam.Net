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
//using Bam.Net.Data.Dynamic;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An abstract base class defining common
    /// properties for any object that is saved to
    /// a Repository including fields useful
    /// for auditing the modification of persisted
    /// data.
    /// </summary>
    [Serializable]
    public abstract class AuditRepoData: RepoData
    {
        public AuditRepoData() : base()
        {
            Created = DateTime.UtcNow;
        }
        
        public string CreatedBy { get; set; }		
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
        public T EnsurePersisted<T>(IRepository repo) where T: AuditRepoData, new()
        {
            T instance = repo.Retrieve<T>(Cuid);
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
        /// properties on this instance.  Will cause the 
        /// Id of this instance to be reset if a representative
        /// value is not found in the repo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repo"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public T EnsureSingle<T>(IRepository repo, string modifiedBy, params string[] propertyNames) where T: AuditRepoData, new()
        {
            T instance = QueryFirstOrDefault<T>(repo, propertyNames);
            if (instance == null) // wasn't saved/found, should reset Id so the repo will Create
            {
                Id = 0;
                ModifiedBy = modifiedBy;
                Modified = DateTime.UtcNow;
                instance = repo.Save((T)this);
            }
            return instance;
        }
	}
}
