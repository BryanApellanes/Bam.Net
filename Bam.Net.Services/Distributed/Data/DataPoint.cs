using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Distributed.Data
{
    [Serializable]
    public class DataPoint: AuditRepoData
    {
        public DataPoint()
        {
            DataPropertyCollection = new DataPropertyCollection();
        }
        public string Description { get; set; }
        public string DataProperties
        {
            get
            {
                return DataPropertyCollection.ToBinaryBytes().ToBase64();
            }
            set
            {
                DataPropertyCollection = value.FromBase64().FromBinaryBytes<DataPropertyCollection>();
            }
        }
        protected internal DataPropertyCollection DataPropertyCollection { get; set; }

        public DataProperty Property(string name, object value = null)
        {
            DataProperty prop;
            DataPropertyCollection.Prop(name, value, out prop);
            return prop;
        }

        public T Property<T>(string name)
        {
            return (T)Property(name, null).Value;
        }
    }
}
