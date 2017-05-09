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
		public long Id { get; set; }
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

        public override bool Equals(object obj)
        {
            RepoData o = obj as RepoData;
            if(o != null)
            {
                return o.Id.Equals(Id) && o.Uuid.Equals(Uuid) && o.Cuid.Equals(Cuid);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() + Uuid.GetHashCode() + Cuid.GetHashCode();
        }
    }
}
