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
                        Path.Combine(folder, "bam.core.dll")
                    };
                    _defaultReferenceAssemblies = defaultAssemblies.ToArray();
                }

                return _defaultReferenceAssemblies;
            }
        }
    }
}
