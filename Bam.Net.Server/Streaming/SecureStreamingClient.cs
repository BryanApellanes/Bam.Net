using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Secure;
using System;
using System.Collections.Generic;
using System.IO;
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
            HmacAlgorithm = HashAlgorithms.SHA256;
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

        public StreamingResponse<TResponse> SendEncryptedRequest(TRequest message)
        {
            if(string.IsNullOrEmpty(PublicKey) ||
                string.IsNullOrEmpty(SessionId))
            {
                StartSession();
            }
            byte[] messageBytes = message.ToBinaryBytes();
            string messageBase64 = messageBytes.ToBase64();
            string hmac = messageBase64.Hmac(AesKeyVectorPair.Key, HmacAlgorithm, Encoding);

            string encryptedBase64Message = AesKeyVectorPair.Encrypt(messageBase64);
            byte[] encryptedMessageBytes = Encoding.GetBytes(encryptedBase64Message);

            TRequest request = new TRequest
            {
                Hmac = hmac,
                RequestType = SecureRequestTypes.Message,
                Message = encryptedMessageBytes,
                SessionId = SessionId
            };
            StreamingResponse<TResponse> response = SendRequest<TRequest, TResponse>(request);
            return response;
        }

        public virtual void StartSession()
        {
            StreamingResponse<TResponse> response = SendRequest<TRequest, TResponse>(new TRequest { RequestType = SecureRequestTypes.StartSession });
            if (!response.Data.Success)
            {
                throw new InvalidOperationException(string.Format("Failed to start secure stream session: {0}", response.Data.Message));
            }
            TResponse startSessionResponse = response.Data;
            PublicKey = startSessionResponse.PublicKey;
            SessionId = startSessionResponse.SessionId;
            SetSessionKey(response);
        }

        protected override T ReceiveResponse<T>(Stream stream)
        {
            // TODO: decrypt server response
            return base.ReceiveResponse<T>(stream);
        }

        private void SetSessionKey(StreamingResponse<TResponse> response)
        {
            SetSessionKeyRequest sessionKeyInfo = SecureSession.CreateSetSessionKeyInfo(PublicKey, out AesKeyVectorPair aesKey);
            AesKeyVectorPair = aesKey;
            StreamingResponse<TResponse> setKeyResponse = SendRequest<TRequest, TResponse>(new TRequest { SessionId = SessionId, RequestType = SecureRequestTypes.SetKey, SessionKeyInfo = sessionKeyInfo });
            if (!response.Data.Success)
            {
                throw new InvalidOperationException(string.Format("Failed to set key for secure stream session: {0}", response.Data.Message));
            }
        }
    }
}
