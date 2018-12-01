using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bam.Net.Web.AppModules;

namespace Bam.Net.Web.Pages
{
    public class TestsModel : PageModel
    {
        public TestsModel(ApplicationServiceRegistry applicationRegistry)
        {
            ApplicationServiceRegistry = applicationRegistry;
        }

        public TestAppModule TestServiceModule
        {
            get
            {
                return ApplicationServiceRegistry.Get<TestAppModule>();
            }
        }

        public ApplicationServiceRegistry ApplicationServiceRegistry { get; set; }
    }
}
