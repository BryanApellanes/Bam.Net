using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    [Serializable]
    public class SecureStreamingRequest<T> : SecureStreamingRequest
    {
        public new T Body { get; set; }
    }

    [Serializable]
    public class SecureStreamingRequest
    {
        /// <summary>
        /// Gets or sets the session key information.  Should be null
        /// for all requests except when setting the symmetric session key.
        /// </summary>
        /// <value>
        /// The session key information.
        /// </value>
        public SetSessionKeyRequest SessionKeyInfo { get; set; }
        public SecureRequestTypes RequestType { get; set; }
        public string SessionId { get; set; }
        public string Hmac { get; set; }
        public bool Validated { get; set; }
        
        /// <summary>
        /// Gets or sets the encrypted bytes.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public byte[] Body { get; set; }
    }
}
