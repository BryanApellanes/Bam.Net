using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.DataReplication
{
    public interface ITypeConverter
    {
        object ChangeType(object value, Type changeTo);
    }
}
