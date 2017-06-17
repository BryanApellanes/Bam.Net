using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.Data;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Data = Bam.Net.Services.AsyncCallback.Data;
using Dao = Bam.Net.Services.AsyncCallback.Data.Dao;
using Bam.Net.Services.AsyncCallback.Data;
using Bam.Net.Services.AsyncCallback.Data.Dao.Repository;

namespace Bam.Net.Services
{
    [Proxy("asyncCallbackSvc")]
    [Encrypt]
    public class AsyncCallbackService: ProxyableService
    {
        static Dictionary<string, Action<AsyncExecutionResponse>> _pendingRequests;
        static ServiceProxyServer _server;
        static object _serverLock = new object();
        static AsyncCallbackService()
        {
            _pendingRequests = new Dictionary<string, Action<AsyncExecutionResponse>>();
        }

        protected AsyncCallbackService() { }

        public AsyncCallbackService(AsyncCallbackRepository repo, AppConf conf) : base(repo, conf)
        {
            HostPrefix = new HostPrefix { HostName = Machine.Current.DnsName, Port = RandomNumber.Between(49152, 65535) };            
            AsyncCallbackRepository = repo;
        }
        
        public AsyncCallbackRepository AsyncCallbackRepository { get; set; }

        [Local]
        public void RegisterPendingAsyncExecutionRequest(AsyncExecutionRequest request, Action<AsyncExecutionResponse> handler)
        {
            Args.ThrowIfNull(request);
            Args.ThrowIfNullOrEmpty(request.Cuid);
            lock (_serverLock)
            {
                if (_server == null)
                {
                    _server = Current.Serve(Current.HostPrefix, Current.Logger);
                }
            }
            SaveExecutionData(request);
            _pendingRequests.Set(request.Cuid, handler);
        }

        object _popLock = new object();
        public virtual void RecieveAsyncExecutionResponse(AsyncExecutionResponse result)
        {
            Args.ThrowIfNull(result, "result");
            Args.ThrowIfNull(result.Request, "result.Request");
            string cuid = result.Request.Cuid;
            Action<AsyncExecutionResponse> action = ((r) => { });
            lock (_popLock)
            {
                if (_pendingRequests.ContainsKey(cuid))
                {
                    action = _pendingRequests[cuid];
                    _pendingRequests.Remove(cuid);
                }
                else
                {
                    Logger.Warning("Received AsyncExecutionResponse with no corresponding request: {0}", result.PropertiesToString());
                }
            }
            Task.Run(() => 
            {
                //SaveResponseData(result); //TODO: address the problems here
                action(result);
            });
        }

        static AsyncCallbackService _current;
        static object _currentLock = new object();
        public static AsyncCallbackService Current
        {
            get
            {
                return _currentLock.DoubleCheckLock(ref _current, () => new AsyncCallbackService(new AsyncCallbackRepository(), new AppConf()));
            }
            set
            {
                _current = value;
            }
        }

        public HostPrefix HostPrefix
        {
            get;set;
        }

        [Local]
        public AsyncExecutionRequest CreateRequest(Type type, string methodName, params object[] arguments)
        {
            return new AsyncExecutionRequest
            {
                RespondToHostName = HostPrefix.HostName,
                RespondToPort = HostPrefix.Port,
                Ssl = HostPrefix.Ssl,
                ClassName = type.Name,
                MethodName = methodName,
                JsonParams = ApiParameters.ParametersToJsonParamsArray(arguments).ToJson()
            };
        }

        public override object Clone()
        {
            AsyncCallbackService clone = new AsyncCallbackService(AsyncCallbackRepository, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        private void SaveResponseData(AsyncExecutionResponse response)
        {
            Args.ThrowIfNull(response, "response");
            Args.ThrowIfNull(response.Request, "response.Request");

            AsyncExecutionData executionData = AsyncCallbackRepository.OneAsyncExecutionDataWhere(c => c.RequestCuid == response.Request.Cuid);
            if(executionData == null)
            {
                Logger.Warning("Recieved response without corresponding ASYNCEXECUTIONDATA entry: \r\n{0}", response.PropertiesToString());
            }
            else
            {
                response.ResultJson = response.Result?.ToJson() ?? "";
                string responseHash = response.ResultJson.Sha256();

                executionData.ResponseCuid = response.Cuid;
                executionData.Responded = new Instant(DateTime.UtcNow);
                executionData.ResponseHash = responseHash;
                executionData.Success = true;

                AsyncCallbackRepository.Save(executionData);
            }

            AsyncExecutionRequestData requestData = AsyncCallbackRepository.OneAsyncExecutionRequestDataWhere(c => c.Cuid == response.Request.Cuid);
            if (requestData == null)
            {
                Logger.Warning("Received response without corresponding ASYNCEXECUTIONREQUESTDATA entry: \r\n{0}", response.PropertiesToString());
            }

            AsyncExecutionResponseData responseData = AsyncCallbackRepository.OneAsyncExecutionResponseDataWhere(c => c.Cuid == response.Cuid);
            if (responseData != null)
            {
                Logger.Warning("Received response that has already been recorded: {0}", response.PropertiesToString());
            }
            else
            {
                responseData = new AsyncExecutionResponseData
                {
                    RequestId = requestData.Id,
                    ResultJson = response.ResultJson
                };

                AsyncCallbackRepository.Save(responseData);
            }
        }

        private void SaveExecutionData(AsyncExecutionRequest request)
        {
            string requestHash = request.GetRequestHash();
            AsyncExecutionData executionData = AsyncCallbackRepository.OneAsyncExecutionDataWhere(c => c.RequestCuid == request.Cuid);
            if (executionData == null)
            {                
                executionData = new AsyncExecutionData
                {
                    RequestCuid = request.Cuid,
                    RequestHash = requestHash,
                    Requested = new Instant(DateTime.UtcNow)
                };
                AsyncCallbackRepository.Save(executionData);
            }
            else
            {
                Logger.Warning("AsyncExecutionRequest was already persisted: {0}", request.Cuid);
            }
            SaveRequestData(request, requestHash);
        }

        private void SaveRequestData(AsyncExecutionRequest request, string requestHash)
        {
            AsyncExecutionRequestData requestData = AsyncCallbackRepository.OneAsyncExecutionRequestDataWhere(c => c.Hash == requestHash);
            if (requestData == null)
            {
                requestData = new AsyncExecutionRequestData
                {
                    Hash = requestHash,
                    ClassName = request.ClassName,
                    MethodName = request.MethodName,
                    JsonParams = request.JsonParams
                };
                AsyncCallbackRepository.Save(requestData);
            }
        }
    }
}
