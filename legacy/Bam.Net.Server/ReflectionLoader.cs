/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Bam.Net.Logging;

namespace Bam.Net.Server
{
    public class ReflectionLoader
    {
        /// <summary>
        /// The names of types to load
        /// </summary>
        public string[] TypesToLoad { get; set; }

        /// <summary>
        /// The search pattern used to identify assembly
        /// files
        /// </summary>
        public string AssemblySearchPatterns { get; set; }

        /// <summary>
        /// A list of directory paths to search
        /// </summary>
        public string[] Paths { get; set; }

        /// <summary>
        /// If true, Load will do .GetTypes().Where(...)
        /// </summary>
        public bool SearchClassNames { get; set; }

        public Type[] Load(ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            List<Type> results = new List<Type>();
            Paths.Each(path =>
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles(AssemblySearchPatterns.DelimitSplit(",", "|"));
                    files.Each(file =>
                    {
                        try
                        {
                            Assembly assembly = Assembly.LoadFrom(file.FullName);
                            if (SearchClassNames)
                            {
                                results.AddRange(SearchAssembly(assembly));
                            }
                            else
                            {
                                results.AddRange(GetTypes(assembly, logger));
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.AddEntry("An exception occurred loading file {0}: {1}", ex, file.FullName, ex.Message);
                        }
                    });
                }
            });

            return results.ToArray();
        }

        private Type[] GetTypes(Assembly assembly, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            List<Type> results = new List<Type>();
            TypesToLoad.Each(typeName =>
            {
                Type type = assembly.GetType(typeName);
                if (type != null)
                {
                    results.Add(type);
                    logger.AddEntry("Type FOUND: Type = {0}, Assembly = {1}", typeName, assembly.FullName);
                }
                else
                {
                    logger.AddEntry("Type NOT found: Type = {0}, Assembly = {1}", LogEventType.Warning, typeName, assembly.FullName);
                }
            });
            return results.ToArray();
        }

        private Type[] SearchAssembly(Assembly assembly, ILogger logger = null)
        {
            logger = logger ?? Log.Default;
            List<string> typesToLoad = new List<string>(TypesToLoad);
            Type[] types = assembly.GetTypes().Where(t => 
                typesToLoad.Contains(t.AssemblyQualifiedName) || 
                typesToLoad.Contains(t.Name) || 
                typesToLoad.Contains(t.FullName)
            ).ToArray();

            logger.AddEntry("Specified {0} types to load; Found {1}", TypesToLoad.Length.ToString(), types.Length.ToString());
            return types;
        }
    }
}
