using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using Bam.Net.Presentation.Html;
using Bam.Net.Logging;
using Bam.Net;
using Bam.Net.Incubation;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Data;
using Bam.Net.Configuration;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using System.IO;
using Bam.Net.UserAccounts;
using Bam.Net.Server;
using Bam.Net.Server.Listeners;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;
using System.Reflection;
using Bam.Net.Server.Renderers;
using Bam.Net.Presentation;

namespace Bam.Net.Server
{
    /// <summary>
    /// The core BamServer
    /// </summary>
    public partial class BamServer // core
    {
        protected void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            IHttpContext wrapper = new HttpContextWrapper(context);//new HttpContextWrapper(new RequestWrapper(request), new ResponseWrapper(response));
            HandleRequestAsync(wrapper);
        }

        private void HandleException(IHttpContext context, Exception ex)
        {
            IResponse response = context.Response;
            IRequest request = context.Request;
            if (response.OutputStream != null)
            {
                using (StreamWriter sw = new StreamWriter(response.OutputStream))
                {

                    string description = "({0})"._Format(ex.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.StatusDescription = description;
                    sw.Write(@"<!DOCTYPE html>
<html>
<body>
<h1>Internal Server Exception</h1>
<p>" + description + "</p></body></html>");
                    sw.Flush();

                }

            }
            MainLogger.AddEntry("An error occurred handling the request: ({0})\r\n*** Request Details {1}***\r\n{2}\r\n\r\n{3}",
                    ex,
                    request.GetClientIp(),
                    ex.Message,
                    request.TryPropertiesToString(),
                    Args.GetMessageAndStackTrace(ex));
        }

        private void HandleResponderNotFound(IHttpContext context)
        {
            IResponse response = context.Response;
            IRequest request = context.Request;

            string path = request.Url.ToString();
            string messageFormat = "No responder was found for the path: {0}";
            string description = "Responder not found";

            using (StreamWriter sw = new StreamWriter(response.OutputStream))
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                response.StatusDescription = description;
                sw.Write(@"<!DOCTYPE html>
<html>
<body>
<h1>" + description + @"
<p>" + string.Format(messageFormat, path) + @"</p>
");
                sw.Flush();
                sw.Close();
            }
            string logMessageFormat = "[ClientIp: {0}] No responder found for the path: {1}";
            MainLogger.AddEntry(logMessageFormat, LogEventType.Warning, request.GetClientIp(), path);
        }
    }
}
