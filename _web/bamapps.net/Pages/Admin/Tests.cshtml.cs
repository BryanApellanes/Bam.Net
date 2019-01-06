using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Bam.Net.Web.Models;
using Bam.Net.Services;

namespace Bam.Net.Web.Pages.Admin
{
    public class TestsModel : DataPageModel
    {
        public TestsModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment): base (applicationRegistry, hostingEnvironment, new string[] {"basetests/base-tests", "basetests/another"}, "json", "yaml")
        {

        }

        public override ActionResult OnGet()
        {
            if(Request.Query.ContainsKey("tests"))
            {
                string testsQuery = Request.Query["tests"];
                string[] tests = testsQuery.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(t=> t.Trim()).ToArray();
                TestFileNames = new List<string>(tests);
            }
            return base.OnGet();
        }

    }
}