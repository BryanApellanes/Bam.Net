using Bam.Net.Logging;
using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class KeyValueStoreClient: SecureStreamingClient<KeyValueRequest, KeyValueResponse>, IKeyValueStore
    {
        public KeyValueStoreClient(string hostName, int port, ILogger logger = null) : base(hostName, port)
        {
            Logger = logger ?? Log.Default;
        }

        public ILogger Logger { get; set; }

        public string Get(string key)
        {
            try
            {
                StreamingResponse<KeyValueResponse> response = SendRequest(new KeyValueRequest { Type = KeyValueRequestTypes.Get, Key = key });
                if (!response.Data.Success)
                {
                    throw new Exception(response.Data.Message);
                }
                return response.Data.Value;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception getting key value: {0}", ex, ex.Message);
                return string.Empty;
            }
        }

        public bool Set(string key, string value)
        {
            try
            {
                StreamingResponse<KeyValueResponse> response = SendRequest(new KeyValueRequest { Type = KeyValueRequestTypes.Set, Key = key, Value = value });
                if (!response.Data.Success)
                {
                    throw new Exception(response.Data.Message);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception setting key value: {0}", ex, ex.Message);
                return false;
            }
        }
    }
}
