using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.DataReplication
{
    public static class DataPropertyExtensions
    {
        public static DataPropertySet ToDataPropertyCollection(this object instance)
        {
            return DataPropertySet.FromInstance(instance);
        }

        public static TypeSchema TypeSchema(this object instance)
        {
            return new TypeSchemaGenerator().CreateTypeSchema(new[] { instance.GetType() });
        }

        public static DataPropertySet GetUpdates(this Bam.Net.Data.Dao dao)
        {
            DataPropertySet result = new DataPropertySet();
            dao.GetNewAssignValues().Each(av => result.Add(av.ColumnName, av.Value));
            return result;
        }

        public static T ToInstance<T>(this DataPropertySet propertyCollection) where T: class, new()
        {
            T result = new T();
            propertyCollection.Each(dp => result.Property(dp.Name, dp.Value));
            return result;
        }
    }
}
