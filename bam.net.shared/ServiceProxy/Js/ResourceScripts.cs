/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using Yahoo.Yui.Compressor;

namespace Bam.Net.ServiceProxy.Js
{
    public class ResourceScripts
    {
        volatile static Dictionary<string, string> _scripts;
        static List<Assembly> _loaded = new List<Assembly>();
        static object scriptLock = new object();
        static ResourceScripts()
        {
            _scripts = new Dictionary<string, string>();
            foreach(string path in ScriptResourcePaths.Value)
            {
                LoadScripts(typeof(ResourceScripts).Assembly, path);
            }
        }

        /// <summary>
        /// Loads all resource scripts in the namespace path of the specified type.
        /// </summary>
        /// <param name="type"></param>
        public static void LoadScripts(Type type)
        {
            lock (scriptLock)
            {
                Assembly assembly = type.Assembly;
                if (_loaded.Contains(assembly))
                {
                    return;
                }
                _loaded.Add(assembly);

                string namespacePath = string.Format("{0}.", type.Namespace);
                LoadScripts(assembly, namespacePath);
            }
        }

        public static void LoadScripts(Assembly assembly, string resourceNamePrefix)
        {
            foreach (string fullScriptPath in assembly.GetManifestResourceNames())
            {
                string ext = Path.GetExtension(fullScriptPath);
                if (!ext.Equals(".js", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                string scriptName = fullScriptPath.Substring(resourceNamePrefix.Length, fullScriptPath.Length - resourceNamePrefix.Length);
                Stream resource = assembly.GetManifestResourceStream(fullScriptPath);
                JavaScriptCompressor jsc = new JavaScriptCompressor();
                using (StreamReader script = new StreamReader(resource))
                {
                    string js = script.ReadToEnd();
                    _scripts.AddMissing(scriptName, js);
                    _scripts.AddMissing(
                        string.Format("{0}.min.js", scriptName.Substring(0, scriptName.Length - 3)),
                        jsc.Compress(js)
                    );
                }
            }
        }

        public static string Get(string scriptName, bool min = false)
        {
            string value = string.Empty;
            if (min && !scriptName.Trim().ToLowerInvariant().EndsWith("min.js"))
            {
                string minScript = string.Format("{0}.min.js", scriptName.Substring(0, scriptName.Length - 3));
                if (_scripts.ContainsKey(minScript))
                {
                    scriptName = minScript;
                }
            }

            if (_scripts.ContainsKey(scriptName))
            {
                value = _scripts[scriptName];
            }
            return value;
        }

        public static string Get(string scriptName, Type type)
        {
            LoadScripts(type);
            return Get(scriptName);
        }
    }
}
