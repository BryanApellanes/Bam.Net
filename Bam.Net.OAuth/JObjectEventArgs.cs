/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Web.Security;
using System.Net;
using Newtonsoft.Json.Linq;

namespace Bam.Net.OAuth
{
    public class JObjectEventArgs: EventArgs
    {
        public JObjectEventArgs(JObject j)
        {
            this.JObject = j;
        }

        public JObject JObject { get; set; }
    }
}
