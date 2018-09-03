using Bam.Net.Logging;
using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class KeyValueStoreServer : SecureStreamingServer<KeyValueRequest, KeyValueResponse>
    {
        public KeyValueStoreServer(ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            KeyValueStore = new FileSystemKeyValueStore(Logger);
        }

        public FileSystemKeyValueStore KeyValueStore { get; set; }

        public override KeyValueResponse ProcessSecureRequest(KeyValueRequest request)
        {
            try
            {
                switch (request.Type)
                {
                    case KeyValueRequestTypes.Invalid:
                        throw new InvalidOperationException("Invalid KeyValueRequestType specified");
                    case KeyValueRequestTypes.Get:
                        return new KeyValueResponse { Success = true, Data = KeyValueStore.Get(request.Key) };
                    case KeyValueRequestTypes.Set:
                        if (KeyValueStore.Set(request.Key, request.Value))
                        {
                            return new KeyValueResponse { Success = true };
                        }
                        break;
                    default:
                        break;
                }
                return new KeyValueResponse { Success = false, Message = "Request unhandled" };

            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception processing keyvalue request: {0}", ex, ex.Message);
                return new KeyValueResponse { Success = false, Message = ex.Message };
            }
        }
    }
}
