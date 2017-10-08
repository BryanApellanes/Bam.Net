/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
using System.Web;
using Bam.Net.Documentation;
using Bam.Net.ServiceProxy;
using Bam.Net;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.Configuration;
using Bam.Net.Incubation;
using Bam.Net.Presentation.Html;

namespace Bam.Net.Server
{
    public class DocsController: Controller
    {
        static DocsController()
        {
            ClassDocumentationResult.DefaultRenderer = (infos, output) =>
            {
                Tag container = new Tag("div");
                infos.Keys.Each(type =>
                {

                    //container.Child(new Tag("h1").Text(type.FullName));
                    //infos[type]
                });
            };
        }
        public ActionResult Get(string[] classNames = null)
        {
            if (classNames == null)
            {
                classNames = ServiceProxySystem.Incubator.ClassNames;
            }

            return new ClassDocumentationResult(classNames);
        }
    }
}
