using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Services;
using Bam.Net.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Admin
{
    public class ConfigurationModel : DataPageModel
    {
        public ConfigurationModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment, params string[] extensionsToLoad) : base(applicationRegistry, hostingEnvironment, extensionsToLoad)
        {
        }

        public ConfigurationModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment, string[] testFileNames, params string[] extensionsToLoad) : base(applicationRegistry, hostingEnvironment, testFileNames, extensionsToLoad)
        {
        }

        public IActionResult OnGet()
        {
            return base.OnGet();
        }
    }
}