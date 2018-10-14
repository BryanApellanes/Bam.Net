using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net.Data.Repositories;
using System.Diagnostics;

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

        [DebuggerStepThrough]
        public override HashSet<Type> GetTypes()
        {
            List<string> namespaces = new List<string>(Namespaces);
            HashSet<Type> result = new HashSet<Type>();
            try
            {
                foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in ass.GetTypes().Where(TypeDaoGenerator.ClrDaoTypeFilter))
                    {
                        if (namespaces.Contains(type.Namespace))
                        {
                            result.Add(type);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Bam.Net.Logging.Log.Default.AddEntry("An exception occurred in {0}: {1}", ex, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return result;
        }
    }
}
