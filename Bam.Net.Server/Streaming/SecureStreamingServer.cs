using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Secure;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Server.Streaming
{
    public abstract class SecureStreamingServer<TRequest, TResponse> : StreamingServer<TRequest, TResponse>
        where TRequest : SecureStreamingRequest, new()
        where TResponse : SecureStreamingResponse, new()
    {
        public SecureStreamingServer()
        {
            HmacAlgorithm = HashAlgorithms.SHA256;
        }

        public override TResponse ProcessRequest(StreamingContext<TRequest> context)
        {
            try
            {
                TRequest request = context.Request.Message;
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
                            return new TResponse { Success = false, SessionId = request.SessionId };
                        }
                        TResponse response = ProcessSecureRequest(decrypted);
                        response.SessionId = request.SessionId;
                        response.PublicKey = SecureSession.Get(request.SessionId).PublicKey;
                        return response;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Exception processing request: {0}", ex, ex.Message);
            }
            return new TResponse();
        }

        public HashAlgorithms HmacAlgorithm { get; set; }

        public abstract TResponse ProcessSecureRequest(TRequest request);

        public override void WriteResponse(StreamingContext context, TResponse message)
        {
            // TODO: encrypt the message
            
            // Get SecureSession using request.SessionId
            // create AesKeyVectorPair from SecureSession
            // message.Cipher = AesEncrypt(message.ToBinaryBytes())
            // WriteResponse(context, message)
            
            // Replace below

            // base implementation  
            StreamingResponse<TResponse> msg = new StreamingResponse<TResponse> { Data = message };
            WriteResponse(context, msg);
            // end base

            //base.WriteResponse(context, message);
        }

        protected virtual TResponse StartSession()
        {
            try
            {
                string sessionId = SecureSession.GenerateId();
                SecureSession session = SecureSession.Get(sessionId);
                return new TResponse { Success = true, SessionId = sessionId, PublicKey = session.PublicKey, Data = new { SessionId = sessionId, session.PublicKey }.ToJson() };
            }
            catch (Exception ex)
            {
                return new TResponse { Success = false, Message = $"Failed to start session: {ex.Message}" };
            }
        }

        protected virtual TResponse SetKey(TRequest request)
        {
            try
            {
                SecureSession session = SecureSession.Get(request.SessionId);
                session.SetSymmetricKey(request.SessionKeyInfo);
                return new TResponse { Success = true, PublicKey = session.PublicKey, SessionId = session.Identifier, Data = new { SessionId = session.Identifier, session.PublicKey }.ToJson() };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error setting session key: {0}", ex, ex.Message);
                return new TResponse { Success = false, Message = $"Failed to set key: {ex.Message}", SessionId = request.SessionId };
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
        protected virtual bool IsValid(TRequest request, out TRequest decrypted)
        {
            decrypted = default(TRequest);
            SecureSession session = SecureSession.Get(request.SessionId);
            if (session == null || string.IsNullOrEmpty(session.AsymmetricKey))
            {
                Logger.AddEntry("No valid session found for request: {0}", request.TryToJson());
                return false;
            }
            if(request.Message == null)
            {
                Logger.AddEntry("No message specified for request: {0}", request.TryToJson());
                return false;
            }
            AesKeyVectorPair aesKey = new AesKeyVectorPair { Key = session.PlainSymmetricKey, IV = session.PlainSymmetricIV };
            string encryptedBase64Message = Encoding.GetString(request.Message);
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
