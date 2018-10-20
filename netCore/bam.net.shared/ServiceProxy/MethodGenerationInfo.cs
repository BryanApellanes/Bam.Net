/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    /// <summary>
    /// Provides meta data to code generators about a
    /// particular method
    /// </summary>
    public class MethodGenerationInfo
    {
        public MethodGenerationInfo(MethodInfo method)
        {
            Method = method;
            UsingNamespaces = new HashSet<string>();
            KnownTypes = new HashSet<Type>
            {
                method.ReturnType
            };
            Parameters.Each(p =>
            {
                KnownTypes.Add(p.ParameterType);
                UsingNamespaces.Add(p.ParameterType.Namespace);
            });
            IsVoidReturn = method.ReturnType == typeof(void);
            if (!IsVoidReturn)
            {
                UsingNamespaces.Add(method.ReturnType.Namespace);
            }
            ReturnTypeCodeString = IsVoidReturn ? "void" : method.ReturnType.Name;
            if (method.ReturnType.HasGenericArguments(out Type[] genericTypesOfReturn))
            {
                string returnTypeName = method.ReturnType == typeof(int) || method.ReturnType == typeof(long) ? method.ReturnType.Name : method.ReturnType.Name.DropTrailingNonLetters();
                ReturnTypeCodeString = string.Format("{0}<{1}>", returnTypeName, genericTypesOfReturn.ToDelimited(t => t.ToTypeString()));
                genericTypesOfReturn.Each(t =>
                {
                    KnownTypes.Add(t);
                    UsingNamespaces.Add(t.Namespace);
                });
            }

            MethodSignature = Parameters.ToDelimited(p => string.Format("{0} {1}", p.ParameterType.ToTypeString(), p.Name.CamelCase())); // method signature
            ParameterInstances = Parameters.ToDelimited(p => p.Name.CamelCase());
        }
        public MethodInfo Method { get; set; }
        public ParameterInfo[] Parameters
        {
            get
            {
                return Method.GetParameters();
            }
        }
        public HashSet<string> UsingNamespaces { get; set; }
        public HashSet<Type> KnownTypes { get; set; }

        HashSet<Assembly> _referenceAssemblies;
        object _refAssLock = new object();
        public HashSet<Assembly> ReferenceAssemblies
        {
            get
            {
                return _refAssLock.DoubleCheckLock(ref _referenceAssemblies, () =>
                {
                    return new HashSet<Assembly>(KnownTypes.Select(t => t.Assembly));
                });
            }
        }
        public bool IsVoidReturn { get; private set; }
        public string ReturnTypeCodeString { get; private set; }
        public string MethodSignature { get; private set; }
        public string ParameterInstances { get; private set; }
    }
}
