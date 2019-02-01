using Bam.Net.CoreServices;
using Bam.Net.Presentation;
using Bam.Net.Services;
using Bam.Net.Services.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bam.Net.Web
{
    public class DataPageModel : BamPageModel
    {
        public const string AppDataFolder = "AppData";

        public DataPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel serviceRegistry) : 
            this(hostingEnvironment, serviceRegistry, "json", "yaml")
        {
        }

        public DataPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel appModel, params string[] extensionsToLoad) : base(hostingEnvironment, appModel)
        {
            HostingEnvironment = hostingEnvironment;
            ExtensionsToLoad = new List<string>();
            ExtensionsToLoad.AddRange(extensionsToLoad);
            Files = new Dictionary<string, string>();
            JsonFiles = new Dictionary<string, string>();
            YamlFiles = new Dictionary<string, string>();
            SetFileContents();
            SetFileContents(JsonFiles, "json");
            SetFileContents(YamlFiles, "yaml");
        }


        public virtual ActionResult OnGet()
        {
            foreach (string extension in ExtensionsToLoad)
            {
                SetFileContents(Files, extension);
            }
            if (ExtensionsToLoad.Contains("json"))
            {
                SetFileContents(JsonFiles, "json");
            }
            if (ExtensionsToLoad.Contains("yaml"))
            {
                SetFileContents(YamlFiles, "yaml");
            }
            if (ExtensionsToLoad.Contains("csv"))
            {
                SetFileContents(CsvFiles, "csv");
            }
            return Page();
        }

        protected List<string> ExtensionsToLoad
        {
            get;
            set;
        }
        
        public Dictionary<string, string> Files
        {
            get;
            set;
        }

        public Dictionary<string, string> CsvFiles
        {
            get;
            set;
        }

        public Dictionary<string, string> JsonFiles
        {
            get;
            set;
        }

        public Dictionary<string, string> YamlFiles
        {
            get;
            set;
        }
                
        private void SetFileContents()
        {
            SetFileContents(Files, "json");
            SetFileContents(Files, "yaml");            
        }

        private void SetFileContents(Dictionary<string, string> container, string fileType)
        {
            container.Clear();
            DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(HostingEnvironment.ContentRootPath, AppDataFolder));
            if (dataDir.Exists)
            {
                foreach (FileInfo file in dataDir.GetFiles($"*.{fileType}"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    string content = System.IO.File.ReadAllText(file.FullName);
                    container.Add(fileName, content);
                }
                DirectoryInfo subTypeDir = new DirectoryInfo(Path.Combine(dataDir.FullName, fileType));
                if (subTypeDir.Exists)
                {
                    foreach (FileInfo file in subTypeDir.GetFiles($"*.{fileType}"))
                    {
                        string fileName = Path.GetFileNameWithoutExtension(file.Name);
                        string content = System.IO.File.ReadAllText(file.FullName);
                        container.Add(fileName, content);
                    }
                }
            }
        }
    }
}
