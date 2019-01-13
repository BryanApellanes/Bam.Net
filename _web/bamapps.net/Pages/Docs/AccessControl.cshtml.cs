using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bam.Net.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bam.Net.Web.Pages.Docs
{
    public class AccessControlModel : BamPageModel
    {
        public AccessControlModel(ApplicationServiceRegistry serviceRegistry, IHostingEnvironment hostingEnvironment) : base(serviceRegistry, hostingEnvironment)
        {
        }
    }
}