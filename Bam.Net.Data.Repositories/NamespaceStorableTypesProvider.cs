using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Data.Repositories;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// A RepositoryTypesProvider implementation that reads namespaces from a file named
    /// StorableTypesNamspaces.txt from the current directory 
    /// </summary>
    public class NamespaceRepositoryStorableTypesProvider : RepositoryTypesProvider
    {
        const string _file = ".\\StorableTypesNamespaces.txt";
        public NamespaceRepositoryStorableTypesProvider()
        {
            Namespaces = new string[] { };
            if (File.Exists(_file))
            {
                Namespaces = File.ReadAllLines(_file);
            }
        }
        public string[] Namespaces { get; set; }
        public override HashSet<Type> GetTypes()
        {
            List<string> namespaces = new List<string>(Namespaces);
            HashSet<Type> result = new HashSet<Type>();
            foreach(Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach(Type type in ass.GetTypes().Where(TypeDaoGenerator.ClrDaoTypeFilter))
                {
                    if (namespaces.Contains(type.Namespace))
                    {
                        result.Add(type);
                    }
                }
            }
            return result;
        }
    }
}
