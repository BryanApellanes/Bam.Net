using Bam.Net.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class ObjectIdentifier<T>: ObjectIdentifier where T: RepoData
    {
        public override Type Type
        {
            get { return typeof(T); }
            set { }
        }
    }

    public class ObjectIdentifier
    {
        public ObjectIdentifier()
        {
            Uuid = Guid.NewGuid().ToString();
            Cuid = NCuid.Cuid.Generate();
        }

        Type _type;
        public virtual Type Type
        {
            get { return _type; }
            set
            {
                _type = value;
                _typeName = _type?.Name;
            }
        }
        string _typeName;
        public virtual string TypeName
        {
            get { return _typeName; }
            set
            {
                _typeName = value;
                _type = Type.GetType(_typeName);
            }
        }
        public ulong Id { get; set; }
        public string Uuid { get; set; }
        public string Cuid { get; set; }

        public dynamic ToInstance(dynamic example)
        {
            Args.ThrowIfNull(Type);
            object instance = Type.Construct();
            ObjectIdentifier id = new ObjectIdentifier();
            ReflectionExtensions.Property(instance, "Uuid", id.Uuid, false);
            ReflectionExtensions.Property(instance, "Cuid", id.Cuid, false);
            Bam.Net.Extensions.CopyProperties(instance, example);
            return instance;
        }

        public dynamic RetrieveInstance(IRepository repo)
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
