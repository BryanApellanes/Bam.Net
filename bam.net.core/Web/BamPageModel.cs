using Bam.Net.CoreServices;
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
    public class BamPageModel : PageModel
    {
        public const string AppDataFolder = "AppData";

        public BamPageModel(ApplicationServiceRegistry serviceRegistry, IHostingEnvironment hostingEnvironment) : 
            this(serviceRegistry, hostingEnvironment, "json", "yaml")
        {
        }

        public BamPageModel(ApplicationServiceRegistry serviceRegistry, IHostingEnvironment hostingEnvironment, params string[] extensionsToLoad)
        {
            HostingEnvironment = hostingEnvironment;
            ExtensionsToLoad = new List<string>();
            ExtensionsToLoad.AddRange(extensionsToLoad);
            Files = new Dictionary<string, string>();
            JsonFiles = new Dictionary<string, string>();
            YamlFiles = new Dictionary<string, string>();
            CoreClient = new CoreClient();
            SetFileContents();
            SetFileContents(JsonFiles, "json");
            SetFileContents(YamlFiles, "yaml");
        }

        public virtual ActionResult OnGet()
        {
            return Page();
        }

        protected List<string> ExtensionsToLoad
        {
            get;
            set;
        }

        public CoreClient CoreClient { get; }

        public Dictionary<string, string> Files
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
        
        

        public IHostingEnvironment HostingEnvironment { get; set; }

        public string Message { get; set; }

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
