using Bam.Net.CommandLine;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Services;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Services.Automation
{
    [Encrypt]
    [ApiKeyRequired]
    [Proxy("commandSvc")]
    [ServiceSubdomain("command")]
    public class CommandService : AsyncProxyableService
    {
        public CommandService()
        {
        }

        public ServiceResponse<CommandInfo> Start(string command)
        {
            try
            {
                UserIsLoggedInOrDie();

                CommandInfo info = new CommandInfo { Command = command };
                IRepository repo = RepositoryResolver.GetRepository(HttpContext);
                info = repo.Save(info);
                Task<ProcessOutput> task = command.RunAsync();
                task.ContinueWith((t) =>
                {
                    Logger.AddEntry("Command completed: {0}", command);
                    info.StandardOut = t.Result.StandardOutput;
                    info.StandardError = t.Result.StandardError;
                    info = repo.Save(info);
                });
                Logger.AddEntry("Running command: {0}", command);
                return new ServiceResponse<CommandInfo>
                {
                    Success = true,
                    Data = info
                };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error in CommandService: {0}", ex, ex.Message);
                return new ServiceResponse<CommandInfo>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }

        public override object Clone()
        {
            CommandService clone = new CommandService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
