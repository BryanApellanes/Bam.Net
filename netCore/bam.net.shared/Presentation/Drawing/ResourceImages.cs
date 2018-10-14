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

namespace Bam.Net.Drawing
{
    public static class ResourceImages
    {
        static List<string> _extensions;
        static Dictionary<string, Image> _images;
        static ResourceImages()
        {
            _extensions = new List<string>
            {
                ".bmp",
                ".png",
                ".jpg",
                ".gif"
            };

            _images = new Dictionary<string, Image>();
        }

        public static bool Has(string fileName)
        {
            return Get(fileName) != null;
        }

        public static bool Has(string fileName, out Image image)
        {
            image = Get(fileName);
            return image != null;
        }

        public static Image Get(string fileName)
        {
            return Get(fileName, Assembly.GetEntryAssembly());
        }

        public static Image Get(string fileName, Assembly assembly)
        {
            Load(assembly);
            if (_images.ContainsKey(fileName))
            {
                return _images[fileName];
            }

            return null;
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
                        loaded.Add(assemblyToLoad);
                        foreach (string resource in assemblyToLoad.GetManifestResourceNames())
                        {
                            string ext = Path.GetExtension(resource);

                            if (_extensions.Contains(ext))
                            {
                                Stream imageStream = assemblyToLoad.GetManifestResourceStream(resource);
                                Image image = Image.FromStream(imageStream);
                                string fileName = GetFileNameFromResourceName(resource);
                                if (!_images.ContainsKey(fileName))
                                {
                                    _images.Add(fileName, image);
                                }
                                else
                                {
                                    Log.AddEntry("A resource image named {0} has already been loaded, {1} will not be loaded", LogEventType.Warning, fileName, resource);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
