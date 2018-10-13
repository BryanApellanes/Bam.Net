using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Incubation;
using Bam.Net.Presentation.Html;
using System.Reflection;
using Bam.Net.Logging;
using Bam.Net.Presentation;

namespace Bam.Net.Server.Renderers
{
    public partial class AppDustRenderer : CommonDustRenderer
    {
        public override void EnsureDefaultTemplate(Type anyType)
        {
            EnsureTemplate(anyType, "default");
        }

        protected internal void EnsureTemplate(Type anyType, string templateName)
        {
            if (!TemplateExists(anyType, templateName, out string fullPath))
            {
                lock (_combinedCompiledTemplatesLock)
                {
                    object instance = anyType.Construct().ValuePropertiesToDynamic();
                    SetTemplateProperties(instance);
                    string htm = InputFor(instance.GetType(), instance).XmlToHumanReadable();

                    FileInfo file = new FileInfo(fullPath);
                    if (!file.Directory.Exists)
                    {
                        file.Directory.Create();
                    }

                    File.WriteAllText(fullPath, htm);
                    _combinedCompiledTemplates = null; // forces reload
                }
            }
        }

        public string InputFor(Type type, object defaults = null, string name = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            return builder.FieldsetFor(type, defaults, name).ToString();
        }
    }
}
