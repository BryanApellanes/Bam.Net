/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace Naizari.Javascript
{
    /// <summary>
    /// This class is not used, but in theory it can be registered in the config file
    /// as an HttpHandler.  This is untested.
    /// </summary>
    public class JavascriptInvocationHandler: IHttpHandler, IRequiresSessionState
    {

        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            JavascriptServer.ProcessRequest(context);
        }

        #endregion
    }
}
