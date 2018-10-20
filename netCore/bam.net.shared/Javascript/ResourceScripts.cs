/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Bam.Net.Javascript
{
    public class ResourceScripts
    {
        volatile static Dictionary<string, string> scripts;
        static List<string> loaded = new List<string>();
        static object scriptLock = new object();
        static ResourceScripts()
        {
            scripts = new Dictionary<string, string>();
            LoadScripts(typeof(ResourceScripts));
        }

        /// <summary>
        /// Loads embedded resouce scripts that are in the namespace path Bam.Net.Javascript
        /// </summary>
        public static void LoadScripts()
        {
            LoadScripts(typeof(ResourceScripts));
        }

        /// <summary>
        /// Loads all resource scripts in the namespace path of the specified type.
        /// </summary>
        /// <param name="type"></param>
        public static void LoadScripts(Type type)
        {
            string namespacePath = string.Format("{0}.", type.Namespace);
            LoadNamespaceScripts(type.Assembly, namespacePath);
        }

        /// <summary>
        /// Loads embedded scripts into memory from the specified
        /// assembly in the specified namespacePath
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="namespacePath"></param>
        public static void LoadNamespaceScripts(Assembly assembly, string namespacePath)
        {
            lock (scriptLock)
            {
                if (!namespacePath.EndsWith("."))
                {
                    namespacePath = string.Format("{0}.", namespacePath);
                }

                if (loaded.Contains(namespacePath))
                {
                    return;
                }
                else
                {
                    loaded.Add(namespacePath);
                }

                foreach (string fullScriptPath in assembly.GetManifestResourceNames())
                {
                    if (fullScriptPath.StartsWith(namespacePath) && fullScriptPath.EndsWith(".js"))
                    {
                        string scriptName = fullScriptPath.Substring(namespacePath.Length, fullScriptPath.Length - namespacePath.Length);
                        Stream resource = assembly.GetManifestResourceStream(fullScriptPath);
                        using (StreamReader script = new StreamReader(resource))
                        {
                            string js = script.ReadToEnd();
                            scripts.AddMissing(scriptName, js);
                        }
                    }
                }
            }
        }

        public static string Get(string scriptName)
        {
            LoadScripts();
            string value = string.Empty;

            if (scripts.ContainsKey(scriptName))
            {
                value = scripts[scriptName];
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
