using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    public class SecureStreamingClient<TRequest, TResponse> : StreamingClient<TRequest, TResponse>
        where TRequest : SecureStreamingRequest, new()
        where TResponse : SecureStreamingResponse, new()
    {
        public SecureStreamingClient(string hostName, int port) : base(hostName, port)
        {
        }

        Encoding _encoding;
        object _encodingLock = new object();
        public Encoding Encoding
        {
            get
            {
                return _encodingLock.DoubleCheckLock(ref _encoding, () => Encoding.UTF8);
            }
            set
            {
                _encoding = value;
            }
        }

        public HashAlgorithms HmacAlgorithm { get; set; }
        public string PublicKey { get; set; }
        public string SessionId { get; set; }
        protected AesKeyVectorPair AesKeyVectorPair { get; set; }        

        public override StreamingResponse SendRequest(object message)
        {
            if(string.IsNullOrEmpty(PublicKey) || string.IsNullOrEmpty(SessionId))
            {
                StartSession();
            }
            byte[] messageBytes = message.ToBinaryBytes();
            TRequest request = new TRequest { Hmac = messageBytes.ToBase64().Hmac(AesKeyVectorPair.Key, HmacAlgorithm, Encoding), RequestType = SecureRequestTypes.Message };
            request.Message = Aes.Encrypt(messageBytes.ToBase64(), AesKeyVectorPair.Key, AesKeyVectorPair.IV).FromBase64();
            return base.SendRequest(request);
        }

        protected virtual void StartSession()
        {
            TResponse startSessionResponse = base.SendRequest(new TRequest { RequestType = SecureRequestTypes.StartSession }).Data;
            PublicKey = startSessionResponse.PublicKey;
            SessionId = startSessionResponse.SessionId;
            SetSessionKeyRequest sessionKeyInfo = SecureSession.CreateSetSessionKeyInfo(PublicKey, out AesKeyVectorPair aesKey);
            TResponse setKeyResponse = base.SendRequest(new TRequest { RequestType = SecureRequestTypes.SetKey, SessionKeyInfo = sessionKeyInfo }).Data;
            AesKeyVectorPair = aesKey;
        }
    }
}
