using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using System.IO;

namespace Bam.Net.Application
{
    public class VyooServer : SimpleServer<VyooResponder>
    {
        public VyooServer(BamConf conf, ILogger logger, bool verbose = false)
            : base(new VyooResponder(conf, logger, verbose), logger)
        {
            Responder.Initialize();
            CreatedOrChangedHandler = (o, fsea) =>
            {
                ReloadFile(fsea);
            };
        }

        public AppConf[] AppConfigs
        {
            get
            {
                return Responder.ContentResponder.AppConfigs;
            }
            set
            {
                Responder.ContentResponder.AppConfigs = value;
            }
        }

        protected void ReloadFile(FileSystemEventArgs args)
        {
            FileInfo file = new FileInfo(args.FullPath);
            if (file.Exists)
            {
                Responder.ContentResponder.UncacheFile(file);
            }
        }
    }
}
