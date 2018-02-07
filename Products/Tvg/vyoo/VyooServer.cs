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
        public VyooServer(BamConf conf, ILogger logger)
            : base(new VyooResponder(conf, logger), logger)
        {
            Responder.Initialize();
            CreatedOrChangedHandler = (o, fsea) =>
            {
                ReloadFile(fsea);
            };
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
