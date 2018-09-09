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

        public SecureStreamingResponse<TResponse> SendEncryptedMessage(TRequest message)
        {
            if (string.IsNullOrEmpty(PublicKey) ||
                string.IsNullOrEmpty(SessionId))
            {
                StartSession();
            }
            byte[] messageBytes = message.ToBinaryBytes();
            string messageBase64 = messageBytes.ToBase64();
            string hmac = messageBase64.Hmac(AesKeyVectorPair.Key, HmacAlgorithm, Encoding);

            string encryptedBase64Message = AesKeyVectorPair.Encrypt(messageBase64);
            byte[] encryptedMessageBytes = Encoding.GetBytes(encryptedBase64Message);

            return SendSecureStreamingRequest((r) =>
            {
                r.Hmac = hmac;
                r.RequestType = SecureRequestTypes.Message;
                r.SessionId = SessionId;
                r.Body = encryptedMessageBytes;
            });
        }

        protected SecureStreamingResponse<TResponse> SendSecureStreamingRequest(Action<SecureStreamingRequest> configure = null)
        {
            SecureStreamingRequest<TRequest> msg = new SecureStreamingRequest<TRequest>();
            configure?.Invoke(msg);
            SerializeToRequestStream(NetworkStream, msg);
            SecureStreamingResponse<TResponse> response = ReceiveResponse<SecureStreamingResponse<TResponse>>(NetworkStream);
            if (!string.IsNullOrEmpty(response.Cipher))
            {
                string messageBase64 = AesKeyVectorPair.Decrypt(response.Cipher);
                byte[] messageBytes = messageBase64.FromBase64();
                response.Cipher = string.Empty;
                response.Body = messageBytes.FromBinaryBytes<TResponse>();
            }
            return response;
        }
        
        public virtual void StartSession()
        {
            SecureStreamingResponse<TResponse> response = SendSecureStreamingRequest((r) => r.RequestType = SecureRequestTypes.StartSession);
            if (!response.Success)
            {
                throw new InvalidOperationException(string.Format("Failed to start secure stream session: {0}", response.Message));
            }
            PublicKey = response.PublicKey;
            SessionId = response.SessionId;
            SetSessionKey();
        }

        private void SetSessionKey()
        {
            SetSessionKeyRequest sessionKeyInfo = SecureSession.CreateSetSessionKeyInfo(PublicKey, out AesKeyVectorPair aesKey);
            AesKeyVectorPair = aesKey;
            SecureStreamingResponse<TResponse> setKeyResponse = SendSecureStreamingRequest((r) =>
            {
                r.SessionId = SessionId;
                r.RequestType = SecureRequestTypes.SetKey;
                r.SessionKeyInfo = sessionKeyInfo;
            });
            if (!setKeyResponse.Success)
            {
                throw new InvalidOperationException(string.Format("Failed to set key for secure stream session: {0}", setKeyResponse.Message));
            }
        }
    }
}
