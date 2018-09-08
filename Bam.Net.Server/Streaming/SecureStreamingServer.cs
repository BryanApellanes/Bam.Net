using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Secure;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    public abstract class SecureStreamingServer<TRequest, TResponse> : StreamingServer<TRequest, TResponse> where TResponse : new()
    {
        public SecureStreamingServer()
        {
            HmacAlgorithm = HashAlgorithms.SHA256;
        }

        protected internal override void ReadRequest(TcpClient client)
        {
            while (client.Connected)
            {
                try
                {
                    GetStreamData(client, out NetworkStream stream, out byte[] requestData);

                    SecureStreamingRequest<TRequest> request = requestData.FromBinaryBytes<SecureStreamingRequest<TRequest>>();
                    SecureStreamingContext<TRequest> ctx = new SecureStreamingContext<TRequest>
                    {
                        Request = request,
                        ResponseStream = stream,
                        Encoding = Encoding
                    };
                    SecureStreamingResponse<TResponse> response = ProcessSecureRequest(ctx);
                    WriteSecureResponse(ctx, response, request.RequestType == SecureRequestTypes.Message);
                }
                catch (Exception ex)
                {
                    Logger.AddEntry("Error reading request: {0}", ex, ex.Message);
                }
            }
        }

        public SecureStreamingResponse<TResponse> ProcessSecureRequest(SecureStreamingContext<TRequest> context)
        {
            try
            {
                SecureStreamingRequest<TRequest> request = context.Request;
                switch (request.RequestType)
                {
                    case SecureRequestTypes.Invalid:
                        throw new InvalidOperationException("Invalid SecureRequestType specified");
                    case SecureRequestTypes.StartSession:
                        return StartSession();
                    case SecureRequestTypes.SetKey:
                        return SetKey(request);
                    case SecureRequestTypes.Message:
                        if (!IsValid(request, out TRequest decrypted))
                        {
                            return new SecureStreamingResponse<TResponse> { Body = new TResponse(), Success = false, SessionId = request.SessionId };
                        }
                        TResponse response = ProcessDecryptedRequest(decrypted);
                        return new SecureStreamingResponse<TResponse> { Success = true, Body = response, SessionId = request.SessionId, PublicKey = SecureSession.Get(request.SessionId).PublicKey };
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception processing request: {0}", ex, ex.Message);
            }
            return new SecureStreamingResponse<TResponse> { Body = new TResponse() };
        }
        
        public HashAlgorithms HmacAlgorithm { get; set; }

        public abstract TResponse ProcessDecryptedRequest(TRequest request);

        public void WriteSecureResponse(StreamingContext context, SecureStreamingResponse<TResponse> response, bool encrypt = false)
        {
            if (encrypt)
            {
                SecureSession secureSession = SecureSession.Get(response.SessionId);
                AesKeyVectorPair aes = new AesKeyVectorPair { Key = secureSession.PlainSymmetricKey, IV = secureSession.PlainSymmetricIV };
                byte[] messageBytes = response.Body.ToBinaryBytes();
                string messageBase64 = messageBytes.ToBase64();
                string hmac = messageBase64.Hmac(aes.Key, HmacAlgorithm, Encoding);
                string encryptedBase64Message = aes.Encrypt(messageBase64);

                response.Cipher = encryptedBase64Message;
                response.Body = default(TResponse);
                response.Hmac = hmac;
            }

            SerializeToResponseStream(context, response);            
        }

        protected virtual SecureStreamingResponse<TResponse> StartSession()
        {
            try
            {
                string sessionId = SecureSession.GenerateId();
                SecureSession session = SecureSession.Get(sessionId);
                return new SecureStreamingResponse<TResponse> { Body = new TResponse(), Success = true, SessionId = sessionId, PublicKey = session.PublicKey };
            }
            catch (Exception ex)
            {
                return new SecureStreamingResponse<TResponse> { Success = false, Body = new TResponse(), Message = $"Failed to start session: {ex.Message}" };
            }
        }

        protected virtual SecureStreamingResponse<TResponse> SetKey(SecureStreamingRequest<TRequest> request)
        {
            try
            {
                SecureSession session = SecureSession.Get(request.SessionId);
                session.SetSymmetricKey(request.SessionKeyInfo);
                return new SecureStreamingResponse<TResponse> { Body = new TResponse(), Success = true, PublicKey = session.PublicKey, SessionId = session.Identifier };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error setting session key: {0}", ex, ex.Message);
                return new SecureStreamingResponse<TResponse> { Body = new TResponse(), Success = false, Message = $"Failed to set key: {ex.Message}", SessionId = request.SessionId };
            }
        }

        /// <summary>
        /// Returns true if the request is valid.  If the request is valid the request.Validated
        /// property is set to true and the Message property is decrypted.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="decrypted"></param>
        /// <returns>
        ///   <c>true</c> if the specified request is valid; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsValid(SecureStreamingRequest request, out TRequest decrypted)
        {
            decrypted = default(TRequest);
            SecureSession session = SecureSession.Get(request.SessionId);
            if (session == null || string.IsNullOrEmpty(session.AsymmetricKey))
            {
                Logger.AddEntry("No valid session found for request: {0}", request.TryToJson());
                return false;
            }
            if(request.Body == null)
            {
                Logger.AddEntry("No message specified for request: {0}", request.TryToJson());
                return false;
            }
            AesKeyVectorPair aesKey = new AesKeyVectorPair { Key = session.PlainSymmetricKey, IV = session.PlainSymmetricIV };
            string encryptedBase64Message = Encoding.GetString(request.Body);
            string messageBase64 = aesKey.Decrypt(encryptedBase64Message);
            string hmac = messageBase64.Hmac(aesKey.Key, HmacAlgorithm, Encoding);
            byte[] messageBytes = messageBase64.FromBase64();                       

            request.Validated = hmac.Equals(request.Hmac);
            if (request.Validated)
            {
                decrypted = messageBase64.FromBase64().FromBinaryBytes<TRequest>();
            }
            return request.Validated;
        }
    }
}
