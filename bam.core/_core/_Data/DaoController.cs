/*
	Copyright © Bryan Apellanes 2015  
*/
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public partial class DaoController: Controller
    {
        public ActionResult Default()
        {
            return Content("Dao Controller");
        }

        public ActionResult Proxies(bool min = false)
        {
            return new DaoProxyResult(min);
        }
    }
}
