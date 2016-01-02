/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Bam.Net.Encryption;
using Bam.Net.Logging;
using Bam.Net.Web;
using System.IO;

namespace Bam.Net.ServiceProxy.Secure
{
    /// <summary>
    /// Class used to set and validate encryption validation
    /// tokens
    /// </summary>
    internal class ApiValidation
    {
        public const string ValidationTokenName = "ValidationToken";
        public const string NonceName = "Timestamp";
        public const string PaddingName = "Padding";

        public static void SetValidationToken(HttpWebRequest request, string postString, string publicKey)
        {
            SetValidationToken(request.Headers, postString, publicKey);
        }

        public static void SetValidationToken(NameValueCollection headers, string postString, string publicKey)
        {
            ValidationToken token = CreateValidationToken(postString, publicKey);
            headers[NonceName] = token.NonceCipher;
            headers[ValidationTokenName] = token.HashCipher;
        }
        
        public static ValidationToken GetValidationToken(NameValueCollection headers)
        {
            ValidationToken result = new ValidationToken();
            result.NonceCipher = headers[NonceName];
            result.HashCipher = headers[ValidationTokenName];
            Args.ThrowIfNull(result.NonceCipher, NonceName);
            Args.ThrowIf<ValidationTokenNotFoundException>(
                result.HashCipher == null || string.IsNullOrEmpty(result.HashCipher),  
                "ValidationToken header was not found: {0}",
                ValidationTokenName);
            return result;
        }

        public static ValidationToken CreateValidationToken(string postString, SecureSession session)
        {
            return CreateValidationToken(postString, session.PublicKey);
        }

        public static ValidationToken CreateValidationToken(string postString, string publicKeyPem)
        {            
            Instant instant = new Instant();
            return CreateValidationToken(instant, postString, publicKeyPem);
        }

        public static ValidationToken CreateValidationToken(Instant instant, string postString, string publicKeyPem)
        {
            string nonce = instant.ToString();
            string kvpFormat = "{0}:{1}";
            //{Month}/{Day}/{Year};{Hour}.{Minute}.{Second}.{Millisecond}:{PostString}
            string hash = kvpFormat._Format(nonce, postString).Sha1();
            string hashCipher = hash.EncryptWithPublicKey(publicKeyPem);
            string nonceCipher = nonce.EncryptWithPublicKey(publicKeyPem);

            return new ValidationToken { HashCipher = hashCipher, NonceCipher = nonceCipher };
        }

        public static TokenValidationStatus ValidateToken(IHttpContext context, string post)
        {
            NameValueCollection headers = context.Request.Headers;
            
            string paddingValue = headers[PaddingName] ?? string.Empty;
            bool usePadding = paddingValue.ToLowerInvariant().Equals("true");
            
            return ValidateToken(headers, post, usePadding);
        }

        public static TokenValidationStatus ValidateToken(NameValueCollection headers, string plainPost, bool usePkcsPadding = false)
        {
            SecureSession session = SecureSession.Get(headers);
            ValidationToken token = GetValidationToken(headers);

            return ValidateToken(session, token, plainPost, usePkcsPadding);
        }

        public static TokenValidationStatus ValidateToken(SecureSession session, ValidationToken token, string plainPost, bool usePkcsPadding = false)
        {
            Args.ThrowIfNull(session, "session");
            Args.ThrowIfNull(token, "token");

            return ValidateToken(session, token.HashCipher, token.NonceCipher, plainPost, usePkcsPadding);
        }

        public static TokenValidationStatus ValidateToken(SecureSession session, string hashCipher, string nonceCipher, string plainPost, bool usePkcsPadding = false)
        {
            string hash = session.DecryptWithPrivateKey(hashCipher, usePkcsPadding);
            string nonce = session.DecryptWithPrivateKey(nonceCipher, usePkcsPadding);

            int offset = session.TimeOffset.Value;

            TokenValidationStatus result = ValidateNonce(nonce, offset);
            if (result == TokenValidationStatus.Success)
            {
                result = ValidateHash(nonce, hash, plainPost);
            }

            return result;
        }

        public static TokenValidationStatus ValidateHash(string nonce, string hash, string plainPost)
        {
            string kvpFormat = "{0}:{1}";
            string checkHash = kvpFormat._Format(nonce, plainPost).Sha1();
            TokenValidationStatus result = TokenValidationStatus.HashFailed;
            if (checkHash.Equals(hash))
            {
                result = TokenValidationStatus.Success;
            }

            return result;
        }

        /// <summary>
        /// Checks that the specified nonce is no more than
        /// 3 minutes in the past or future
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static TokenValidationStatus ValidateNonce(string nonce, int offset)
        {
            TokenValidationStatus result = TokenValidationStatus.Success;
            Instant requestInstant = Instant.FromString(nonce);
            Instant currentInstant = new Instant();

            int difference = currentInstant.DiffInMilliseconds(requestInstant);
            difference = difference - offset;
            if (TimeSpan.FromMilliseconds(difference).TotalMinutes > 3)
            {
                result = TokenValidationStatus.NonceFailed;
            }
            return result;
        }
    }
}
