/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Bam.Net.Data
{
    public class DaoController: Controller
    {
        public ActionResult Default()
        {
            return JavaScript("alert('dao.default: You probably didn't intend for this');");
        }

        public ActionResult Proxies(bool min = false)
        {
            return new DaoProxyResult(min);
        }
    }
}
