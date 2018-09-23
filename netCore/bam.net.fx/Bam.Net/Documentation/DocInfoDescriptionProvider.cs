using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation
{
    public class DocInfoDescriptionProvider : IDescriptionProvider
    {
        public string GetMethodDescription(MethodInfo method)
        {
            return new DocInfo(method).Summary;
        }

        public string GetParameterDescription(ParameterInfo parameterInfo)
        {
            DocInfo methodDoc = new DocInfo((MethodInfo)parameterInfo.Member);
            ParamInfo paramInfo = methodDoc.ParamInfos.FirstOrDefault(pi => pi.Name.Equals(parameterInfo.Name));
            if (paramInfo != null)
            {
                return paramInfo.Description;
            }
            return string.Empty;
        }

        public string GetPropertyDescription(PropertyInfo propertyInfo)
        {
            return new DocInfo(propertyInfo).Summary;
        }

        public string GetTypeDescription(Type type)
        {
            return new DocInfo(type).Summary;
        }
    }
}
