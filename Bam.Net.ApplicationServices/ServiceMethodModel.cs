/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.Razor;

namespace Bam.Net.ApplicationServices
{
    public class ServiceMethodModel
    {
        public ServiceMethodModel(MethodInfo method, params Assembly[] assembliesToreference)
            : this(new MethodGenerationInfo(method), assembliesToreference)
        { }

        public ServiceMethodModel(MethodGenerationInfo methodGenInfo, params Assembly[] assembliesToReference)
        {
            this.MethodGenerationInfo = methodGenInfo;
            List<Assembly> assemblies = new List<Assembly>(assembliesToReference);
            assemblies.AddRange(methodGenInfo.ReferenceAssemblies);
            this.ReferenceAssemblies = assemblies.ToArray();
        }

        public MethodGenerationInfo MethodGenerationInfo { get; private set; }

        public string ReturnType
        {
            get
            {
                return MethodGenerationInfo.ReturnTypeCodeString;
            }
        }

        public string MethodName
        {
            get
            {
                return MethodGenerationInfo.Method.Name;
            }
        }
        public string Signature
        {
            get
            {
                return MethodGenerationInfo.MethodSignature;
            }
        }

        public string ParameterInstances
        {
            get
            {
                return MethodGenerationInfo.ParameterInstances;
            }
        }
        public string OverrideOrNew
        {
            get
            {
                return MethodGenerationInfo.Method.IsVirtual ? "override" : "new";
            }
        }

        public string Invocation
        {
            get
            {
                bool isVoidReturn = MethodGenerationInfo.IsVoidReturn;
                string returnOrBlank = MethodGenerationInfo.IsVoidReturn ? "" : "return ";                
                return string.Format("{0}_proxyClient.{1}", returnOrBlank, MethodGenerationInfo.Method.Name);
            }
        }

        public Assembly[] ReferenceAssemblies
        {
            get;
            set;
        }

        public string Render()
        {
            return Render(ReferenceAssemblies);
        }

        public string Render(params Assembly[] assembliesToReference)
        {
            return RazorRenderer.RenderResource<ServiceMethodModel>(this, "ServiceMethod.tmpl", assembliesToReference);
        }
    }
}
