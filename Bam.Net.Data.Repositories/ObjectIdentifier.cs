using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class ObjectIdentifier
    {
        public ObjectIdentifier()
        {
            Uuid = Guid.NewGuid().ToString();
            Cuid = NCuid.Cuid.Generate();
        }

        Type _type;
        public Type Type
        {
            get { return _type; }
            set
            {
                _type = value;
                _typeName = _type?.Name;
            }
        }
        string _typeName;
        public string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
                _type = Type.GetType(_typeName);
            }
        }
        public long Id { get; set; }
        public string Uuid { get; set; }
        public string Cuid { get; set; }

        public dynamic NewObject(dynamic example)
        {
            Args.ThrowIfNull(Type);
            object instance = Type.Construct();
            ObjectIdentifier id = new ObjectIdentifier();
            ReflectionExtensions.Property(instance, "Uuid", id.Uuid, false);
            ReflectionExtensions.Property(instance, "Cuid", id.Cuid, false);
            Extensions.CopyProperties(instance, example);
            return instance;
        }

        public dynamic GetInstance(IRepository repo)
        {
            return repo.Retrieve(Type, Uuid);
        }

        public static ObjectIdentifier FromRepoData(RepoData data)
        {
            return new ObjectIdentifier
            {
                Id = data.Id,
                Uuid = data.Uuid,
                Cuid = data.Cuid,
                Type = data.GetType()
            };
        }
    }
}
