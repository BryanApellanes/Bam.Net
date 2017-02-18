using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using System.Reflection;
using System.Collections;

namespace Bam.Net.Services.Distributed.Data
{
    public class DataRelationship: RepoData
    {
        public string LeftCuid { get; set; }
        public string RightCuid { get; set; }
        
        public  string RelationshipDescription { get; set; }

        public static List<DataRelationship> FromInstance(object instance, DaoRepository repo)
        {
            TypeSchema typeSchema = repo.TypeSchema;
            List<DataRelationship> relationships = new List<DataRelationship>();
            Type instanceType = instance.GetType();
            string instanceCuid = instance.Property<string>("Cuid");
            typeSchema.ForeignKeys.Where(fk => fk.PrimaryKeyType == instanceType).Each(fk =>
            {
                foreach (object referencer in (IEnumerable)fk.CollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = instanceCuid, RightCuid = referencer.Property<string>("Cuid"), RelationshipDescription = $"{instanceType.Name}->{fk.CollectionProperty.Name}" });
                }
            });
            typeSchema.Xrefs.Where(x => x.Left == instanceType).Each(x=>
            {
                foreach(object rightInsance in (IEnumerable)x.LeftCollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = instanceCuid, RightCuid = rightInsance.Property<string>("Cuid"), RelationshipDescription = $"{x.Left.Name}<->{x.Right.Name}" });
                }
            });
            typeSchema.Xrefs.Where(x => x.Right == instanceType).Each(x =>
            {
                foreach (object leftInstance in (IEnumerable)x.RightCollectionProperty.GetValue(instance))
                {
                    relationships.Add(new DataRelationship { LeftCuid = leftInstance.Property<string>("Cuid"), RightCuid = instanceCuid, RelationshipDescription = $"{x.Left.Name}<->{x.Right.Name}" });
                }
            });
            return relationships;
        }
    }
}
