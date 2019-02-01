using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.Reflection;
using System.Collections;

namespace Bam.Net.Services.DataReplication.Data
{
    [Serializable]
    public class DataRelationship: RepoData
    {
        public string LeftCuid { get; set; }
        public string RightCuid { get; set; }        
        public  string RelationshipDescription { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is DataRelationship other)
            {
                return (other.LeftCuid.Equals(LeftCuid) && other.RightCuid.Equals(RightCuid)) ||
                    (other.LeftCuid.Equals(RightCuid) && other.RightCuid.Equals(LeftCuid));
            }
            return false;
        }
        public override int GetHashCode()
        {
            List<string> cuids = new List<string> { LeftCuid, RightCuid };
            cuids.Sort();
            return $"{cuids[0]}.{cuids[1]}".GetHashCode();
        }
        public static HashSet<DataRelationship> FromInstance(object instance)
        {
            Type instanceType = instance.GetType();
            TypeSchema typeSchema = new TypeSchemaGenerator().CreateTypeSchema(new[] { instanceType });
            HashSet<DataRelationship> relationships = new HashSet<DataRelationship>();
            string instanceCuid = instance.Property<string>("Cuid");
            typeSchema.ForeignKeys.Where(fk => fk.PrimaryKeyType == instanceType).Each(fk =>
            {
                foreach (object referencer in (IEnumerable)fk.CollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = instanceCuid, RightCuid = referencer.Property<string>("Cuid"), RelationshipDescription = $"{fk.ForeignKeyType.Namespace}.{fk.ForeignKeyType.Name}->{fk.PrimaryKeyType.Namespace}.{fk.PrimaryKeyType.Name}" });
                }
            });
            typeSchema.Xrefs.Where(x => x.Left == instanceType).Each(x=>
            {
                foreach(object rightInstance in (IEnumerable)x.RightCollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = instanceCuid, RightCuid = rightInstance.Property<string>("Cuid"), RelationshipDescription = $"{x.Left.Namespace}.{x.Left.Name}<->{x.Right.Namespace}.{x.Right.Name}" });
                }
            });
            typeSchema.Xrefs.Where(x => x.Right == instanceType).Each(x =>
            {
                foreach (object leftInstance in (IEnumerable)x.LeftCollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = leftInstance.Property<string>("Cuid"), RightCuid = instanceCuid, RelationshipDescription = $"{x.Left.Namespace}.{x.Left.Name}<->{x.Right.Namespace}.{x.Right.Name}" });
                }
            });
            return relationships;
        }
    }
}
