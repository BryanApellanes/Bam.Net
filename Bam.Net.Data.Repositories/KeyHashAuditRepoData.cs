using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// The base class to extend for any class whose identity is determined
    /// by multiple properties addorned with CompositeKeyAttribute.
    /// </summary>
    /// <seealso cref="Bam.Net.Data.Repositories.AuditRepoData" />
    /// <seealso cref="Bam.Net.Data.Repositories.IHasKeyHash" />
    [Serializable]
    public abstract class KeyHashAuditRepoData : AuditRepoData, IHasKeyHash
    {
        public KeyHashAuditRepoData()
        {
            PropertyDelimiter = "\r\n";
        }

        string[] _compositeKeyProperties;
        public virtual string[] CompositeKeyProperties
        {
            get
            {
                if (_compositeKeyProperties == null)
                {
                    _compositeKeyProperties = KeyHashProvider.GetCompositeKeyProperties(GetType());
                }
                return _compositeKeyProperties;
            }
            set
            {
                _compositeKeyProperties = value;
            }
        }

        public int GetIntKeyHash()
        {
            return KeyHashProvider.GetIntKeyHash(this, PropertyDelimiter, CompositeKeyProperties);
        }

        public long GetLongKeyHash()
        {
            return KeyHashProvider.GetLongKeyHash(this, PropertyDelimiter, CompositeKeyProperties);
        }

        public ulong GetULongKeyHash()
        {
            return KeyHashProvider.GetULongKeyHash(this, PropertyDelimiter, CompositeKeyProperties);
        }

        protected string PropertyDelimiter { get; set; }

        public override int GetHashCode()
        {
            return GetIntKeyHash();
        }

        public override bool Equals(object obj)
        {
            if (obj is KeyHashAuditRepoData o)
            {
                return o.GetHashCode().Equals(GetHashCode());
            }
            return false;
        }
    }
}
