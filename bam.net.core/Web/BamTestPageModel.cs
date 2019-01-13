using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Web
{
    public class BamTestPageModel: BamPageModel
    {
        public BamTestPageModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment, string[] testFileNames)
            : base(applicationRegistry, hostingEnvironment, "json", "yaml", "txt", "js")
        {
            TestFileNames = new List<string>(testFileNames);
        }

        public List<string> TestFileNames { get; private set; }
            
        public override ActionResult OnGet()
        {
            if (Request.Query.ContainsKey("tests"))
            {
                string testsQuery = Request.Query["tests"];
                string[] tests = testsQuery.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray();
                TestFileNames = new List<string>(tests);
            }
            return base.OnGet();
        }
    }
}
