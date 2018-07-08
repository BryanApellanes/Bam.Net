using Bam.Net.Data;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices.WebHooks
{
    [Proxy("webHookSvc")]
    [ApiKeyRequired]
    public class WebHookService : ProxyableService
    {
        protected WebHookService()
        {
        }

        public WebHookService(DaoRepository repo, AppConf appConf) : base(repo, appConf)
        {
            DaoRepository.AddTypes(typeof(WebHookDescriptor), typeof(WebHookSubscriber));
        }

        public virtual CoreServiceResponse<WebHookSubscriber> Subscribe(string webHookName, string url)
        {
            try
            {
                WebHookDescriptor descriptor = DaoRepository.Query<WebHookDescriptor>(new { WebHookName = webHookName }).FirstOrDefault();
                if (descriptor == null)
                {
                    descriptor = new WebHookDescriptor { WebHookName = webHookName };
                    descriptor = DaoRepository.Save(descriptor);
                }
                WebHookSubscriber subscriber = new WebHookSubscriber { Url = url };
                subscriber.Descriptor = descriptor;
                return new CoreServiceResponse<WebHookSubscriber> { Success = true, Data = DaoRepository.Save(subscriber) };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse<WebHookSubscriber> { Success = false, Message = ex.Message };
            }
        }

        public virtual CoreServiceResponse ListSubscribers(string webHookName)
        {
            try
            {
                WebHookDescriptor descriptor = DaoRepository.Query<WebHookDescriptor>(new { WebHookName = webHookName }).FirstOrDefault();
                string[] result = new string[] { };
                if(descriptor != null)
                {
                    result = descriptor.Subscribers.Select(whs => whs.Url).ToArray();
                }
                return new CoreServiceResponse { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public virtual CoreServiceResponse Call(string webHookName, string bodyOrNull, Dictionary<string, string> headers)
        {
            try
            {
                WebHookDescriptor descriptor = DaoRepository.Query<WebHookDescriptor>(new { WebHookName = webHookName }).FirstOrDefault();
                if(descriptor != null)
                {
                    if(descriptor.Subscribers.Count > 0)
                    {
                        Parallel.ForEach(descriptor.Subscribers, (subscriber) =>
                        {
                            if (!string.IsNullOrEmpty(subscriber.Url))
                            {
                                if (!string.IsNullOrEmpty(bodyOrNull))
                                {
                                    Http.Post(subscriber.Url, bodyOrNull, headers);
                                    Logger.Info("POST: WebHook Subscriber: {0}", subscriber.Url);
                                }
                                else
                                {
                                    Http.Get(subscriber.Url, headers);
                                    Logger.Info("GET: WebHook Subscriber: {0}", subscriber.Url);
                                }
                            }
                            else
                            {
                                Logger.Warning("Found subscriber without Url: {0}", subscriber.Uuid);
                            }
                        });
                        return new CoreServiceResponse { Success = true, Message = string.Join("\r\n", descriptor.Subscribers.Select(s => s.Url).ToArray()) };
                    }
                }
                return new CoreServiceResponse { Success = true, Message = $"WebHook not found: {webHookName}" };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        public override object Clone()
        {
            WebHookService clone = new WebHookService();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
