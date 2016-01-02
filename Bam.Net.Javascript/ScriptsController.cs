/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using Bam.Net;
using System.IO;
using Yahoo.Yui.Compressor;

namespace Bam.Net.Html
{
    public class ScriptsController: Controller
    {
        static Dictionary<string, string> _scripts;
        static object _scriptLock = new object();
        static Dictionary<string, string> Scripts
        {
            get
            {
                return _scriptLock.DoubleCheckLock(ref _scripts, () => new Dictionary<string, string>());
            }
        }

        static Dictionary<string, string> _minScripts;
        static object _minScriptsLock = new object();
        static Dictionary<string, string> MinScripts
        {
            get
            {
                return _minScriptsLock.DoubleCheckLock(ref _minScripts, () => new Dictionary<string, string>());
            }
        }

        public ActionResult Load(string name, string callback = "", bool min = false)
        {
            string scriptName = name.ToLowerInvariant();
            Dictionary<string, string> container = GetScriptContainer(min, scriptName);
            string script = container.ContainsKey(scriptName) ? container[scriptName] : ";alert('script \"{0}\" not found');"._Format(name);
            if (!string.IsNullOrEmpty(callback))
            {
                script = "{0};\r\n\r\n{1};"._Format(script, callback);
            }
            return JavaScript(script);
        }

        private Dictionary<string, string> GetScriptContainer(bool min, string scriptName)
        {
            string scriptPath = FindScript(scriptName);//Server.MapPath("~/scripts/{0}"._Format(scriptName));

            if (!Scripts.ContainsKey(scriptName) &&
                System.IO.File.Exists(scriptPath))
            {
                Scripts.Add(scriptName, System.IO.File.ReadAllText(scriptPath));
            }

            if (!MinScripts.ContainsKey(scriptName) &&
                System.IO.File.Exists(scriptPath))
            {
              JavaScriptCompressor compressor = new JavaScriptCompressor();
              MinScripts.Add(scriptName, compressor.Compress(Scripts[scriptName]));
            }

            Dictionary<string, string> container = min ? MinScripts : Scripts;
            return container;
        }

        private string FindScript(string scriptName)
        {
            string scriptPath = Server.MapPath("~/scripts/{0}"._Format(scriptName));
            if (!System.IO.File.Exists(scriptPath))
            {
                scriptPath = Server.MapPath("~/{0}"._Format(scriptName));
            }

            if (!System.IO.File.Exists(scriptPath))
            {
                scriptPath = null;
            }

            return scriptPath;
        }
    }
}
