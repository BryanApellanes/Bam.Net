/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.Mvc;
using Bam.Net;
using Bam.Net.Web;
using Bam.Net.ServiceProxy;
using Bam.Net.Configuration;

namespace Bam.Net.UserAccounts
{
    public class PageResult: RedirectResult
    {
        public PageResult(Uri url)
            : base(url.ToString())
        {
            PageName = Path.GetFileNameWithoutExtension(url.AbsolutePath);
            ApplicationNameProvider = DefaultConfigurationApplicationNameProvider.Instance;
        }

        public PageResult(string uri)
            : this(new Uri(uri))
        { }

        public IApplicationNameProvider ApplicationNameProvider { get; set; }

        public string PageName
        {
            get;
            set;
        }

        public void ExecuteResult()
        {

        }

        public virtual void ExecuteResult(IHttpContext context)
        {
            context.Response.Redirect(Url);
        }

        public override void ExecuteResult(ControllerContext context)
        {            
            context.HttpContext.Response.Redirect(Url);
        }
    }
}
