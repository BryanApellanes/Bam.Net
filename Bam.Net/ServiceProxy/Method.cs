using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy
{
    public static class Method
    {
        public static bool WillProxy(this MethodInfo method, bool includeLocal = false)
        {
            bool baseCheck = !method.Name.StartsWith("remove_") &&
                                   !method.Name.StartsWith("add_") &&
                                   method.MemberType == MemberTypes.Method &&
                                   !method.IsProperty() &&
                                   method.DeclaringType != typeof(object);
            bool hasExcludeAttribute = method.HasCustomAttributeOfType(out ExcludeAttribute attr);
            bool result = false;
            if (includeLocal)
            {
                result = hasExcludeAttribute ? (attr is LocalAttribute && baseCheck) : baseCheck;
            }
            else
            {
                result = hasExcludeAttribute ? false : baseCheck;
            }
            return result;
        }
    }
}
