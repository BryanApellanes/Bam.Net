/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Reflection;
using System.IO;
using System.Drawing;
using Naizari.Extensions;
using Naizari.Helpers;
using Naizari.Logging;

namespace Naizari
{
    //TODO: Fix this class to properly handle all files (somehow).  Make sure all methods are thread safe
    public static class Resources
    {
        static Dictionary<string, string> javaScriptFiles;
        static Dictionary<string, string> javascriptFriendlyNamesToQualifiedScriptPath;
        static Dictionary<string, Bitmap> images;
        static Dictionary<string, string> textFiles;
        static Dictionary<string, string> textFilesByName;
        static Dictionary<string, string> sqlFiles;
        static Dictionary<string, string> depFiles;

        static Resources()
        {
            Resources.javaScriptFiles = new Dictionary<string, string>(50);
            Resources.javascriptFriendlyNamesToQualifiedScriptPath = new Dictionary<string, string>(50);
            Resources.images = new Dictionary<string, Bitmap>(50);
            Resources.textFiles = new Dictionary<string, string>(50);
            Resources.sqlFiles = new Dictionary<string, string>(50);
            Resources.depFiles = new Dictionary<string, string>(50);
            Resources.textFilesByName = new Dictionary<string, string>(50);
        }

        public static void LoadResources(Assembly assemblyToLoad)
        {
            Load(assemblyToLoad);
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

                            if (ext.ToLowerInvariant().Equals(".js"))
                            {
                                LoadJavascript(assemblyToLoad, resource);
                            }

                            LoadImages(assemblyToLoad, resource, ext);

                            LoadTextFiles(assemblyToLoad, resource, ext);

                            LoadSqlScripts(assemblyToLoad, resource, ext);

                            LoadDepFiles(assemblyToLoad, resource, ext);
                        }
                    }
                }
            }
        }

        public static string ReadTextFile(string fileName)
        {
            return ReadTextFile(fileName, Assembly.GetExecutingAssembly());
        }

        public static string ReadTextFile(string fileName, Assembly assemblyToReadFrom)
        {
            Load(assemblyToReadFrom);
            if (textFilesByName.ContainsKey(fileName))
                return textFilesByName[fileName];

            ExceptionHelper.Throw<FileNotFoundException>("The file {0} was not found in the specified assembly '{1}'", fileName, assemblyToReadFrom.FullName);
            return string.Empty;
        }

        #region private loading methods
        private static void LoadDepFiles(Assembly assemblyToLoad, string resource, string ext)
        {
            if (ext.ToLowerInvariant().Equals(".dep"))
            {
                Stream File = assemblyToLoad.GetManifestResourceStream(resource);
                using (File)
                {
                    using (StreamReader sr = new StreamReader(File))
                    {
                        if (!Resources.depFiles.ContainsKey(resource.ToLower()))
                        {
                            Resources.depFiles.Add(resource.ToLower(), sr.ReadToEnd());
                        }
                    }
                }
            }
        }

        private static void LoadSqlScripts(Assembly assemblyToLoad, string resource, string ext)
        {
            if (ext.ToLowerInvariant().Equals(".sql"))
            {
                Stream File = assemblyToLoad.GetManifestResourceStream(resource);
                using (File)
                {
                    using (StreamReader sr = new StreamReader(File))
                    {
                        if (!Resources.sqlFiles.ContainsKey(resource.ToLower()))
                        {
                            Resources.sqlFiles.Add(resource.ToLower(), sr.ReadToEnd());
                        }
                    }
                }
            }
        }

        private static void LoadTextFiles(Assembly assemblyToLoad, string resource, string ext)
        {
            ext = ext.ToLowerInvariant();
            if (ext.Equals(".txt") || ext.Equals(".ascx") || ext.Equals(".cs"))
            {

                Stream File = assemblyToLoad.GetManifestResourceStream(resource);
                string fileText = string.Empty;
                using (File)
                {
                    using (StreamReader sr = new StreamReader(File))
                    {
                        if (!Resources.textFiles.ContainsKey(resource.ToLower()))
                        {
                            fileText = sr.ReadToEnd();
                        }
                    }
                }

                if (!textFiles.ContainsKey(resource.ToLower()))
                    Resources.textFiles.Add(resource.ToLower(), fileText);
                string fileName = GetFileNameFromResourceName(resource.ToLower());
                if (!textFilesByName.ContainsKey(fileName))
                    Resources.textFilesByName.Add(fileName, fileText);
            }
        }

        static object imageFilesLock = new object();
        private static void LoadImages(Assembly assemblyToLoad, string resource, string ext)
        {
            if (ext.ToLowerInvariant().Equals(".gif") ||
                ext.ToLowerInvariant().Equals(".jpg") ||
                ext.ToLowerInvariant().Equals(".jpeg") ||
                ext.ToLowerInvariant().Equals(".png"))
            {
                lock (imageFilesLock)
                {
                    if (!Resources.images.ContainsKey(resource.ToLower()))
                    {
                        Bitmap image = (Bitmap)Bitmap.FromStream(assemblyToLoad.GetManifestResourceStream(resource));
                        Resources.images.Add(resource.ToLower(), image);
                    }
                }
            }
        }

        static object scriptFilesLock = new object();
        static object scriptFilesByNameLock = new object();

        public static void LoadJavascript(Assembly assemblyToLoad)
        {
            foreach (string resource in assemblyToLoad.GetManifestResourceNames())
            {
                string ext = Path.GetExtension(resource);

                if (ext.ToLowerInvariant().Equals(".js"))
                {
                    LoadJavascript(assemblyToLoad, resource);
                }
            }
        }

        private static void LoadJavascript(Assembly assemblyToLoad, string resource)
        {
            string fileName = GetFileNameFromResourceName(resource).ToLowerInvariant();

            Stream file = assemblyToLoad.GetManifestResourceStream(resource);
            using (file)
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    lock (scriptFilesLock)
                    {
                        if (!Resources.javaScriptFiles.ContainsKey(resource.ToLower()))
                            Resources.javaScriptFiles.Add(resource.ToLower(), sr.ReadToEnd());
                    }
                }
            }

            lock (scriptFilesByNameLock)
            {
                string qualifiedScriptPath = resource.ToLowerInvariant();
                if (Resources.javascriptFriendlyNamesToQualifiedScriptPath.ContainsKey(fileName))
                {
                    string current = resource.ToLowerInvariant();
                    string previous = Resources.javascriptFriendlyNamesToQualifiedScriptPath[fileName];
                    if (!current.Equals(previous))
                    {
                        Log.Default.AddEntry("A script named {0} with a different resource path has already been added, that script will not be available by friendly name. current: {1}, previous: {2}", LogEventType.Warning, fileName, current, previous);
                        Resources.javascriptFriendlyNamesToQualifiedScriptPath[fileName] = current;
                    }
                }
                else
                {
                    Resources.javascriptFriendlyNamesToQualifiedScriptPath.Add(fileName, qualifiedScriptPath);
                }
            }
        }

        private static string GetFileNameFromResourceName(string resource)
        {
            string[] splitName = resource.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            string fileName = string.Empty;
            if (splitName.Length >= 2)
                fileName = splitName[splitName.Length - 2] + "." + splitName[splitName.Length - 1];
            else
                fileName = splitName[splitName.Length];
            return fileName;
        }
        #endregion

        public static Dictionary<string, string> JavaScriptFriendlyNamesToQualifiedScriptPath
        {
            get { return Resources.javascriptFriendlyNamesToQualifiedScriptPath; }
        }

        public static Dictionary<string, string> JavaScript
        {
            get { return Resources.javaScriptFiles; }
        }

        public static Dictionary<string, Bitmap> Images
        {
            get { return Resources.images; }
        }

        public static Dictionary<string, string> TextFiles
        {
            get { return Resources.textFiles; }
        }

        public static Dictionary<string, string> TextFilesByName
        {
            get { return Resources.textFilesByName; }
        }

        public static Dictionary<string, string> SqlFiles
        {
            get { return Resources.sqlFiles; }
        }

        public static Dictionary<string, string> DepFiles
        {
            get { return Resources.depFiles; }
        }
    }
}
