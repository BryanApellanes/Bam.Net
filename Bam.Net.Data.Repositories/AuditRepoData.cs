using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
<<<<<<< Updated upstream
    public abstract class AuditRepoData: RepoData
    {
        public AuditRepoData() : base()
=======
	public abstract class AuditRepoData: RepoData
	{
        public AuditRepoData() : base()
        {
            Created = DateTime.UtcNow;
        }

        public DateTime? Created { get; set; }
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
>>>>>>> Stashed changes
        {
            Modified = Created;
        }
        public DateTime? Modified { get; set; }

        public string ModifiedBy { get; set; }
    }
}
