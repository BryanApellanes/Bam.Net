using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Distributed.Data
{
    [Serializable]
    public class DataProperty
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is DataProperty dataProp)
            {
                return dataProp.Name.Equals(Name) && Value.Equals(dataProp.Value);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.GetHashCode(Name, Value);
        }
    }
}
