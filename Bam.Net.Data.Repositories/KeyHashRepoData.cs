using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    [Serializable]
    public abstract class KeyHashRepoData : RepoData, IHasKeyHash
    {
        public KeyHashRepoData()
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

        protected string PropertyDelimiter { get; set; }

        public override int GetHashCode()
        {
            return GetIntKeyHash();
        }

        public override bool Equals(object obj)
        {
            KeyHashRepoData o = obj as KeyHashRepoData;
            if (o != null)
            {
                return o.GetHashCode().Equals(GetHashCode());
            }
            return false;
        }

        public virtual T Save<T>(IRepository repo) where T : RepoData, new()
        {
            T existing = QueryFirstOrDefault<T>(repo, CompositeKeyProperties);
            if(existing == null)
            {
                RepoData data = (RepoData)repo.Save((object)this);
                existing = repo.Retrieve<T>(data.Uuid);
            }
            else
            {
                existing = repo.Retrieve<T>(existing.Uuid);
            }
            return existing;
        }
    }
}
