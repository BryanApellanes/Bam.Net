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
            if(value == null)
            {
                return null;
            }

            if (value is string v)
            {
                if (string.IsNullOrEmpty(v))
                {
                    return null;
                }
            }

            if (changeTo.IsCompatibleWith(typeof(DateTime)))
            {
                if(DateTime.TryParse(value.ToString(), out DateTime result))
                {
                    return result;
                }
            }
            return Convert.ChangeType(value, changeTo);
        }
    }
}
