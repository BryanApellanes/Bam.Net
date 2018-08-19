using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class InvalidIdPropertyTypeException: Exception
    {
        public InvalidIdPropertyTypeException(PropertyInfo prop) : base($"Id property {prop.DeclaringType.Name}.{prop.Name} is of type {prop.PropertyType.Name} but should ulong") { }
    }
}
