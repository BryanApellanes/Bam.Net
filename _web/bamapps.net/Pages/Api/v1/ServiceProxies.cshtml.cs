using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Api.v1
{
    public class ServiceProxiesModel : Presentation.BamPageModel
    {
        public ServiceProxiesModel(IHostingEnvironment hostingEnvironment, ApplicationModel applicationModel) 
            : base(hostingEnvironment, applicationModel)
        {
        }

        public override ActionResult OnGet()
        {
            return base.OnGet();
        }
    }
}