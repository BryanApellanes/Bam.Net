/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.Incubation;
using Bam.Net.Html;
using System.Reflection;

namespace Bam.Net.Server.Renderers
{
    public class AppDustRenderer: CommonDustRenderer
    {
        public AppDustRenderer(AppContentResponder appContent)
            : base(appContent.ContentResponder)
        {
            this.AppContentResponder = appContent;
        }

        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        string _compiledDustTemplates;
        object _compiledDustTemplatesLock = new object();
        /// <summary>
        /// All application compiled dust templates including Server level
        /// layouts, templates and app custom and type templates
        /// </summary>
        public override string CompiledTemplates
        {
            get
            {
                return _compiledDustTemplatesLock.DoubleCheckLock(ref _compiledDustTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    templates.AppendLine(CompiledLayoutTemplates);
                    templates.AppendLine(CompiledCommonTemplates);

                    DirectoryInfo appDust = new DirectoryInfo(Path.Combine(AppContentResponder.AppRoot.Root, "views"));
                    string domAppName = AppConf.DomApplicationIdFromAppName(this.AppContentResponder.AppConf.Name);
                    string appCompiledTemplates = DustScript.CompileDirectory(appDust, "*.dust", SearchOption.AllDirectories, domAppName + ".");

                    templates.Append(appCompiledTemplates);
                    return templates.ToString();
                });
            }
        }

        protected internal bool TemplateExists(Type anyType, string templateFileNameWithoutExtension, out string fullPath)
        {
            string relativeFilePath = "~/views/{0}/{1}.dust"._Format(anyType.Name, templateFileNameWithoutExtension);
            fullPath = AppContentResponder.AppRoot.GetAbsolutePath(relativeFilePath);
            return File.Exists(fullPath);
        }

        protected internal void EnsureDefaultTemplate(Type anyType)
        {
            EnsureTemplate(anyType, "default");
        }

        protected internal void EnsureTemplate(Type anyType, string templateName)
        {
            string fullPath;
            if(!TemplateExists(anyType, templateName, out fullPath))
            {
                lock(_compiledDustTemplatesLock)
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
                    _compiledDustTemplates = null; // forces reload
                }
            }
        }

        public string InputFor(Type type, object defaults = null, string name = null)
        {
            InputFormBuilder builder = new InputFormBuilder();
            return builder.FieldsetFor(type, defaults, name).ToString();
        }

        private void SetTemplateProperties(object instance)
        {
            Type type = instance.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                if (prop.PropertyType == typeof(string) ||
                    prop.PropertyType == typeof(int) ||
                    prop.PropertyType == typeof(long))
                {
                    prop.SetValue(instance, "{" + prop.Name + "}", null);
                }
            }
        }
    }
}
