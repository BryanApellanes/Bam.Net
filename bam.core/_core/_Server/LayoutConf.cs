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
using Bam.Net.Services;

namespace Bam.Net.Server
{
    public partial class LayoutConf
    {
        protected internal void SetIncludes(AppConf conf, LayoutModel layoutModel)
        {
            Args.ThrowIfNull(conf, "AppConf");
            Args.ThrowIfNull(conf.BamConf, "BamConf");
            Args.ThrowIfNull(conf.BamConf.ContentRoot, "ContentRoot");
            ApplicationServiceRegistry reg = ApplicationServiceRegistry.ForApplication(conf.Name);
            IIncludesResolver includesResolver = reg.Get<IIncludesResolver>();
            Includes commonIncludes = new Includes(); 
            if (IncludeCommon)
            {
                commonIncludes = includesResolver.ResolveCommonIncludes(conf.BamConf.ContentRoot);   
            }
            Includes appIncludes = includesResolver.ResolveApplicationIncludes(conf.Name, conf.BamConf.ContentRoot);
            Includes combined = commonIncludes.Combine(appIncludes);
            StringBuilder styleSheetLinkTags = new StringBuilder();
            foreach (string css in combined.Css)
            {
                styleSheetLinkTags.AppendLine(StyleSheetLinkTag.For(css).Render());
            }

            layoutModel.StyleSheetLinkTags = styleSheetLinkTags.ToString();

            StringBuilder scriptLinkTags = new StringBuilder();
            foreach (string script in combined.Scripts)
            {
                scriptLinkTags.Append(ScriptTag.For(script).Render());
            }

            layoutModel.ScriptTags = scriptLinkTags.ToString();
        }
    }
}
