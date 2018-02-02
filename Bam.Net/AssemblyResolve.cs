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
        static HashSet<string> _names = new HashSet<string>();
        public static void Monitor(ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) =>
            {
                if (!_names.Contains(args.Name))
                {
                    _names.Add(args.Name);
                    string messageFormat = @"AppDomain.CurrentDomain.AssemblyResolve event fired and assembly was not found:
\tRequesting Assembly: {0}
\tRequesting Assembly Location: {1}
\tRequested Assembly: {2}
\tEvent sender: {3}";
                    logger.AddEntry(messageFormat, LogEventType.Warning, args.RequestingAssembly?.FullName.Or("[null]"), args.RequestingAssembly?.GetFileInfo().FullName.Or("[null]"), args.Name.Or("[null]"), sender?.ToString().Or("[null]"));
                }
                return null;
            };
        }
    }
}
