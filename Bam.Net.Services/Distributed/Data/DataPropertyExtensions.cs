using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.Services.Distributed.Data
{
    public static class DataPropertyExtensions
    {
        public static DataPropertyCollection ToDataPropertyCollection(this object instance)
        {
            return DataPropertyCollection.FromInstance(instance);
        }

        public static DataPropertyCollection GetUpdates(this Dao dao)
        {
            DataPropertyCollection result = new DataPropertyCollection();
            dao.GetNewAssignValues().Each(av => result.Add(av.ColumnName, av.Value));
            return result;
        }

        public static T ToInstance<T>(this DataPropertyCollection propertyCollection) where T: class, new()
        {
            T result = new T();
            propertyCollection.Each(dp => result.Property(dp.Name, dp.Value));
            return result;
        }
    }
}
