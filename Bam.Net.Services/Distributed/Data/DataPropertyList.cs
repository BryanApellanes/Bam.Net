using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Distributed.Data
{
    [Serializable]
    public class DataPropertyList: List<DataProperty>
    {
        public DataPropertyList Add(string name, object value)
        {
            DataProperty ignore;
            return Add(name, value, out ignore);
        }
        public DataPropertyList Add(string name, object value, out DataProperty dataProperty)
        {
            dataProperty = new DataProperty { Name = name, Value = value };
            Add(dataProperty);
            return this;
        }
    }
}
