/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Web;
using System.Net;

namespace Naizari.Extensions
{
    public static class HttpResponseExtensions
    {
        public static void WriteLine(HttpResponse response, string str)
        {
            
            response.Write(HttpContext.Current.Server.HtmlDecode(str) + "<br />");
        }
    }
}
