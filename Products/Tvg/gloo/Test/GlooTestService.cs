using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.Application
{
    [Proxy("glooTestSvc")]
    public class GlooTestService
    {
        public GlooMonkey GetMonkey(string name)
        {
            return new GlooMonkey(name);
        }
    }

    [Encrypt]
    [Proxy("glooEncryptedTestSvc")]
    public class GlooEncryptedTestService
    {
        public GlooMonkey GetMonkey(string name)
        {
            return new GlooMonkey(string.Format("From Encrypted Test Service: {0}", name));
        }
    }

    [ApiKeyRequired]
    [Proxy("glooApiKeyRequiredSvc")]
    public class GlooApiKeyRequiredTestService
    {
        public GlooMonkey GetMonkey(string name)
        {
            return new GlooMonkey(string.Format("From ApiKeyRequired Test Service: {0}", name));
        }
    }
}
