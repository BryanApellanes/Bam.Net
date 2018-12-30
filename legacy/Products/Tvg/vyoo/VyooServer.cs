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
            Init();
        }

        public VyooServer(AppConf[] appConfigs, ILogger logger, bool verbose = false) 
            : base(new VyooResponder(appConfigs, logger, verbose), logger)
        {
            Init();
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

        protected void ReloadFile(object sender, FileSystemEventArgs args)
        {
            FileInfo file = new FileInfo(args.FullPath);
            if (file.Exists)
            {
                Responder.ContentResponder.UncacheFile(file);
                Responder.ContentResponder.RefreshLayouts();
            }
        }

        private void Init()
        {
            Responder.Initialize();
            CreatedOrChangedHandler = ReloadFile;
        }
    }
}
