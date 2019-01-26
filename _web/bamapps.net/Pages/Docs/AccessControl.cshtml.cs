using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Presentation;
using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Docs
{
    public class AccessControlModel : DataPageModel
    {
        public AccessControlModel(IHostingEnvironment hostingEnvironment, ApplicationModel appModel) : 
            base(hostingEnvironment, appModel)
        {
        }
    }
}