using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.CoreServices;
using Bam.Net.CoreServices.ApplicationRegistration;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Data = Bam.Net.Services.AsyncCallback.Data;
using Dao = Bam.Net.Services.AsyncCallback.Data.Dao;
using Bam.Net.Services.AsyncCallback.Data;
using Bam.Net.Services.AsyncCallback.Data.Dao.Repository;

namespace Bam.Net.Services
{
    /// <summary>
    /// A service that will catch async responses
    /// </summary>
    [Proxy("asyncCallbackSvc")]
    [Encrypt]
    public class AsyncCallbackService: ProxyableService
    {
        Dictionary<string, Action<AsyncExecutionResponse>> _pendingRequests;
        ServiceProxyServer _server;
        object _serverLock = new object();

        protected AsyncCallbackService() { }

        public AsyncCallbackService(AsyncCallbackRepository repo, AppConf conf) : base(repo, conf)
        {
            HostPrefix = new HostPrefix { HostName = Machine.Current.DnsName, Port = RandomNumber.Between(49152, 65535) };                 
            AsyncCallbackRepository = repo;
            _pendingRequests = new Dictionary<string, Action<AsyncExecutionResponse>>();
        }

        public override object Clone()
        {
            AsyncCallbackService clone = new AsyncCallbackService(AsyncCallbackRepository, AppConf);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            clone._pendingRequests = _pendingRequests;
            clone._server = _server;
            clone._serverLock = _serverLock;
            return clone;
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
                    _server = this.Serve(HostPrefix, Logger);
                }
            }
            SaveExecutionRequestData(request);
            _pendingRequests.Set(request.Cuid, handler);
        }

        object _popLock = new object();
        public virtual void RecieveAsyncExecutionResponse(AsyncExecutionResponse result) // called by the server side to send responses
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
                SaveResponseData(result); 
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
        public AsyncExecutionResponseData GetCachedResponse(string requestHash)
        {
            return AsyncCallbackRepository.OneAsyncExecutionResponseDataWhere(c => c.RequestHash == requestHash);
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

        private void SaveResponseData(AsyncExecutionResponse response)
        {
            Args.ThrowIfNull(response, "response");
            Args.ThrowIfNull(response.Request, "response.Request");
            response.ResultJson = response.Result?.ToJson() ?? "";
            string responseHash = response.ResultJson.Sha256();

            AsyncExecutionData executionData = AsyncCallbackRepository.OneAsyncExecutionDataWhere(c => c.RequestCuid == response.Request.Cuid);
            if(executionData == null)
            {
                Logger.Warning("Recieved response without corresponding ASYNCEXECUTIONDATA entry: \r\n{0}", response.PropertiesToString());
            }
            else
            {
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
                    ResultJson = response.ResultJson,
                    ResponseHash = responseHash,
                    RequestHash = requestData.RequestHash
                };

                AsyncCallbackRepository.Save(responseData);
            }
        }

        private void SaveExecutionRequestData(AsyncExecutionRequest request)
        {
            string requestHash = request.GetRequestHash();
            AsyncExecutionData executionData = AsyncCallbackRepository.OneAsyncExecutionDataWhere(c => c.RequestHash == requestHash);
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
                Logger.Warning("AsyncExecutionData was already persisted: Request.Cuid={0}, RequestHash={1}", executionData.RequestCuid, requestHash);
            }
            SaveRequestData(request, requestHash);
        }

        private void SaveRequestData(AsyncExecutionRequest request, string requestHash)
        {
            AsyncExecutionRequestData requestData = AsyncCallbackRepository.OneAsyncExecutionRequestDataWhere(c => c.RequestHash == requestHash);
            if (requestData == null)
            {
                requestData = new AsyncExecutionRequestData
                {
                    Cuid = request.Cuid,
                    RequestHash = requestHash,
                    ClassName = request.ClassName,
                    MethodName = request.MethodName,
                    JsonParams = request.JsonParams
                };
                Expect.AreEqual(requestHash, requestData.GetRequestHash());
                AsyncCallbackRepository.Save(requestData);
            }
            else
            {
                Logger.Warning("AsyncExecutionRequestData was already persisted: RequestHash={0}", requestHash);
            }
        }
    }
}
