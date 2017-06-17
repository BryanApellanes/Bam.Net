using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Incubation;
using Bam.Net.Logging;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Services.AsyncCallback;
using Bam.Net.Services.AsyncCallback.Data;
using Bam.Net.Web;

namespace Bam.Net.Services
{
    public abstract class AsyncProxyableService : ProxyableService, IHasServiceProvider
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
        }

        AsyncCallbackService _callbackService;
        object _callbackServiceLock = new object();
        public AsyncCallbackService CallbackService
        {
            get
            {
                return _callbackServiceLock.DoubleCheckLock(ref _callbackService, () => AsyncCallbackService.Current);
            }
            set
            {
                _callbackService = value;
            }
        }

        public Action<AsyncExecutionResponse> DefaultResponseHandler { get; set; }
        public Incubator ServiceProvider { get; set; }

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

                blocker.WaitOne();

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
            // TODO: check if the specified request has been made already by checking the hash
            // add a parameter to specify how old a response needs to be to require re-invoking the request
            responseHandler = responseHandler ?? DefaultResponseHandler;
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
                AsyncCallbackService asyncCallback = _proxyFactory.GetProxy<AsyncCallbackService>(request.RespondToHostName, request.RespondToPort);                
                ExecutionRequest execRequest = new ExecutionRequest(request.ClassName, request.MethodName, "json")
                {
                    ServiceProvider = ServiceProvider,
                    Context = HttpContext,
                    JsonParams = request.JsonParams
                };
                execRequest.Execute();
                asyncCallback.RecieveAsyncExecutionResponse(new AsyncExecutionResponse { Request = request, Result = execRequest.Result });
            });
        }
    }
}
