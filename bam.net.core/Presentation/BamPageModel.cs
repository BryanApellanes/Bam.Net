
using System.Collections.Generic;
using System.IO;
using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Presentation
{
    public class BamPageModel: PageModel
    {
        public BamPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel) : 
            this(hostingEnvironment, applicationModel, "json", "yaml", "xml", "csv", "txt")
        {
        }

        public BamPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel, params string[] extensionsToLoad)
        {
            ApplicationModel = applicationModel;
            HostingEnvironment = hostingEnvironment;
            ExtensionsToLoad = new List<string>();
            ExtensionsToLoad.AddRange(extensionsToLoad);
            Files = new Dictionary<string, string>();
            JsonFiles = new Dictionary<string, string>();
            YamlFiles = new Dictionary<string, string>();
            CsvFiles = new Dictionary<string, string>();
            TestFileNames = new List<string>();
        }

        public BamPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel, string[] testFileNames, params string[] extensionsToLoad): this(hostingEnvironment, applicationModel, extensionsToLoad)
        {
            TestFileNames.AddRange(testFileNames);
        }
        
        public string Message { get; set; }

        public ApplicationModel ApplicationModel { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }

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

        public Dictionary<string, string> CsvFiles
        {
            get;
            set;
        }

        public List<string> TestFileNames
        {
            get;
            set;
        }

        public virtual ActionResult OnGet()
        {
            foreach(string extension in ExtensionsToLoad)
            {
                SetFileContents(Files, extension);
            }
            if(ExtensionsToLoad.Contains("json"))
            {
                SetFileContents(JsonFiles, "json");
            }
            if(ExtensionsToLoad.Contains("yaml"))
            {
                SetFileContents(YamlFiles, "yaml");
            }
            if(ExtensionsToLoad.Contains("csv"))
            {
                SetFileContents(CsvFiles, "csv");
            }
            return Page();
        }

        private void SetFileContents(Dictionary<string, string> container, string fileType)
        {
            container.Clear();
            DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(HostingEnvironment.ContentRootPath, "AppData"));
            if(dataDir.Exists)
            {                
                foreach (FileInfo file in dataDir.GetFiles($"*.{fileType}"))
                {
                    string fileName = Path.GetFileNameWithoutExtension(file.Name);
                    string content = System.IO.File.ReadAllText(file.FullName);
                    container.Add(fileName, content);
                }
                DirectoryInfo subTypeDir = new DirectoryInfo(Path.Combine(dataDir.FullName, fileType));
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