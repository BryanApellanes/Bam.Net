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
                SecureStreamingResponse<KeyValueResponse> response = SendEncryptedMessage(new KeyValueRequest { Type = KeyValueRequestTypes.Get, Key = key });
                if (!response.Success)
                {
                    throw new Exception(response.Message);
                }
                return response.Body.Value;
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
                SecureStreamingResponse<KeyValueResponse> response = SendEncryptedMessage(new KeyValueRequest { Type = KeyValueRequestTypes.Set, Key = key, Value = value });
                if (!response.Success)
                {
                    throw new Exception(response.Message);
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
