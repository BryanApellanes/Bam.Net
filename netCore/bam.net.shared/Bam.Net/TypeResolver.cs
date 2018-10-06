using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Dynamic
{
    public class TypeResolver : ITypeResolver
    {
        public TypeResolver()
        {
            _assemblies = new List<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
        }

        List<Assembly> _assemblies;
        public Assembly[] Assemblies
        {
            get { return _assemblies.ToArray(); }
        }

        public bool ScanAssemblies { get; set; }

        public void AddAssembly(string assemblyFilePath)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFilePath);
            if(assembly != null)
            {
                _assemblies.Add(assembly);
            }
        }

        public Type ResolveType(string typeName)
        {
            Type type = Type.GetType(typeName);
            if(type == null && ScanAssemblies)
            {
                foreach(Assembly a in Assemblies)
                {
                    type = a.GetType(typeName);
                    if(type == null)
                    {
                        type = a.GetTypes().FirstOrDefault(t => t.Name.Equals(typeName) || $"{t.Namespace}.{t.Name}".Equals(typeName));
                    }

                    if(type != null)
                    {
                        break;
                    }
                }
            }
            return type;
        }

        public Type ResolveType(string nameSpace, string typeName)
        {
            return ResolveType($"{nameSpace}.{typeName}");
        }
    }
}
