
using System.Collections.Generic;
using System.IO;
using Bam.Net.Presentation.Handlebars;
using Bam.Net.Services;
using Bam.Net.Services.Clients;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Presentation
{
    public class RazorPageModel: Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        public RazorPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel) : 
            this(hostingEnvironment, applicationModel, "json", "yaml", "xml", "csv", "txt")
        {
        }

        public RazorPageModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel, params string[] extensionsToLoad)
        {
            CoreClient = new CoreClient();
            ApplicationModel = applicationModel;
            HostingEnvironment = hostingEnvironment;            
        }
        
        public string Message { get; set; }

        public CoreClient CoreClient { get; }

        public ApplicationModel ApplicationModel { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }

        public HandlebarsDirectory HandlebarsDirectory
        {
            get
            {
                return new HandlebarsDirectory(Path.Combine(HostingEnvironment.ContentRootPath, "wwwroot", "handlebars"));
            }
        }

        protected void SetFileContents(Dictionary<string, string> container, string fileType)
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