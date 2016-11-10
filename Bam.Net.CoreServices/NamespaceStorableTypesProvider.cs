using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.CoreServices
{
    public class NamespaceStorableTypesProvider : IStorableTypesLoader
    {
        const string _file = ".\\StorableTypesNamespaces.txt";
        public NamespaceStorableTypesProvider()
        {
            Namespaces = new string[] { };
            if (File.Exists(_file))
            {
                Namespaces = File.ReadAllLines(_file);
            }
        }
        public string[] Namespaces { get; set; }
        public IEnumerable<Type> GetTypes()
        {
            List<string> namespaces = new List<string>(Namespaces);
            foreach(Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(Type type in ass.GetTypes())
                {
                    if (namespaces.Contains(type.Namespace))
                    {
                        yield return type;
                    }
                }
            }
        }
    }
}
