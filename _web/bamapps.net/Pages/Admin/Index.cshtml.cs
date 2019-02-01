using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Admin
{
    public class IndexModel : DataPageModel
    {
        public IndexModel(IHostingEnvironment hostingEnvironment, ApplicationModel appModel) : base(hostingEnvironment, appModel, ".json", ".yaml")
        {
        }

        public override ActionResult OnGet()
        {
            return Page();
        }
    }
}