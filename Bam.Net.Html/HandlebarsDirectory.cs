using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Html
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
            _templates = new Dictionary<string, Func<object, string>>();
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

        public void AddTemplate(string name, string source, bool reload = false)
        {
            string filePath = Path.Combine(Directory.FullName, $"{name}.{FileExtension}");
            source.SafeWriteToFile(filePath, true);
            if (reload)
            {
                Reload();
            }
            else
            {
                _templates.AddMissing(name, Handlebars.Compile(source));
            }
        }

        public void AddPartial(string name, string source, bool reload = false)
        {
            if(PartialsDirectory == null)
            {
                SetPartialsDirectory(Path.Combine(Directory.FullName, "Partials"));
            }
            string filePath = Path.Combine(PartialsDirectory.FullName, $"{name}.{FileExtension}");
            source.SafeWriteToFile(filePath, true);
            if (reload)
            {
                Reload();
            }
            else
            {
                Handlebars.RegisterTemplate(name, source);
            }
        }

        public string Render(string templateName, object data)
        {
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
        public void Reload()
        {
            lock (_reloadLock)
            {
                if (PartialsDirectory != null)
                {
                    _templates = new Dictionary<string, Func<object, string>>();
                    foreach (FileInfo partial in PartialsDirectory.GetFiles($"*.{FileExtension}"))
                    {
                        string name = Path.GetFileNameWithoutExtension(partial.FullName);
                        Handlebars.RegisterTemplate(name, partial.ReadAllText());
                    }
                }
                if(Directory != null)
                {
                    foreach (FileInfo file in Directory?.GetFiles($"*.{FileExtension}"))
                    {
                        string name = Path.GetFileNameWithoutExtension(file.FullName);
                        _templates.AddMissing(name, Handlebars.Compile(file.ReadAllText()));
                    }
                }
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
