using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace Bam.Net
{
    public static partial class AdHocCSharpCompiler
    {
        static string[] _defaultReferenceAssemblies = new string[] { };
        public static string[] DefaultReferenceAssemblies
        {
            get
            {
                if (_defaultReferenceAssemblies.Length == 0)
                {
                    string folder = typeof(AdHocCSharpCompiler).Assembly.GetFileInfo().DirectoryName;
                    List<string> defaultAssemblies = new List<string>
                    {
                        "System.dll",
                        "System.Core.dll",
                        "System.Xml.dll",
                        "System.Data.dll",
                        Path.Combine(folder, "System.Web.Mvc.dll"),
                        Path.Combine(folder, "Bam.Net.dll"),
                        Path.Combine(folder, "Bam.Net.ServiceProxy.dll"),
                        Path.Combine(folder, "Bam.Net.Data.dll"),
                        Path.Combine(folder, "Bam.Net.Data.Schema.dll"),
                        Path.Combine(folder, "Bam.Net.Data.dll"),
                        Path.Combine(folder, "Bam.Net.Incubation.dll")
                    };
                    _defaultReferenceAssemblies = defaultAssemblies.ToArray();
                }

                return _defaultReferenceAssemblies;
            }
        }
    }
}
