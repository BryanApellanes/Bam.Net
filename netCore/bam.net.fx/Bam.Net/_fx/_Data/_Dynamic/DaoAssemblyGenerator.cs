using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Bam.Net.Data.MsSql;
using System.IO;
using System.CodeDom.Compiler;
using System.Reflection;

namespace Bam.Net.Data.Dynamic
{
    public partial class DaoAssemblyGenerator
    {
        public DaoAssemblyGenerator(SchemaExtractor schemaExtractor, string workspacePath = null)
        {
            this.SchemaExtractor = schemaExtractor ?? new MsSqlSmoSchemaExtractor("Default");
            this.ReferenceAssemblies = new Assembly[] { };
            this._referenceAssemblyPaths = new List<string>(AdHocCSharpCompiler.DefaultReferenceAssemblies);

            this.Workspace = workspacePath ?? ".";
        }
    }
}
