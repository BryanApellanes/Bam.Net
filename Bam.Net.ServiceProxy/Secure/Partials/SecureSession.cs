/*
	Copyright © Bryan Apellanes 2015  
*/
// Model is Table
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;
using Bam.Net.ServiceProxy;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Net;
using Bam.Net.Encryption;
using System.Web.Mvc;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;


namespace Bam.Net.ServiceProxy.Secure
{
    public partial class SecureSession
    {
        static SecureSession()
        {
            DefaultKeySize = RsaKeyLength._1024;
        }
        public static RsaKeyLength DefaultKeySize
        {
            get;
            set;
        }

        /// <summary>
        /// The name of the cookie used to hold the 
        /// session identifier.  Returns ServiceProxySystem.SessionName
        /// </summary>
        public static string CookieName
        {
            get
            {
                return ServiceProxySystem.SecureSessionName;
            }
        }

        AsymmetricKeyParameter _privateKeyParameter;
        object _privateKeyLock = new object();
        protected internal AsymmetricKeyParameter PrivateKeyParameter
        {
            get
            {
                return _privateKeyLock.DoubleCheckLock(ref _privateKeyParameter, () => AsymmetricKey.ToKeyPair().Private);
            }
        }

        AsymmetricKeyParameter _publicKeyParameter;
        object _publicKeyLock = new object();
        protected internal AsymmetricKeyParameter PublicKeyParameter
        {
            get
            {
                return _publicKeyLock.DoubleCheckLock(ref _publicKeyParameter, () => AsymmetricKey.ToKeyPair().Public);
            }
        }

        public static SecureSession Init(IHttpContext context)
        {
            return Get(context);
        }

        /// <summary>
        /// Gets a SecureSession instance for the specified
        /// context using context.Request creating the session if
        /// necessary.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="instant"></param>
        /// <returns></returns>
        public static SecureSession Get(IHttpContext context, Instant instant = null)//, KeyTypeEnum clientType = KeyTypeEnum.Private)
        {
            return Get(context.Request, context.Response, instant);
        }

        public static string GenerateId()
        {
            return ServiceProxySystem.GenerateId();
        }

        /// <summary>
        /// Gets a SecureSession instance for the specified request
        /// creating it if necessary.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="instant"></param>
        /// <returns></returns>
        public static SecureSession Get(IRequest request, IResponse response, Instant instant = null)
        {
            Args.ThrowIfNull(request, "request");

            Cookie cookie = request.Cookies[CookieName];
            if (cookie == null)
            {
                string sid = request.Headers[CookieName];
                if (string.IsNullOrEmpty(sid))
                {
                    sid = GenerateId();
                }
                cookie = new Cookie(CookieName, sid);
                response.Cookies.Add(cookie);
            }

            return Get(cookie, instant);
        }


        public static SecureSession Get(IRequest request, Instant instant = null)
        {
            return Get(request.Cookies[SecureSession.CookieName], instant);
        }

        public static SecureSession Get(Cookie cookie, Instant instant = null)
        {
            Args.ThrowIfNull(cookie, "cookie");

            SecureSession result = null;
            if (cookie != null && cookie.Name.Equals(CookieName))
            {
                result = Get(cookie.Value, instant);
            }

            return result;
        }

        public static SecureSession Get(NameValueCollection headers, Instant instant = null)
        {
            string sessionIdentifier = headers[CookieName];            
            Args.ThrowIfNull(sessionIdentifier, CookieName);
            return Get(sessionIdentifier, instant);
        }

        /// <summary>
        /// Gets a SecureSession with the specified sessionIdentifier creating it
        /// if necessary
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public static SecureSession Get(string sessionIdentifier, Instant instant = null)
        {
            SecureSession result = SecureSession.OneWhere(c => c.Identifier == sessionIdentifier);
            if (result == null)
            {
                result = CreateSession(sessionIdentifier, instant);
            }

            return result;
        }

        public void SetSymmetricKey(string key, string iv)
        {
            string keyCipher = EncryptWithPublicKey(key);
            string keyHash = key.Sha1();
            string keyHashCipher = keyHash.EncryptWithPublicKey(PublicKey, Encoding.UTF8);
            string ivHash = iv.Sha1();
            string ivCipher = EncryptWithPublicKey(iv);
            string ivHashCipher = EncryptWithPublicKey(ivHash);
            SetSymmetricKey(keyCipher, keyHashCipher, ivCipher, ivHashCipher);
        }

        public void SetSymmetricKey(string keyCipher, string keyHashCipher, string ivCipher, string ivHashCipher)
        {
            SetSymmetricKey(new SetSessionKeyRequest(keyCipher, keyHashCipher, ivCipher, ivHashCipher));
        }

