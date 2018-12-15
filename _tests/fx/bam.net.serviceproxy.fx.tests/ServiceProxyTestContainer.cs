using Bam.Net.Data;
using Bam.Net.Data.Schema;
using Bam.Net.Data.SQLite;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Web;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer : CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldBeAbleToDownloadAndCompileCSharpProxy()
        {
            RegisterDb();
            ServiceProxyTestHelpers.StartSecureChannelTestServerGetEncryptedEchoClient(out BamServer server, out SecureServiceProxyClient<EncryptedEcho> sspc);

            string value = Http.Get($"{sspc.BaseAddress}ServiceProxy/CSharpProxies?namespace=My.Test.Namespace&classes=EncryptedEcho");
            FileInfo codeFile = new FileInfo(".\\Tmp\\code.cs");
            if (codeFile.Directory.Exists)
            {
                codeFile.Directory.Delete(true);
            }
            codeFile.Directory.Create();
            value.SafeWriteToFile(codeFile.FullName, true);

            List<string> referenceAssemblies = new List<string>(DaoGenerator.DefaultReferenceAssemblies)
            {
                typeof(ServiceProxyClient).Assembly.GetFileInfo().FullName,
                typeof(BamServer).Assembly.GetFileInfo().FullName
            };

            CompilerResults results = AdHocCSharpCompiler.CompileDirectories(new DirectoryInfo[] { codeFile.Directory }, ".\\Tmp\\TestClients.dll", referenceAssemblies.ToArray(), false);

            if (results.Errors.Count > 0)
            {
                StringBuilder message = new StringBuilder();
                foreach (CompilerError err in results.Errors)
                {
                    message.AppendLine(string.Format("File=>{0}\r\n{1}:::Line {2}, Column {3}::{4}",
                                                    err.FileName, err.ErrorNumber, err.Line, err.Column, err.ErrorText));
                }
                OutLine(message.ToString(), ConsoleColor.Red);
            }

            Expect.AreEqual(0, results.Errors.Count);

            Expect.IsTrue(results.CompiledAssembly.GetFileInfo().Exists);

            server.Stop();
        }

        public static void RegisterDb()
        {
            SQLiteDatabase db = new SQLiteDatabase();
            Db.For<Secure.Application>(db);
            Db.For<Account>(UserAccountsDatabase.Default);
            Db.TryEnsureSchema<Secure.Application>(db);
            SQLiteRegistrar.Register<Secure.Application>();
        }
    }
}
