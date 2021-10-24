using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;

namespace Bam.Net.Presentation.Handlebars
{
    public class HandlebarsDirectory
    {
        public static implicit operator DirectoryInfo(HandlebarsDirectory dir)
        {
            return dir.Directory;
        }

        public HandlebarsDirectory(DirectoryInfo directory, ILogger logger = null)
        {
            Args.ThrowIfNull(directory, "directory");
            FileExtension = "hbs";
            Directory = directory;
            Logger = logger ?? Log.Default;
            if (!directory.Exists)
            {
                Logger.Warning("Handlebars directory does not exist: {0}", _directory.FullName);
            }
        }

        public HandlebarsDirectory(string directoryPath, ILogger logger = null): this(new DirectoryInfo(directoryPath), logger)
        {
        }

        public ILogger Logger { get; }
        
        public Dictionary<string, Func<object, string>> Templates { get; private set; }

        public bool HasTemplate(string templateName)
        {
            return Templates.ContainsKey(templateName);
        }
        
        public HandlebarsDirectory CombineWith(params HandlebarsDirectory[] dirs)
        {
            Reload();
            HandlebarsDirectory combined = new HandlebarsDirectory(Directory);
            combined.CopyProperties(this);
            foreach(HandlebarsDirectory dir in dirs)
            { 
                dir.Reload();
                foreach (DirectoryInfo partialDir in dir.PartialsDirectories)
                {
                    combined.PartialsDirectories.Add(partialDir);
                }
                foreach(string key in dir.Templates.Keys)
                {
                    combined.Templates.AddMissing(key, dir.Templates[key]);
                }
            }
            combined.Reload();
            return combined;
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
                Templates.AddMissing(templateName, HandlebarsDotNet.Handlebars.Compile(source));
            }
        }

        public void AddPartial(string templateName, string source, bool reload = false)
        {
            if(PartialsDirectories == null)
            {
                AddPartialsDirectory(Path.Combine(Directory.FullName, "Partials"));
            }
            string filePath = Path.Combine(Directory.FullName, $"{templateName}.{FileExtension}");
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
            get => _directory;
            set => SetDirectory(value);
        }
        
        public void AddPartialsDirectory(string partialsDirectory)
        {
            if(PartialsDirectories == null)
            {
                PartialsDirectories = new HashSet<DirectoryInfo>
                {
                    new DirectoryInfo(partialsDirectory)
                };
            }
            else
            {
                PartialsDirectories.Add(new DirectoryInfo(partialsDirectory));
            }
            Reload();
        }
        
        public string FileExtension { get; set; }
        public HashSet<DirectoryInfo> PartialsDirectories { get; set; }
        readonly object _reloadLock = new object();
        bool _loaded = false;

        public bool IsLoaded => _loaded;

        public void Reload()
        {
            Load(true);
        }

        public void Load(bool reload)
        {
            if(!_loaded || reload)
            {
                lock (_reloadLock)
                {
                    Templates = new Dictionary<string, Func<object, string>>();
                    if (PartialsDirectories != null)
                    {
                        foreach (DirectoryInfo partialsDirectory in PartialsDirectories)
                        {
                            if (partialsDirectory.Exists)
                            {
                                foreach (FileInfo partial in partialsDirectory.GetFiles($"*.{FileExtension}"))
                                {
                                    string shortName = Path.GetFileNameWithoutExtension(partial.FullName);
                                    string longName = partial.FullName.Truncate($".{FileExtension}".Length);
                                    string content = partial.ReadAllText();
                                    HandlebarsDotNet.Handlebars.RegisterTemplate(shortName, content);
                                    HandlebarsDotNet.Handlebars.RegisterTemplate(longName, content);
                                }
                            }
                        }
                    }
                    if (Directory != null && Directory.Exists)
                    {
                        foreach (FileInfo file in Directory?.GetFiles($"*.{FileExtension}"))
                        {
                            AddCompiledTemplateFile(file);
                        }
                    }
                    _loaded = true;
                }
            }
        }

        public void AddCompiledTemplateFile(FileInfo file)
        {
            string shortName = Path.GetFileNameWithoutExtension(file.FullName);
            string longName = file.FullName.Truncate($".{FileExtension}".Length);
            string content = file.ReadAllText();
            Func<object, string> template = HandlebarsDotNet.Handlebars.Compile(content);
            Templates.AddMissing(shortName, template);
            Templates.AddMissing(longName, template);
        }

        private void SetDirectory(DirectoryInfo directory)
        {
            _directory = directory;
            if (PartialsDirectories == null)
            {
                AddPartialsDirectory(directory.FullName);
                if (!_directory.Exists)
                {
                    _directory.Create();
                }
                DirectoryInfo partials = _directory.GetDirectories("Partials").FirstOrDefault();
                if (partials != null)
                {
                    AddPartialsDirectory(partials.FullName);
                }
            }
            Reload();
        }
    }
}
