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
    public class IndexModel : DataPageModel
    {
        public IndexModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment) : base(applicationRegistry, hostingEnvironment, ".json", ".yaml")
        {
        }

        public override ActionResult OnGet()
        {
            return Page();
        }
    }
}