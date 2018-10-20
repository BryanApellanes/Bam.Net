/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Reflection;
using System.IO;
using Bam.Net.Logging;

namespace Bam.Net
{
    public static class ResourceTextFiles
    {
        static Dictionary<string, string> _textFiles;
        static Dictionary<string, string> _textFilesByName;
        static List<string> _extensions;
        
        static ResourceTextFiles()
        {
            _textFiles = new Dictionary<string, string>();
            _textFilesByName = new Dictionary<string, string>();
            _extensions = new List<string>();
            _extensions.Add(".txt");
        }

        public static void LoadResources(Assembly assemblyToLoad)
        {
            Load(assemblyToLoad);
        }

        /// <summary>
        /// Adds an extension to be loaded when resource text 
        /// files are loaded
        /// </summary>
        /// <param name="ext"></param>
        public static void AddExtensionToLoad(string ext)
        {
            _extensions.Add(ext);
        }

        private static volatile List<Assembly> loaded = new List<Assembly>();
        private static object loadLock = new object();
        public static void Load(Assembly assemblyToLoad)
        {
            if (!loaded.Contains(assemblyToLoad))
            {
                lock (loadLock)
                {
                    if (!loaded.Contains(assemblyToLoad))
                    {
                        foreach (string resource in assemblyToLoad.GetManifestResourceNames())
                        {
                            string ext = Path.GetExtension(resource);
                            
                            LoadTextFiles(assemblyToLoad, resource, ext);
                        }
                    }
                }
            }
        }

        public static string ReadTextFile(string fileName)
        {
            return ReadTextFile(fileName, Assembly.GetEntryAssembly());
        }

        public static string ReadTextFile(string fileName, Assembly assemblyToReadFrom)
        {
            Load(assemblyToReadFrom);
            if (_textFilesByName.ContainsKey(fileName))
            {
                return _textFilesByName[fileName];
            }

            ExceptionHelper.Throw<FileNotFoundException>("The file {0} was not found in the specified assembly '{1}'", fileName, assemblyToReadFrom.FullName);
            return string.Empty;
        }

        #region private loading methods

        private static void LoadTextFiles(Assembly assemblyToLoad, string resource, string ext)
        {
            ext = ext.ToLowerInvariant();
            if (_extensions.Contains(ext))
            {
                LoadText(assemblyToLoad, resource, _textFiles, _textFilesByName);
            }
        }

        private static void LoadText(Assembly assemblyToLoad, string resource, Dictionary<string, string> files, Dictionary<string, string> filesByName)
        {
            Stream File = assemblyToLoad.GetManifestResourceStream(resource);
            string fileText = string.Empty;
            using (File)
            {
                using (StreamReader sr = new StreamReader(File))
                {
                    if (!files.ContainsKey(resource.ToLower()))
                    {
                        fileText = sr.ReadToEnd();
                        files.Add(resource.ToLower(), fileText);
                    }

                    string fileName = GetFileNameFromResourceName(resource.ToLower());
                    if (!filesByName.ContainsKey(fileName))
                    {
                        filesByName.Add(fileName, fileText);
                    }
                }
            }
        }

        private static string GetFileNameFromResourceName(string resource)
        {
            string[] splitName = resource.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            string fileName = string.Empty;
            if (splitName.Length >= 2)
            {
                fileName = splitName[splitName.Length - 2] + "." + splitName[splitName.Length - 1];
            }
            else
            {
                fileName = splitName[splitName.Length];
            }
            return fileName;
        }
        #endregion
        
        public static Dictionary<string, string> TextFiles
        {
            get { return _textFiles; }
        }

        public static Dictionary<string, string> TextFilesByName
        {
            get { return _textFilesByName; }
        }
    }
}
