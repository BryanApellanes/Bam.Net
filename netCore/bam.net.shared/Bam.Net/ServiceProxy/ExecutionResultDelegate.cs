/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Bam.Net.Incubation;
using System.Reflection;

namespace Bam.Net.ServiceProxy
{
    public delegate ActionResult ExecutionResultDelegate(ExecutionRequest request);
}
