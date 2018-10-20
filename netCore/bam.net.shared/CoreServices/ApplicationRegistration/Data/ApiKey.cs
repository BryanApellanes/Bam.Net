using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    [Serializable]
    public class ApiKey: AuditRepoData
    {
        public long ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public string ClientId { get; set; }
        public string SharedSecret { get; set; }
        public DateTime? Confirmed { get; set; }
        public bool Disabled { get; set; }
        public string DisabledBy { get; set; }

        public static ApiKey FromKeyInfo(ApiKeyInfo info)
        {
            ApiKey key = new ApiKey()
            {
                ClientId = info.ApplicationClientId,
                SharedSecret = info.ApiKey
            };
            return key;
        }

        public ApiKeyInfo ToKeyInfo()
        {
            return new ApiKeyInfo
            {
                ApplicationClientId = ClientId,
                ApiKey = SharedSecret,
                ApplicationNameProvider = new StaticApplicationNameProvider(Application.Name)
            };
        }
    }
}
