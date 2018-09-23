using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation
{
    /// <summary>
    /// Provides descriptions for reflected MemberInfos
    /// </summary>
    public interface IDescriptionProvider
    {
        string GetTypeDescription(Type type);
        string GetMethodDescription(MethodInfo method);
        string GetParameterDescription(ParameterInfo parameterInfo);
        string GetPropertyDescription(PropertyInfo propertyInfo);
    }
}
