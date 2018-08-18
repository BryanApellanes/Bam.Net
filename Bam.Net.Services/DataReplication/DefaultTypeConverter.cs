using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public class DefaultTypeConverter : ITypeConverter
    {
        public object ChangeType(object value, Type changeTo)
        {
            return Convert.ChangeType(value, changeTo);
        }
    }
}
