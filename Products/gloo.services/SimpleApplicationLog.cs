using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.Logging;

namespace gloo.services
{
    public class SimpleApplicationLog : ILog
    {
        public SimpleApplicationLog(CoreClient coreClient)
        {

        }
        public CoreClient CoreClient { get; }
        public void Error(string messageSignature, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Info(string messageSignature, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Warning(string messageSignature, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
