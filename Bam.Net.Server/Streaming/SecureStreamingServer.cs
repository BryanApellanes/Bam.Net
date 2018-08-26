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
            HmacAlgorithm = HashAlgorithms.SHA1;
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
                        if (!IsValid(request))
                        {
                            return new TResponse { Success = false, SessionId = request.SessionId };
                        }
                        TResponse response = ProcessSecureRequest(request);
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

        protected virtual TResponse StartSession()
        {
            try
            {
                string sessionId = SecureSession.GenerateId();
                SecureSession session = SecureSession.Get(sessionId);
                return new TResponse { Success = true, Data = new { SessionId = sessionId, session.PublicKey }.ToJson() };
            }
            catch (Exception ex)
            {
                return new TResponse { Success = false, Data = $"Failed to start session: {ex.Message}" };
            }
        }

        protected virtual TResponse SetKey(TRequest request)
        {
            try
            {
                SecureSession session = SecureSession.Get(request.SessionId);
                session.SetSymmetricKey(request.SessionKeyInfo);
                return new TResponse { Success = true, PublicKey = session.PublicKey, SessionId = request.SessionId };
            }
            catch (Exception ex)
            {
                Logger.AddEntry("Error setting session key: {0}", ex, ex.Message);
                return new TResponse { Success = false, SessionId = request.SessionId };
            }
        }

        /// <summary>
        /// Returns true if the request is valid.  If the request is valid the request.Validated
        /// property is set to true and the Message property is decrypted.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>
        ///   <c>true</c> if the specified request is valid; otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool IsValid(TRequest request)
        {
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

            string hmac = request.Message.ToBase64().Hmac(session.SymmetricKey, HmacAlgorithm, Encoding);
            request.Validated = request.Hmac.Equals(hmac);
            if (request.Validated)
            {
                request.Message = DecryptMessage(request);
            }
            return request.Validated;
        }

        private byte[] DecryptMessage(TRequest request)
        {
            SecureSession session = SecureSession.Get(request.SessionId);
            return Aes.Decrypt(request.Message.ToBase64(), session.SymmetricKey, session.SymmetricIV).FromBase64();
        }
    }
}
