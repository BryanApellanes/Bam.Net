using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Data.Repositories;
using System.Reflection;
using Bam.Net.Data;
using System.IO;
using Bam.Net;
using Bam.Net.Data.SQLite;
using Bam.Net.Logging;

namespace troo
{
    [Serializable]
    public class UtilityActions : CommandLineTestInterface
    {
        [ConsoleAction("generateDaoForTypes", "Generate Daos for types")]
        public static void GenerateDaoForTypes()
        {
            Assembly typeAssembly = Assembly.LoadFrom(GetArgument("typeAssembly", "Please enter the path to the assembly containing the types to generate daos for"));
            string schemaName = GetArgument("schemaName", "Please enter the schema name to use").Replace(".", "_");
            string fromNameSpace = GetArgument("fromNameSpace", "Please enter the namespace containing the types to generate daos for");
            string toNameSpace = GetArgument("toNameSpace", "Please enter the namespace to write generated daos to");
            DaoRepository repo = new DaoRepository(new SQLiteDatabase(".", schemaName), Log.Default, schemaName);
            repo.DaoNameSpace = toNameSpace;
            repo.AddNamespace(typeAssembly, fromNameSpace);
            Assembly daoAssembly = repo.GenerateDaoAssembly();
            FileInfo fileInfo = daoAssembly.GetFileInfo();
            string copyTo = Path.Combine(GetArgument("copyTo", "Please enter the directory to copy the resulting assemlby to"), fileInfo.Name);
            fileInfo.CopyTo(copyTo, true);
            OutLineFormat("File generated:\r\n{0}", copyTo);
        }
    }
}
