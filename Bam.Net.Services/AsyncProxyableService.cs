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
    public abstract class AsyncProxyableService : ApplicationProxyableService, IHasServiceProvider
    {
        ProxyFactory _proxyFactory;

        public AsyncProxyableService(AsyncCallbackService callbackService, DaoRepository repository, AppConf appConf) : base(repository, appConf)
        {
            Init(callbackService);
        }

        protected AsyncProxyableService()
        {
            Init();
        }

        private void Init(AsyncCallbackService callbackService = null)
        {
            CallbackService = callbackService;
            _proxyFactory = new ProxyFactory();
            DefaultResponseHandler = ((r) => { Logger.Info("AsyncResponseRecieved: {0}", r.PropertiesToString()); });
            AsyncWaitTimeout = 1000 * 60 * 5; // 5 minutes
        }

        public AsyncCallbackService CallbackService { get; set; }

        public Action<AsyncExecutionResponse> DefaultResponseHandler { get; set; }
        public Incubator ServiceProvider { get; set; }

        /// <summary>
        /// The number of milliseconds to wait for async tasks to complete
        /// </summary>
        public int AsyncWaitTimeout { get; set; }

        /// <summary>
        /// Invoke the specified method asynchronously and unwrap the response.  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        [Local]
        public Task<T> InvokeAsync<T>(string methodName, params object[] arguments)
        {
            return Task.Run<T>(() =>
            {
                Task<AsyncExecutionResponse> task = InvokeAsync(methodName, arguments);
                task.Wait(AsyncWaitTimeout);

                if(task.Result?.ResultJson != null)
                {
                    task.Result.Result = task.Result.ResultJson.FromJson<T>();
                }   
                
                return (T)(task.Result?.Result);
            });
        }

        /// <summary>
        /// Invoke the specified method asynchronously and return the wrapped response.
        /// If the call succeeded without errors on the server the Task.Result.Result 
        /// will be the result of the method call.  If validation failed the 
        /// Task.Result.Result will be the ValidationResult containing additional information
        /// about the validation failure(s).  If an exception occurred the Task.Result.Result
        /// will be the message of the exception that occurred.
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        [Local]
        public Task<AsyncExecutionResponse> InvokeAsync(string methodName,  params object[] arguments)
        {
            return Task.Run(() =>
            {
                AutoResetEvent blocker = new AutoResetEvent(false);
                AsyncExecutionResponse result = null;                
                InvokeAsync((r) =>
                {
                    result = r;
                    blocker.Set();
                }, methodName, arguments);

                blocker.WaitOne(AsyncWaitTimeout);

                return result;
            });
        }

        [Local]
        public void InvokeAsync(Action<AsyncExecutionResponse> responseHandler, string methodName, params object[] arguments)
        {
            InvokeAsync(CallbackService.CreateRequest(GetProxiedType(), methodName, arguments), responseHandler);
        }

        [Local]
        public void InvokeAsync(AsyncExecutionRequest request, Action<AsyncExecutionResponse> responseHandler = null)
        {
            responseHandler = responseHandler ?? DefaultResponseHandler;
            if (!request.UseCachedResponse)
            {
                CallExecuteRemoteAsync(request, responseHandler);
            }
            else
            {
                AsyncExecutionResponseData response = CallbackService.GetCachedResponse(request.GetRequestHash());
                if (response != null && new Instant(DateTime.UtcNow).DiffInMinutes(response.Created.Value) <= request.ResponseMaxAgeInMinutes)
                {
                    AsyncExecutionResponse result = response.CopyAs<AsyncExecutionResponse>();
                    result.Success = true;
                    result.Request = request;
                    result.ResultJson = response.ResultJson;
                    responseHandler(result);
                }
                else
                {
                    if (response != null)
                    {
                        CallbackService.AsyncCallbackRepository.Delete(response);
                    }
                    CallExecuteRemoteAsync(request, responseHandler);
                }
            }
        }

        private void CallExecuteRemoteAsync(AsyncExecutionRequest request, Action<AsyncExecutionResponse> responseHandler)
        {
            CallbackService.RegisterPendingAsyncExecutionRequest(request, responseHandler);
            ExecuteRemoteAsync(request);
        }

        /// <summary>
        /// The method executed server side
        /// </summary>
        /// <param name="request"></param>
        public virtual void ExecuteRemoteAsync(AsyncExecutionRequest request) // this should be executing server side after encryption has been handled
        {
            Task.Run(() =>
            {
                AsyncCallbackService asyncCallback = _proxyFactory.GetProxy<AsyncCallbackService>(request.RespondToHostName, request.RespondToPort, Logger);
                ExecutionRequest execRequest = new ExecutionRequest(request.ClassName, request.MethodName, "json")
                {
                    ServiceProvider = ServiceProvider,
                    Context = HttpContext,
                    JsonParams = request.JsonParams
                };
                bool success = execRequest.Execute();
                AsyncExecutionResponse response = new AsyncExecutionResponse
                {
                    Success = success,
                    Request = request,
                    Result = execRequest.Result
                };
                if (!success)
                {
                    if (execRequest?.Result is ValidationResult validation && validation.Success == false)
                    {
                        response.ValidationFailure = new ValidationFailure { Message = validation.Message, Failures = validation.ValidationFailures };
                        response.Result = null;
                    }
                }
                asyncCallback.RecieveAsyncExecutionResponse(response);
            });
        }
    }
}
