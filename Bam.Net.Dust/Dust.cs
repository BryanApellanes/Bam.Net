/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy.Js;
using Bam.Net.Html;
using EcmaScript.NET;

namespace Bam.Net.Dust
{
    /// <summary>
    /// Convenience entry point into Dust initialization and 
    /// configuration.  Set DustRoot to the folder that contains
    /// Dust templates or set it in the configuration file
    /// using the key DustRoot.  Be sure to call Dust.Initialize()
    /// before attempting Dust templating.
    /// </summary>
    public static class Dust
    {
        static Dictionary<string, string> _compiledTemplates;
        static Dictionary<string, DustTemplate> _templates;
        static string _dustRoot = "~/Dust";

        static Dust()
        {
            ResourceScripts.LoadScripts(typeof(Dust));
            _compiledTemplates = new Dictionary<string, string>();
            _templates = new Dictionary<string, DustTemplate>();
            _dustRoot = DefaultConfiguration.GetAppSetting("DustRoot", _dustRoot);
        }

        public static void SetCompiledTemplate(DustTemplate template)
        {
            string name = template.Name.ToLowerInvariant();
            string script = template.CompiledScript;
            if (!CompiledTemplates.ContainsKey(name))
            {
                CompiledTemplates.Add(name, script);
            }
            else
            {
                CompiledTemplates[name] = script;
            }
        }

        public static string AllCompiledTemplates
        {
            get
            {
                StringBuilder script = new StringBuilder();
                foreach (string template in CompiledTemplates.Values)
                {
                    script.Append(template);
                }

                return script.ToString();
            }
        }

        public static void SetTemplate(DustTemplate template)
        {
            Args.ThrowIf<ArgumentNullException>(string.IsNullOrEmpty(template.Name), "template.Name must be specified");

            string name = template.Name.ToLowerInvariant();
            if (!Templates.ContainsKey(name))
            {
                Templates.Add(name, template);
            }
            else
            {
                Templates[name] = template;
            }
        }

        /// <summary>
        /// Holds references by template name to the javascript compiled
        /// result of calling dust.compile
        /// </summary>
        public static Dictionary<string, string> CompiledTemplates
        {
            get
            {
                return _compiledTemplates;
            }
        }

        /// <summary>
        /// Holds references by template name to the DustTemplate
        /// instances
        /// </summary>
        public static Dictionary<string, DustTemplate> Templates
        {
            get
            {
                return _templates;
            }
        }

        private static StringBuilder CreateBaseScript()
        {
            ResourceScripts.LoadScripts(typeof(Dust));
            StringBuilder script = new StringBuilder();
            script.Append(ResourceScripts.Get("json2.js"));
            script.Append(ResourceScripts.Get("dust.custom.js"));
            return script;
        }

        /// <summary>
        /// Returns true if the specified tempalteName has been regisered
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static bool TemplateIsRegistered(string templateName)
        {
            Args.ThrowIf<ArgumentNullException>(string.IsNullOrEmpty(templateName), "templateName must be specified");
            templateName = templateName.ToLowerInvariant();
            return _compiledTemplates.ContainsKey(templateName);
        }

        public static MvcHtmlString RenderDust(this HtmlHelper helper, string templateName, object model)
        {
            return RenderMvcHtmlString(templateName, model);
        }

        public static MvcHtmlString RenderMvcHtmlString(string templateName, object value)
        {
            templateName = ValidateInput(templateName);

            return MvcHtmlString.Create(_templates[templateName].Render(value));
        }

        public static string Render(string templateName, object value)
        {
            templateName = ValidateInput(templateName);

            return _templates[templateName].Render(value, AllCompiledTemplates);
        }

        private static string ValidateInput(string templateName)
        {
            Args.ThrowIf<ArgumentNullException>(string.IsNullOrEmpty(templateName), "templateName must be specified");
            templateName = templateName.ToLowerInvariant();

            if (!TemplateIsRegistered(templateName))
            {
                throw Args.Exception<InvalidOperationException>("The specified templateName ({0}) was not registered", templateName);
            }
            return templateName;
        }

        public static Tag RegisterDustTemplates(this UrlHelper helper)
        {
            return new JsTag(helper.Content("~/dust/templates"));
        }

        public static MvcHtmlString DustTemplateFor(this HtmlHelper helper, Type type, object wrapperAttrs = null)
        {
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            Args.ThrowIf<InvalidOperationException>(ctor == null, "The specified type {0} doesn't have a parameterless constructor", type.Name);
            object value = ctor.Invoke(null);
            return DustTemplateFor(helper, value, wrapperAttrs);
        }

        public static MvcHtmlString DustTemplateFor(this HtmlHelper helper, object value, object wrapperAttrs = null)
        {
            Type type = value.GetType();
            foreach (PropertyInfo prop in type.GetProperties())
            {
                if (prop.CanWrite && prop.PropertyType == typeof(string))
                {
                    StringBuilder str = new StringBuilder();
                    str.A("{").Af("{0}", prop.Name).A("}");
                    prop.SetValue(value, str.ToString(), null);
                }
            }

            return helper.InputsFor(type, value, wrapperAttrs);
        }

        // TODO: Enable remote repository as DustRoot (http://templates.mysite.cxm)

        public static string DustRoot
        {
            get { return _dustRoot; }
            set
            {
                if (!value.Equals(_dustRoot))
                {
                    _dustRoot = value;
                    _initialized = false;
                }
            }
        }

        static bool _initialized;
        static object initLock = new object();
        /// <summary>
        /// Loads templates from the folder specified in DustRoot
        /// </summary>
        public static void Initialize()
        {
            if (!_initialized)
            {
                lock (initLock)
                {
                    if (!_initialized)
                    {
                        LoadTemplates();
                        _initialized = true;
                    }
                }
            }
        }

        static Func<string, string> _getAbsoluteDustRootDelegate = (relative) =>
        {
            return HttpContext.Current.Server.MapPath(relative);
        };

        /// <summary>
        /// The delegate used during initialization to determine the 
        /// absolute path to the dust root folder given the relative path.
        /// The default implementation uses HttpContext.Current.Server.MapPath
        /// </summary>
        public static Func<string, string> GetAbsoluteDustRootDelegate
        {
            get
            {
                return _getAbsoluteDustRootDelegate;
            }
            set
            {
                _getAbsoluteDustRootDelegate = value;
            }
        }

        private static void LoadTemplates()
        {
            string rootDir = _dustRoot;
            if (_dustRoot.StartsWith("~"))
            {
                rootDir = GetAbsoluteDustRootDelegate(rootDir);
            }

            LoadFromFileSystem(rootDir);
        }

        private static void LoadFromFileSystem(string rootDir)
        {
            DirectoryInfo dir = new DirectoryInfo(rootDir);
            if (dir.Exists)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    DustTemplate template = new DustTemplate(file);
                    template.Compile();
                }

                DirectoryInfo[] subDirs = dir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    files = subDir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        string fileName = file.Name;
                        fileName = fileName.Substring(0, fileName.Length - file.Extension.Length);
                        string templateName = string.Format("{0}_{1}", subDir.Name, fileName);
                        DustTemplate template = new DustTemplate(file, templateName);
                        template.Compile();
                    }
                }
            }
        }
    }
}
