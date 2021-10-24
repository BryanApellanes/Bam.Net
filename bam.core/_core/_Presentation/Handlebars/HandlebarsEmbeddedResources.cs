using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsEmbeddedResources
    {
        public HandlebarsEmbeddedResources(Assembly assembly)
        {
            Assembly = assembly;
            Templates = new Dictionary<string, Func<object, string>>();
        }

        public Assembly Assembly { get; set; }

        public Dictionary<string, Func<object, string>> Templates
        {
            get;
            set;
        }

        readonly object _reloadLock = new object();
        bool _loaded = false;
        public bool IsLoaded => _loaded;

        public string Render(string templateName, object data)
        {
            if (!_loaded)
            {
                Reload();
            }
            if (!Templates.ContainsKey(templateName))
            {
                Args.Throw<InvalidOperationException>("Specified template not found: {0}", templateName);
            }
            return Templates[templateName](data);
        }

        public void Reload()
        {
            lock (_reloadLock)
            {
                // register each before compiling individually so each is available as a partial
                ForEachEmbeddedTemplate(resourceName =>
                {
                    using (TextReader sr = new StreamReader(Assembly.GetManifestResourceStream(resourceName)))
                    {
                        string longName = Path.GetFileNameWithoutExtension(resourceName);
                        string shortName = longName.Substring(longName.LastIndexOf(".") + 1);
                        string templateText = sr.ReadToEnd();
                        HandlebarsDotNet.Handlebars.RegisterTemplate(longName, templateText);
                        HandlebarsDotNet.Handlebars.RegisterTemplate(shortName, templateText);
                    }
                });

                ForEachEmbeddedTemplate(resourceName =>
                {
                    using (TextReader sr = new StreamReader(Assembly.GetManifestResourceStream(resourceName)))
                    {
                        string longName = Path.GetFileNameWithoutExtension(resourceName);
                        string shortName = longName.Substring(longName.LastIndexOf(".") + 1);
                        string templateText = sr.ReadToEnd();
                        Func<object, string> compiled = HandlebarsDotNet.Handlebars.Compile(templateText);
                        Templates.AddMissing(longName, compiled);
                        Templates.AddMissing(shortName, compiled);
                    }
                });

                _loaded = true;
            }
        }

        private void ForEachEmbeddedTemplate(Action<string> action)
        {
            string[] resourceNames = Assembly.GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                if (resourceName.EndsWith(".hbs", StringComparison.InvariantCultureIgnoreCase))
                {
                    action(resourceName);
                }
            }
        }
    }
}
