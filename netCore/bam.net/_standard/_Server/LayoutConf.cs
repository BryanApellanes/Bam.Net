using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;
using Bam.Net.Server;
using Newtonsoft.Json;
using Bam.Net.Presentation;
using Bam.Net.Presentation.Html;
using Bam.Net.Configuration;
using Bam.Net.Logging;

namespace Bam.Net.Server
{
    public partial class LayoutConf
    {
        protected internal void SetIncludes(AppConf conf, LayoutModel layoutModel)
        {
            Log.AddEntry("{0} is not supported on this platform.", nameof(SetIncludes));
        }
    }
}
