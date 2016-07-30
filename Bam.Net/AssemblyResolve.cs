using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net;

namespace Bam.Net
{
    public static class AssemblyResolve
    {
        public static void Monitor(Func<ILogger> loggerProvider)
        {
            Monitor(loggerProvider());
        }
        public static void Monitor(ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                logger.AddEntry("Assembly {0}\r\nAt {1}\r\nRequested {2}\r\n\t, but it was not found", LogEventType.Warning, args.RequestingAssembly.FullName, args.RequestingAssembly.GetFileInfo().FullName, args.Name);
                return null;
            };
        }
    }
}
