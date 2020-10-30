/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net;
using Bam.Net.Caching.File;
using Bam.Net.Javascript;
using Bam.Net.Logging;
using Bam.Net.Server.Renderers;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.UserAccounts.Data;
using Newtonsoft.Json.Linq;
using System.Reflection;
using Bam.Net.Server.Meta;
using Bam.Net.Data.Repositories;
using Bam.Net.Configuration;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    /// <summary>
    /// The primary responder for all content files found in ~s:/ (defined as BamServer.ContentRoot)
    /// </summary>
    public partial class ContentResponder : Responder, IInitialize<ContentResponder>
    {
        public void RefreshLayouts()
        {
            Task.Run(() =>
            {
                foreach (AppContentResponder appContent in AppContentResponders.Values.ToArray())
                {
                    appContent.LayoutModelsByPath.Clear();
                }
            });
        }
    }
}
