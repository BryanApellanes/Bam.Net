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

namespace Bam.Net.Server
{
    public partial class LayoutConf
    {
        protected internal void SetIncludes(AppConf conf, LayoutModel layoutModel)
        {
            Includes includes = AppContentResponder.GetAppIncludes(conf);
            if (IncludeCommon)
            {
                Includes commonIncludes = ContentResponder.GetCommonIncludes(ContentRoot);
                includes = commonIncludes.Combine(includes);
            }
            layoutModel.ScriptTags = includes.GetScriptTags().ToHtmlString();
            layoutModel.StyleSheetLinkTags = includes.GetStyleSheetLinkTags().ToHtmlString();
        }
    }
}
