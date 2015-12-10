/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Naizari.Helpers;
using System.Web.SessionState;

namespace Naizari.Logging
{
    public class HttpModuleLogger: IHttpModule, IRequiresSessionState
    {
        public HttpModuleLogger()
        {
        }

        #region IHttpModule Members

        public void Dispose()
        {
            
        }

        public void Init(HttpApplication context)
        {
            context.Error += new EventHandler(contextError);
            context.AuthenticateRequest += new EventHandler(contextAuthenticateRequest);
            context.AcquireRequestState += new EventHandler(contextAcquireRequestState);

            
        }

        void contextAcquireRequestState(object sender, EventArgs e)
        {
            try
            {
                HttpApplication application = (HttpApplication)sender;

                if (application.Context.Session != null)
                {
                    string sessionId = application.Context.Session.SessionID;
                    string userName = UserUtil.GetCurrentUser();
                    string page = application.Context.Request.RawUrl;
                    Log.Default.AddEntry("SESSIONID:{0};USERNAME:{1};PAGE:{2}", new string[] { sessionId, userName, page });
                }
            }
            catch { }
        }

        void contextAuthorizeRequest(object sender, EventArgs e)
        {
            try
            {
                HttpApplication application = (HttpApplication)sender;

                if (Log.Default is XmlLogger)
                    ((XmlLogger)Log.Default).EntriesPerFile = 20;

                Log.Default.AddEntry("{0} was authorized. \r\nURL: {1}\r\nUserAgent: {2}", new string[] { UserUtil.GetCurrentUser(), application.Request.RawUrl, application.Request.UserAgent });
            }
            catch { }
        }

        void contextAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                //HttpApplication application = (HttpApplication)sender;

                if (Log.Default is XmlLogger)
                    ((XmlLogger)Log.Default).EntriesPerFile = 20;

                Log.Default.AddEntry("{0} was authenticated. URL: {1}", new string[] { UserUtil.GetCurrentUser(), HttpContext.Current.Request.RawUrl });

            }
            catch { }
        }

        void contextError(object sender, EventArgs e)
        {
            try
            {
                HttpApplication application = (HttpApplication)sender;
                Exception exception = application.Server.GetLastError();

                if (exception != null)
                {
                    HttpRequest request = application.Context.Request;
                    string message = request.RawUrl + "\r\n" + exception.Message;
                    Log.Default.AddEntry(exception.Message, exception);
                 
                }
            }
            catch { }
        }

        #endregion
    }
}
