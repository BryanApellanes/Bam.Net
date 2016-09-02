/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Logging
{
    [Proxy("log")]
    public class ClientLogger: ILog
    {
        public ClientLogger()
        {
            this.Logger = Log.Default;
        }

        public ClientLogger(ILogger logger)
        {
            this.Logger = logger;
        }

        protected ILogger Logger
        {
            get;
            set;
        }

        public void AddEntry(string messageSignature, int verbosity, string[] formatArgs)
        {
            Logger.AddEntry(messageSignature, verbosity, formatArgs);
        }

        public void Error(string messageSignature, params object[] args)
        {
            Logger.AddEntry(messageSignature, Args.Exception(messageSignature, args), args.Select(a => a.ToString()).ToArray());
        }

        public void Info(string messageSignature, params object[] args)
        {
            Logger.AddEntry(messageSignature, args.Select(a => a.ToString()).ToArray());
        }

        public void Warning(string messageSignature, params object[] args)
        {
            Logger.AddEntry(messageSignature, LogEventType.Warning, args.Select(a => a.ToString()).ToArray());
        }
    }
}
