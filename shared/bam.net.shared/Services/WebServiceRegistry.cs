using Bam.Net.CoreServices;
using Bam.Net.Incubation;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Bam.Net.Services
{
    public class WebServiceRegistry: ServiceRegistry
    {
        public WebServiceRegistry() { }
        
        public WebServiceRegistry(Incubator registry)
        {
            CopyWebServices(registry);
        }

        public void CopyWebServices(Incubator registry)
        {
            foreach (string className in registry.ClassNames)
            {
                object instance = registry.Get(className, out Type type);
                if (type.HasCustomAttributeOfType<ProxyAttribute>())
                {
                    CopyTypeFrom(type, registry);
                    List<object> ctorArgs = registry.GetCtorParams(type, out ConstructorInfo ctor);
                    if (ctor == null)
                    {
                        throw new InvalidOperationException($"Unable to get the appropriate arguments to construct the class {className}");
                    }
                    ParameterInfo[] parameters = ctor.GetParameters();
                    if (ctorArgs.Count != parameters.Length)
                    {
                        throw new InvalidOperationException($"Unable to get the correct number of constructor arguments for class {className}");
                    }
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        ParameterInfo parameterInfo = parameters[i];
                        SetCtorParam(type, parameterInfo.Name, ctorArgs[i]);
                    }
                }
            }
        }

        public static WebServiceRegistry FromRegistry(ServiceRegistry registry)
        {
            return FromIncubator(registry);
        }

        public static WebServiceRegistry FromIncubator(Incubator incubator)
        {
            WebServiceRegistry result = new WebServiceRegistry();
            result.CopyWebServices(incubator);
            return result;
        }
    }
}
