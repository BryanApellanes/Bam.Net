using Bam.Net.Logging;
using Bam.Net.Server.Streaming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Services
{
    public class KeyValueStoreServer : SecureStreamingServer<KeyValueRequest, KeyValueResponse>
    {
        public KeyValueStoreServer(string name, int port, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            KeyValueStore = new FileSystemKeyValueStore(Logger);
        }

        public KeyValueStoreServer(DirectoryInfo localStorage, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            KeyValueStore = new FileSystemKeyValueStore(localStorage, logger);
        }

        public KeyValueStoreServer(FileSystemKeyValueStore localStorage, ILogger logger = null)
        {
            Logger = logger ?? Log.Default;
            KeyValueStore = localStorage;
        }

        public FileSystemKeyValueStore KeyValueStore { get; set; }

        public override KeyValueResponse ProcessDecryptedRequest(KeyValueRequest request)
        {
            try
            {
                switch (request.Type)
                {
                    case KeyValueRequestTypes.Invalid:
                        throw new InvalidOperationException("Invalid KeyValueRequestType specified");
                    case KeyValueRequestTypes.Get:
                        return new KeyValueResponse { Success = true, Value = KeyValueStore.Get(request.Key) };
                    case KeyValueRequestTypes.Set:
                        if (KeyValueStore.Set(request.Key, request.Value))
                        {
                            return new KeyValueResponse { Success = true, Value = request.Value};
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

        public override KeyValueResponse ProcessRequest(StreamingContext<KeyValueRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
