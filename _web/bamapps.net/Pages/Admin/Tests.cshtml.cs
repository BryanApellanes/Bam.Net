using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Bam.Net.Services;

namespace Bam.Net.Web.Pages.Admin
{
    public class TestsModel : BamTestPageModel
    {
        public TestsModel(ApplicationServiceRegistry applicationRegistry, IHostingEnvironment hostingEnvironment): base(applicationRegistry, hostingEnvironment, new string[] { "basetests/base-tests", "basetests/another" })
        {

        }



    }
}