using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services.AsyncCallback;
using Bam.Net.Services.AsyncCallback.Data;
using Bam.Net.Web;
using System.Reflection;

namespace Bam.Net.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Bam.Net.CoreServices.ApplicationProxyableService" />
    /// <seealso cref="Bam.Net.ServiceProxy.IHasServiceProvider" />
    public abstract partial class AsyncProxyableService
    {
        /// <summary>
        /// The method executed server side
        /// </summary>
        /// <param name="request"></param>
        public virtual void ExecuteRemoteAsync(AsyncExecutionRequest request) // this should be executing server side after encryption has been handled
        {
            Task.Run(() =>
            {
                AsyncCallbackService asyncCallback = _proxyFactory.GetProxy<AsyncCallbackService>(request.RespondToHostName, request.RespondToPort, Logger);
                // This executes server side after the SecureChannel has decrypted and validated, need to set IsInitialized to true to 
                // ensure the request doesn't reinitialize to a state where it believes it is an execution request
                // targeting SecureChannel since that is what is in the HttpContext.Request.Url
                ExecutionRequest execRequest = new ExecutionRequest
                {
                    ClassName = request.ClassName,
                    MethodName = request.MethodName,
                    Ext = "json",
                    ServiceProvider = ServiceProvider,
                    JsonParams = request.JsonParams,
                    IsInitialized = true,
                    Context = HttpContext
                };
                bool success = execRequest.Execute();
                AsyncExecutionResponse response = new AsyncExecutionResponse
                {
                    Success = success,
                    Request = request,
                    Result = execRequest.Result
                };
                asyncCallback.RecieveAsyncExecutionResponse(response);
            });
        }
    }
}
