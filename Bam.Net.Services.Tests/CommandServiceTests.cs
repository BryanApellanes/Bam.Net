using Bam.Net.CommandLine;
using Bam.Net.CoreServices;
using Bam.Net.Services.Automation;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;
using Bam.Net.UserAccounts.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Tests
{
    [Serializable]
    public class CommandServiceTests : CommandLineTestInterface    
    {
        [UnitTest]
        public void CanCallCommand()
        {
            UserTestTools.SignUpAndLogin("CanCallCommand", out ServiceProxy.IHttpContext context, out UserAccounts.LoginResponse result);
            ServiceRegistry reg = CoreServiceRegistryContainer.Create();
            ConsoleLogger logger = new ConsoleLogger()
            {
                AddDetails = false
            };
            logger.StartLoggingThread();
            CommandService svc = reg.Get<CommandService>();
            svc.HttpContext = context;
            svc.Logger = logger;
            ServiceResponse<CommandInfo> cmd = svc.Start("dir");
            OutLine(cmd.ToJson());
        }
    }
}
