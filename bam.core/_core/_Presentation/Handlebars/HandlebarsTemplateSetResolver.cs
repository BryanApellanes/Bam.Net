using System.IO;
using Bam.Net.Server;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsTemplateSetResolver
    {
        public HandlebarsTemplateSetResolver(AppConf appConf)
        {
            AppConf = appConf;
        }
        
        public AppConf AppConf { get; set; }

        public HandlebarsTemplateSet GetTemplateSet()
        {
            return new HandlebarsTemplateSet(Path.Combine(AppConf.AppRoot.Root, "Handlebars"));
        }
    }
}