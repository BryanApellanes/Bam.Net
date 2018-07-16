using Bam.Net.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services.Clients
{
    [Proxy("webhookClient")]
    public class WebHookClient : ProxyableService
    {
        public WebHookClient() { }

        public event EventHandler WebHookReceived;

        public virtual bool ReceiveWebHook(string webHookName, string bodyOrNull, Dictionary<string, string> headers)
        {
            if(!IsValid(webHookName, bodyOrNull, headers))
            {
                return false;
            }

            try
            {
                WebHookReceived?.Invoke(this, new WebHookClientEventArgs { WebHookName = webHookName, BoryOrBlank = bodyOrNull });
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Failed to invoke WebHookReceived event: {0}", ex, ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Returns true if the webhook call is valid.  The default implementation of this method will always
        /// return true, this should be overridden if additional validation is required.
        /// </summary>
        /// <param name="webHookName">Name of the web hook.</param>
        /// <param name="bodyOrNull">The body or null.</param>
        /// <param name="headers">The headers.</param>
        /// <returns>
        ///   <c>true</c> if the specified web hook name is valid; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsValid(string webHookName, string bodyOrNull, Dictionary<string, string> headers)
        {
            return true; // override for if additional validation required
        }

        public override object Clone()
        {
            WebHookClient clone = new WebHookClient();
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }
    }
}
