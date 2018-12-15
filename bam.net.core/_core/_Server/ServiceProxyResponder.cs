using Bam.Net.Incubation;
using Bam.Net.Yaml;
using System.IO;
using System.Reflection;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using Bam.Net.Server.Renderers;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Server.Meta;

namespace Bam.Net.Server
{
    public partial class ServiceProxyResponder
    {
        private bool SendMethodForm(IHttpContext context)
        {
            Logger.AddEntry("{0} method is not supported by this frameowrk", nameof(SendMethodForm));
            return false;
        }
    }
}
