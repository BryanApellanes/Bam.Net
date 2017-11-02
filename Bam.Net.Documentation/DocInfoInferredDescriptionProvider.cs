using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Documentation
{
    /// <summary>
    /// Gets member descriptions from DocInfoAttributes or xml documentation if
    /// it exists
    /// </summary>
    public class DocInfoInferredDescriptionProvider : IDescriptionProvider
    {
        static Dictionary<string, List<DocInfo>> _docInfosByTypeName;
        static DocInfoInferredDescriptionProvider()
        {
            _docInfosByTypeName = new Dictionary<string, List<DocInfo>>();
        }

        public DocInfoInferredDescriptionProvider()
        {            
        }

        public string GetMethodDescription(MethodInfo method)
        {
            Type type = method.DeclaringType;
            DocInfo.AddDocInfos(_docInfosByTypeName, type);
            string memberName = $"{type.Namespace}.{type.Name}.{method.Name}";
            DocInfo docInfo = _docInfosByTypeName[method.DeclaringType.AssemblyQualifiedName].FirstOrDefault(di => di.MemberType == ClassMemberType.Method && di.MemberName.Equals(memberName));
            if(docInfo != null)
            {
                return docInfo.Summary;
            }
            return string.Empty;
        }

        public string GetParameterDescription(ParameterInfo parameterInfo)
        {
            MemberInfo method = parameterInfo.Member;
            Type type = method.DeclaringType;
            DocInfo.AddDocInfos(_docInfosByTypeName, type);
            string memberName = $"{type.Namespace}.{type.Name}.{method.Name}";
            DocInfo docInfo = _docInfosByTypeName[method.DeclaringType.AssemblyQualifiedName].FirstOrDefault(di => di.MemberType == ClassMemberType.Method && di.MemberName.Equals(memberName));
            if(docInfo != null)
            {
                return docInfo.Summary;
            }
            return string.Empty;
        }

        public string GetPropertyDescription(PropertyInfo propertyInfo)
        {
            Type type = propertyInfo.DeclaringType;
            DocInfo.AddDocInfos(_docInfosByTypeName, type);
            string memberName = $"{type.Namespace}.{type.Name}.{propertyInfo.Name}";
            DocInfo docInfo = _docInfosByTypeName[type.AssemblyQualifiedName].FirstOrDefault(di => di.MemberType == ClassMemberType.Method && di.MemberName.Equals(memberName));
            if (docInfo != null)
            {
                return docInfo.Summary;
            }
            return string.Empty;
        }

        public string GetTypeDescription(Type type)
        {
            DocInfo.AddDocInfos(_docInfosByTypeName, type);
            string memberName = $"{type.Namespace}.{type.Name}";
            DocInfo docInfo = _docInfosByTypeName[type.AssemblyQualifiedName].FirstOrDefault(di => di.MemberType == ClassMemberType.Method && di.MemberName.Equals(memberName));
            if (docInfo != null)
            {
                return docInfo.Summary;
            }
            return string.Empty;
        }
    }
}