        public void SetSymmetricKey(SetSessionKeyRequest request)
        {
            PlainSymmetricKey = DecryptWithPrivateKey(request.KeyCipher, request.GetEngine());
            string passwordHash = DecryptWithPrivateKey(request.KeyHashCipher, request.GetEngine());
            Expect.AreEqual(PlainSymmetricKey.Sha1(), passwordHash, "Key hash check failed");
            PlainSymmetricIV = DecryptWithPrivateKey(request.IVCipher, request.GetEngine());
            string ivHash = DecryptWithPrivateKey(request.IVHashCipher, request.GetEngine());
            Expect.AreEqual(PlainSymmetricIV.Sha1(), ivHash, "IV hash check failed");
            this.Save();
        }

        string _plainSymmetricKey;
        object _plainSymmetricKeyLock = new object();
        protected internal string PlainSymmetricKey
        {
            get
            {
                return _plainSymmetricKeyLock.DoubleCheckLock(ref _plainSymmetricKey, () => DecryptWithPrivateKey(SymmetricKey));                
            }
            private set
            {
                _plainSymmetricKey = value;
                SymmetricKey = _plainSymmetricKey.EncryptWithPublicKey(AsymmetricKey.ToKeyPair().Public);
            }
        }

        string _plainSymmetricIV;
        object _plainSymmetricIVLock = new object();
        protected internal string PlainSymmetricIV
        {
            get
            {
                return _plainSymmetricIVLock.DoubleCheckLock(ref _plainSymmetricIV, () => DecryptWithPrivateKey(SymmetricIV));
            }
            private set
            {
                _plainSymmetricIV = value;
                SymmetricIV = _plainSymmetricIV.EncryptWithPublicKey(AsymmetricKey.ToKeyPair().Public);
            }
        }

        /// <summary>
        /// Perform symmetric encryption on the specified plainText
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public string Encrypt(string plainText)
        {
            Encrypted encryptor = new Encrypted(plainText, PlainSymmetricKey, PlainSymmetricIV);
            return encryptor.Value;
        }

        /// <summary>
        /// Perform symmetric decryption on the specified cipher
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public string Decrypt(string cipher)
        {
            Decrypted decryptor = new Decrypted(cipher, PlainSymmetricKey, PlainSymmetricIV);
            return decryptor.Value;
        }

        protected internal string DecryptWithPrivateKey(string cipher, bool usePkcsPadding)
        {
            return DecryptWithPrivateKey(cipher, RsaCrypto.GetRsaEngine(usePkcsPadding));
        }

        /// <summary>
        /// Perform asymmetric decryption on the specified cipher
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        protected internal string DecryptWithPrivateKey(string cipher, IAsymmetricBlockCipher engine = null)
        {
            return cipher.DecryptWithPrivateKey(AsymmetricKey.ToKeyPair().Private, null, engine);
        }

        /// <summary>
        /// Perform asymmetric encryption on the specified plainText
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        protected internal string EncryptWithPublicKey(string plainText, IAsymmetricBlockCipher engine = null)
        {
            AsymmetricKeyParameter key = AsymmetricKey.ToKeyPair().Public;// GetPublicKey();

            return plainText.EncryptWithPublicKey(key, null, engine);
        }
        
        string _publicKey;
        public string PublicKey
        {
            get
            {
                if (string.IsNullOrEmpty(_publicKey))
                {
                    _publicKey = AsymmetricKey.ToKeyPair().Public.ToPem();
                }
                return _publicKey;
            }            
        }

        string _privateKey;
        public string PrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(_privateKey))
                {
                    _privateKey = AsymmetricKey.ToKeyPair().Private.ToPem();
                }

                return _privateKey;
            }
        }

        private static SecureSession CreateSession(string identifier, Instant instant = null)
        {
            if (instant == null)
            {
                instant = new Instant();
            }

            DateTime now = DateTime.UtcNow;
            SecureSession result = new SecureSession();
            result.Identifier = identifier;
            result.CreationDate = now;
            result.LastActivity = now;
            result.TimeOffset = instant.DiffInMilliseconds(now);
            result.IsActive = true;
            
            AsymmetricCipherKeyPair keys = RsaKeyGen.GenerateKeyPair(DefaultKeySize);
            result.AsymmetricKey = keys.ToPem();

            AesKeyVectorPair kvp = new AesKeyVectorPair();
            result.SymmetricKey = kvp.Key.EncryptWithPublicKey(keys.Public);
            result.SymmetricIV = kvp.IV.EncryptWithPublicKey(keys.Public);
            
            result.Save();
            return result;
        }
    }
}																								
