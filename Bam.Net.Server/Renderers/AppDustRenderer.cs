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
using Bam.Net.Presentation.Html;
using System.Reflection;
using Bam.Net.Logging;
using Bam.Net.Presentation;

namespace Bam.Net.Server.Renderers
{
    public class AppDustRenderer: CommonDustRenderer
    {
        public AppDustRenderer(AppContentResponder appContent)
            : base(appContent.ContentResponder)
        {
            AppContentResponder = appContent;
            Logger = appContent.Logger;
        }
        
        public AppContentResponder AppContentResponder
        {
            get;
            set;
        }

        string _combinedCompiledTemplates;
        object _combinedCompiledTemplatesLock = new object();
        /// <summary>
        /// All application compiled dust templates including Server level
        /// layouts, templates.
        /// </summary>
        public override string CombinedCompiledTemplates
        {
            get
            {
                return _combinedCompiledTemplatesLock.DoubleCheckLock(ref _combinedCompiledTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();
                    Logger.AddEntry("AppDustRenderer::Appending compiled layout templates");
                    templates.AppendLine(CombinedCompiledLayoutTemplates);
                    Logger.AddEntry("AppDustRenderer::Appending compiled common templates");
                    templates.AppendLine(ContentResponder.CommonTemplateManager.CombinedCompiledTemplates);

                    foreach(string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo appDust = new DirectoryInfo(Path.Combine(AppContentResponder.AppRoot.Root, templateDirectoryName));
                        AppendTemplatesFromDirectory(appDust, templates);
                    }
                    return templates.ToString();
                });
            }
        }

        string _combinedCompiledLayoutTemplates;
        object _combinedCompiledLayoutTemplatesLock = new object();
        /// <summary>
        /// Represents the compiled javascript result of doing dust.compile
        /// against all the files found in ~s:/common/views/layouts.
        /// </summary>
        public override string CombinedCompiledLayoutTemplates
        {
            get
            {
                return _combinedCompiledLayoutTemplatesLock.DoubleCheckLock(ref _combinedCompiledLayoutTemplates, () =>
                {
                    StringBuilder templates = new StringBuilder();

                    foreach(string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo layouts = new DirectoryInfo(Path.Combine(AppContentResponder.AppRoot.Root, templateDirectoryName, "layouts"));
                        string compiledLayouts = DustScript.CompileTemplates(layouts.FullName, "*.dust", Logger);
                        templates.Append(compiledLayouts);
                    }

                    templates.Append(base.CombinedCompiledLayoutTemplates);
                    return templates.ToString();
                });
            }
        }

        List<ICompiledTemplate> _compiledTemplates;
        object _compiledTemplatesLock = new object();
        public override IEnumerable<ICompiledTemplate> CompiledTemplates
        {
            get
            {
                return _compiledTemplatesLock.DoubleCheckLock(ref _compiledTemplates, () =>
                {
                    List<ICompiledTemplate> allResults = base.CompiledTemplates.ToList();
                    foreach (string templateDirectoryName in TemplateDirectoryNames)
                    {
                        DirectoryInfo appDustDirectory = new DirectoryInfo(Path.Combine(AppContentResponder.AppRoot.Root, templateDirectoryName));
                        DustScript.CompileTemplates(appDustDirectory, out List<ICompiledTemplate> results, "*.dust");
                        allResults.AddRange(results);
                    }
                    return allResults;
                });
            }
        }

        protected internal bool TemplateExists(Type anyType, string templateFileNameWithoutExtension, out string fullPath)
        {
            string relativeFilePath = "~/views/{0}/{1}.dust"._Format(anyType.Name, templateFileNameWithoutExtension);
            fullPath = AppContentResponder.AppRoot.GetAbsolutePath(relativeFilePath);
            return File.Exists(fullPath);
        }

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

        private void AppendTemplatesFromDirectory(DirectoryInfo appDust, StringBuilder templates)
        {
            string domAppName = AppConf.DomApplicationIdFromAppName(this.AppContentResponder.AppConf.Name);
            Logger.AddEntry("AppDustRenderer::Compiling directory {0}", appDust.FullName);
            string appCompiledTemplates = DustScript.CompileTemplates(appDust, "*.dust", SearchOption.AllDirectories, domAppName + ".", Logger);

            templates.Append(appCompiledTemplates);
        }
    }
}
