﻿using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsDirectory
    {
        public static implicit operator DirectoryInfo(HandlebarsDirectory dir)
        {
            return dir.Directory;
        }
        Dictionary<string, Func<object, string>> _templates;
        public HandlebarsDirectory(DirectoryInfo directory)
        {
            FileExtension = "hbs";
            Directory = directory;
        }

        public HandlebarsDirectory(string directoryPath): this(new DirectoryInfo(directoryPath))
        {
        }

        public Dictionary<string, Func<object, string>> Templates
        {
            get
            {
                return _templates;
            }
        }

        public void AddTemplate(string templateName, string source, bool reload = false)
        {
            string filePath = Path.Combine(Directory.FullName, $"{templateName}.{FileExtension}");
            source.SafeWriteToFile(filePath, true);
            if (reload)
            {
                Reload();
            }
            else
            {
                _templates.AddMissing(templateName, HandlebarsDotNet.Handlebars.Compile(source));
            }
        }

        public void AddPartial(string templateName, string source, bool reload = false)
        {
            if(PartialsDirectory == null)
            {
                SetPartialsDirectory(Path.Combine(Directory.FullName, "Partials"));
            }
            string filePath = Path.Combine(PartialsDirectory.FullName, $"{templateName}.{FileExtension}");
            source.SafeWriteToFile(filePath, true);
            if (reload)
            {
                Reload();
            }
            else
            {
                HandlebarsDotNet.Handlebars.RegisterTemplate(templateName, source);
            }
        }

        public string Render(string templateName, object data)
        {
            if (!_loaded)
            {
                Reload();
            }
            if (Templates.ContainsKey(templateName))
            {
                return Templates[templateName](data);
            }
            return string.Empty;
        }
        DirectoryInfo _directory;
        public DirectoryInfo Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                SetDirectory(value);
            }
        }
        public void SetPartialsDirectory(string partialsDirectory)
        {
            PartialsDirectory = new DirectoryInfo(partialsDirectory);
            Reload();
        }
        public string FileExtension { get; set; }
        public DirectoryInfo PartialsDirectory { get; set; }
        object _reloadLock = new object();
        bool _loaded = false;
        public void Reload()
        {
            lock (_reloadLock)
            {
                _templates = new Dictionary<string, Func<object, string>>();
                if (PartialsDirectory != null)
                {
                    foreach (FileInfo partial in PartialsDirectory.GetFiles($"*.{FileExtension}"))
                    {
                        string name = Path.GetFileNameWithoutExtension(partial.FullName);
                        HandlebarsDotNet.Handlebars.RegisterTemplate(name, partial.ReadAllText());
                    }
                }
                if(Directory != null)
                {
                    foreach (FileInfo file in Directory?.GetFiles($"*.{FileExtension}"))
                    {
                        string name = Path.GetFileNameWithoutExtension(file.FullName);
                        _templates.AddMissing(name, HandlebarsDotNet.Handlebars.Compile(file.ReadAllText()));
                    }
                }
                _loaded = true;
            }
        }
        private void SetDirectory(DirectoryInfo directory)
        {
            _directory = directory;
            if (PartialsDirectory == null)
            {
                if (!_directory.Exists)
                {
                    _directory.Create();
                }
                DirectoryInfo partials = _directory.GetDirectories("Partials").FirstOrDefault();
                if (partials != null)
                {
                    PartialsDirectory = new DirectoryInfo(partials.FullName);
                }
            }
            Reload();
        }
    }
}
