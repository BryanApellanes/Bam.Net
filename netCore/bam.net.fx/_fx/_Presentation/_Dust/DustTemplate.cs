/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Bam.Net;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Js;
using Bam.Net.Incubation;
using EcmaScript.NET;
using System.Text.RegularExpressions;

namespace Bam.Net.Presentation.Dust
{
    /// <summary>
    /// Represents a dust template
    /// </summary>
    public class DustTemplate
    {
        internal DustTemplate()
        {

        }
        public DustTemplate(string templateSource, string templateName)
        {
            this.Source = templateSource;
            this.Name = templateName;
        }

        public DustTemplate(FileInfo file, string templateName = null)
        {
            if (string.IsNullOrEmpty(templateName))
            {
                string name = file.Name;
                name = name.Substring(0, name.Length - file.Extension.Length);
                this.Name = name;
            }
            else
            {
                this.Name = templateName;
            }

            if (!file.Exists)
            {
                throw Args.Exception<FileNotFoundException>("The specified dust template {0} was not found", file.FullName);
            }

            using (StreamReader sr = new StreamReader(file.FullName))
            {
                this.Source = sr.ReadToEnd();
            }
        }

        public Exception Exception
        {
            get;
            set;
        }

        string _src;
        public string Source
        {
            get
            {
                return _src;
            }
            set
            {
                _src = value.Replace("\r\n", "");
            }
        }

        public string Name
        {
            get;
            set;
        }

        string _compiledScript;
        public string CompiledScript
        {
            get
            {
                if (string.IsNullOrEmpty(_compiledScript))
                {
                    Compile();
                }

                return _compiledScript;
            }
            private set
            {
                _compiledScript = value;
            }
        }

        public string Render(object value)
        {
            return Render(value, CompiledScript);
        }

        protected internal string Render(object value, string templateScript)
        {
            try
            {
                string json = value.ToJson();
                StringBuilder script = CreateBaseScript();
                script.Append("dust.loadSource(compiledSource);");                
                script.Append(@"dust.render(templateName, JSON.parse(jsonData), function(err, result){
    error = err;
    output = result;
})");

                EcmaScript.NET.Context ctx = EcmaScript.NET.Context.Enter();
                ScriptableObject scope = ctx.InitStandardObjects();                
                SetParameters(scope);
                ScriptableObject.PutProperty(scope, "jsonData", json);
                ScriptableObject.PutProperty(scope, "compiledSource", Regex.Unescape(templateScript));
                ScriptableObject.PutProperty(scope, "error", null);
                ScriptableObject.PutProperty(scope, "output", "");

                ctx.EvaluateString(scope, script.ToString(), "render", 0, null);

                object error = ScriptableObject.GetProperty(scope, "error");
                if (error != null)
                {
                    throw new InvalidOperationException("An error occurred during dust.render: " + error.ToString());
                }

                string result = ((string)ScriptableObject.GetProperty(scope, "output"));
                return Regex.Unescape(result);
            }
            catch (Exception ex)
            {
                string sig = "An error occurred rendering template ({0}) : {1}";
                string msg = ex.Message;
                this.Exception = Args.Exception<DustException>(sig, Name, msg);
                return string.Format(sig, Name, msg);
            }
        }

        object _compileLock = new object();
        protected internal void Compile()
        {
            lock (_compileLock)
            {
                if (!Dust.CompiledTemplates.ContainsKey(Name))
                {
                    SetCompiledScript();
                }
                else
                {
                    _compiledScript = Dust.CompiledTemplates[Name];
                }

                if (!Dust.Templates.ContainsKey(Name))
                {
                    SetTemplate();
                }
            }
        }

        private void SetCompiledScript()
        {
            try
            {
                StringBuilder script = CreateBaseScript();
                script.Append("compiledTemplate = dust.compile(templateSource, templateName);");

                EcmaScript.NET.Context ctx = EcmaScript.NET.Context.Enter();
                
                ScriptableObject scope = ctx.InitStandardObjects();
                SetParameters(scope);
                ScriptableObject.PutProperty(scope, "compiledTemplate", null);
                
                ctx.EvaluateString(scope, script.ToString(), "Compile", 0, null);
                CompiledScript = (string)ScriptableObject.GetProperty(scope, "compiledTemplate");

                SetCompiledTemplate();
            }
            catch (Exception ex)
            {
                CompiledScript = string.Format("alert('An error occurred compiling {0}: {1}');", Name, ex.Message);
                Exception = Args.Exception<DustException>("An error occurred compiling script ({0}) :{1}", ex, Name, ex.Message);
            }
        }

        private void SetTemplate()
        {
            Dust.SetTemplate(this);
        }

        private void SetCompiledTemplate()
        {
            Dust.SetCompiledTemplate(this);
        }
        
        private void SetParameters(IScriptable scope)
        {
            ScriptableObject.PutProperty(scope, "templateSource", Source);
            ScriptableObject.PutProperty(scope, "templateName", Name);
        }

        private static StringBuilder CreateBaseScript()
        {
            ResourceScripts.LoadScripts(typeof(Dust));
            StringBuilder script = new StringBuilder();
            script.Append(ResourceScripts.Get("json2.js"));
            script.Append(ResourceScripts.Get("dust.custom.js"));
            return script;
        }
    }
}
