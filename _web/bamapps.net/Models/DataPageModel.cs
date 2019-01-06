
using System.Collections.Generic;
using System.IO;
using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Models {

    public class DataPageModel: PageModel
    {
        public DataPageModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment, params string[] extensionsToLoad)
        {
            ApplicationServiceRegistry = applicationRegistry;
            HostingEnvironment = hostingEnvironment;
            ExtensionsToLoad = new List<string>();
            ExtensionsToLoad.AddRange(extensionsToLoad);
            Files = new Dictionary<string, string>();
            JsonFiles = new Dictionary<string, string>();
            YamlFiles = new Dictionary<string, string>();
            TestFileNames = new List<string>();
        }

        public DataPageModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment, string[] testFileNames, params string[] extensionsToLoad): this(applicationRegistry, hostingEnvironment, extensionsToLoad)
        {
            TestFileNames.AddRange(testFileNames);
        }

        public ApplicationServiceRegistry ApplicationServiceRegistry { get; set; }

        public List<string> TestFileNames
        {
            get;
            set;
        }

        protected List<string> ExtensionsToLoad
        {
            get;
            set;
        }

        public IHostingEnvironment HostingEnvironment { get; set; }

        public string Message { get; set; }
        
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
            return Page();
        }

        private void SetFileContents(Dictionary<string, string> container, string fileType)
        {
            container.Clear();
            DirectoryInfo dataDir = new DirectoryInfo(Path.Combine(HostingEnvironment.ContentRootPath, "Data"));
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