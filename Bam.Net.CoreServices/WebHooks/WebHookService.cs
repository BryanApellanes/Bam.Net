using Bam.Net.CoreServices.WebHooks.Data;
using Bam.Net.CoreServices.WebHooks.Data.Dao.Repository;
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
    public class WebHookService : ApplicationProxyableService
    {
        public const string HashHeader = "X-BAM-WEBHOOK-SHA256";
        protected WebHookService()
        {
        }

        public WebHookService(WebHooksRepository webhooksrepository, DaoRepository repo, AppConf appConf) : base(repo, appConf)
        {
            WebHooksRepository = webhooksrepository;
        }

        public WebHooksRepository WebHooksRepository { get; set; }

        public virtual CoreServiceResponse<WebHookSubscriber> SubscribeToWebHook(string webHookName, string url)
        {
            try
            {
                WebHookDescriptor descriptor = WebHooksRepository.OneWebHookDescriptorWhere(d => d.WebHookName == webHookName);
                if (descriptor == null)
                {
                    descriptor = new WebHookDescriptor { WebHookName = webHookName };
                    descriptor = WebHooksRepository.Save(descriptor);
                }
                WebHookSubscriber subscriber = WebHooksRepository.Save(new WebHookSubscriber { Url = url });
                subscriber.Descriptors.Add(descriptor);
                subscriber = WebHooksRepository.Save(subscriber);
                return new CoreServiceResponse<WebHookSubscriber> { Success = true, Data = new { WebHookName = webHookName, Subscriber = subscriber.Uuid, subscriber.Url} };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse<WebHookSubscriber> { Success = false, Message = ex.Message };
            }
        }

        [RoleRequired("/WebHookService/AccessDenied", "Admin", "WebHookServiceAdmin", "WebHookService.ListWebHooks")]
        public virtual CoreServiceResponse ListWebHooks()
        {
            try
            {
                HashSet<string> webhookNames = new HashSet<string>();
                WebHooksRepository.BatchAllWebHookDescriptors(1000, (webhooks) => webhookNames.Each(whn => webhookNames.Add(whn))).Wait();
                CoreServiceResponse response = new CoreServiceResponse { Success = true, Data = webhookNames.ToArray() };
                return response;
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        [RoleRequired("/WebHookService/AccessDenied", "Admin", "WebHookServiceAdmin", "WebHookService.ListSubscribers")]
        public virtual CoreServiceResponse<WebHookSubscriptionInfo> ListSubscribers(string webHookName)
        {
            try
            {
                WebHookDescriptor descriptor = WebHooksRepository.OneWebHookDescriptorWhere(d => d.WebHookName == webHookName);

                WebHookSubscriptionInfo[] result = new WebHookSubscriptionInfo[] { };
                if(descriptor != null)
                {
                    result = descriptor.Subscribers.Select(whs => new WebHookSubscriptionInfo { WebHookName = webHookName, Url = whs.Url }).ToArray();
                }
                return new CoreServiceResponse<WebHookSubscriptionInfo> { Success = true, Data = result };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse<WebHookSubscriptionInfo> { Success = false, Message = ex.Message };
            }
        }

        public virtual CoreServiceResponse GetHashFormat()
        {
            return new CoreServiceResponse { Success = true, Data = "{WebHookName}{BodyOrBlank}{SharedSecret}" };
        }

        [Local]
        public CoreServiceResponse Call(string webHookName, string bodyOrNull)
        {
            WebHookDescriptor descriptor = WebHooksRepository.OneWebHookDescriptorWhere(d => d.WebHookName == webHookName);
            return Call(descriptor, bodyOrNull, new Dictionary<string, string>
            {
                { HashHeader, GetCallHash(descriptor, bodyOrNull) }
            });
        }

        public virtual CoreServiceResponse Call(WebHookDescriptor descriptor, string bodyOrNull, Dictionary<string, string> headers)
        {
            try
            {
                Args.ThrowIfNull(descriptor);
                if (!headers.ContainsKey(HashHeader))
                {
                    return new CoreServiceResponse { Success = false, Message = $"Header missing ({HashHeader})" };
                }

                string webHookName = descriptor.WebHookName;

                if(descriptor != null)
                {
                    string hash = GetCallHash(descriptor, bodyOrNull);
                    if (!hash.Equals(headers[HashHeader]))
                    {
                        WebHookCall call = new WebHookCall { Succeeded = false };
                        WebHooksRepository.Save(call);
                        return new CoreServiceResponse { Success = false, Message = "Hash check failed" };
                    }

                    if (descriptor.Subscribers.Count > 0)
                    {
                        Parallel.ForEach(descriptor.Subscribers, (subscriber) =>
                        {
                            if (!string.IsNullOrEmpty(subscriber.Url))
                            {
                                string response = string.Empty;
                                if (!string.IsNullOrEmpty(bodyOrNull))
                                {
                                    response = Http.Post(subscriber.Url, bodyOrNull, headers);
                                    Logger.Info("POST: WebHook Subscriber: {0}", subscriber.Url);
                                }
                                else
                                {
                                    response = Http.Get(subscriber.Url, headers);
                                    Logger.Info("GET: WebHook Subscriber: {0}", subscriber.Url);
                                }
                                WebHookCall call = new WebHookCall { Succeeded = true, Response = response };
                                WebHooksRepository.Save(call);
                            }
                            else
                            {
                                Logger.Warning("Found subscriber without Url: {0}", subscriber.Uuid);
                            }
                        });
                        return new CoreServiceResponse { Success = true, Message = string.Join("\r\n", descriptor.Subscribers.Select(s => s.Url).ToArray()) };
                    }
                    return new CoreServiceResponse { Success = false, Message = $"No subscribers for specified webhook: {webHookName}" };
                }
                return new CoreServiceResponse { Success = true, Message = $"WebHook not found: {webHookName}" };
            }
            catch (Exception ex)
            {
                return new CoreServiceResponse { Success = false, Message = ex.Message };
            }
        }

        private string GetCallHash(WebHookDescriptor descriptor, string bodyOrNull)
        {
            string hashFormat = GetHashFormat().Data.ToString();
            string hash = hashFormat.NamedFormat(new { descriptor.WebHookName, BodyOrBlank = bodyOrNull, descriptor.SharedSecret }).Sha256();
            return hash;
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
