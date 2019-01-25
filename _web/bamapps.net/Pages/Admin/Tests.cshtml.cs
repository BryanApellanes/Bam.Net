using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Bam.Net.Services;
using Bam.Net.Presentation;

namespace Bam.Net.Web.Pages.Admin
{
    public class TestsModel : BamTestPageModel
    {
        public TestsModel(IHostingEnvironment hostingEnvironment, ApplicationModel appModel) : base(hostingEnvironment, appModel, new string[] { "basetests/base-tests", "basetests/another" })
        {

        }



    }
}