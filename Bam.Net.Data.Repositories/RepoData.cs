/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCuid;

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
            Modified = Created;
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
        public string ModifiedBy { get; set; }
		public DateTime? Created { get; set; }
		public DateTime? Modified { get; set; }
	}
}
