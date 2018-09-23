using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    /// <summary>
    /// An Aes key, iv and asymmetric key pem string.
    /// </summary>
    public class KeySet
    {
        public KeySet()
        {
            Name = ApplicationNameProvider.Default.GetApplicationName();
            Init();
        }

        static KeySet _forApplication;
        static object _forApplicationLock = new object();
        public static KeySet ForApplication
        {
            get
            {
                return _forApplicationLock.DoubleCheckLock(ref _forApplication, () => Load(ApplicationNameProvider.Default.GetApplicationName()));
            }
        }

        public static FileInfo New(string name)
        {
            KeySet keyset = new KeySet() { Name = name };
            return keyset.Save();
        }

        public FileInfo Save()
        {
            FileInfo file = GetFile(Name);
            this.ToJsonFile(file);
            return file;
        }

        public static KeySet Load(string name)
        {
            FileInfo file = GetFile(name);
            if (!file.Exists)
            {
                file = New(name);
            }
            return Load(file);
        }

        public static KeySet Load(FileInfo file)
        {            
            return file.FromJsonFile<KeySet>();            
        }

        public string Name { get; set; }
        public string AesKeyCipher { get; set; }
        public string AesIvCipher { get; set; }

        string _aesKey;
        protected string AesKey
        {
            get
            {
                if (string.IsNullOrEmpty(_aesKey))
                {
                    _aesKey = AsymmetricDecrypt(AesKeyCipher);
                }
                return _aesKey;
            }
        }

        string _aesIv;
        protected string AesIv
        {
            get
            {
                if (string.IsNullOrEmpty(_aesIv))
                {
                    _aesIv = AsymmetricDecrypt(AesIvCipher);
                }
                return _aesIv;
            }
        }

        public string GetAesKey()
        {
            return AesKey;
        }

        AsymmetricCipherKeyPair _asymmetricKeys;
        string _asymmetricPemString;
        public string AsymmetricKey
        {
            get
            {
                if (string.IsNullOrEmpty(_asymmetricPemString))
                {
                    _asymmetricPemString = _asymmetricKeys.ToPem();
                }
                return _asymmetricPemString;
            }
            set
            {
                _asymmetricPemString = value;
                _asymmetricKeys = _asymmetricPemString.FromPem();
            }
        }

        public string Encrypt(string value)
        {
            return Aes.Encrypt(value, GetAesKeyVectorPair());
        }

        public string Decrypt(string base64EncodedValue)
        {
            return Aes.Decrypt(base64EncodedValue, GetAesKeyVectorPair());
        }

        public string AsymmetricEncrypt(string plainText, IAsymmetricBlockCipher engine = null)
        {
            return PublicKeyEncrypt(plainText, engine);
        }

        /// <summary>
        /// Use private key to decrypt, same as PrivateKeyDecrypt.
        /// </summary>
        /// <param name="cipher">The cipher.</param>
        /// <param name="engine">The engine.</param>
        /// <returns></returns>
        public string AsymmetricDecrypt(string cipher, IAsymmetricBlockCipher engine = null)
        {
            return PrivateKeyDecrypt(cipher, engine);
        }

        /// <summary>
        /// Use public key to encrypt, same as AsymmetricEncrypt.
        /// </summary>
        /// <param name="plainText">The plain text.</param>
        /// <param name="engine">The engine.</param>
        /// <returns></returns>
        public string PublicKeyEncrypt(string plainText, IAsymmetricBlockCipher engine = null)
        {
            AsymmetricKeyParameter key = GetAsymmetricKeys().Public;

            return plainText.EncryptWithPublicKey(key, null, engine);
        }

        public string PrivateKeyDecrypt(string cipher, IAsymmetricBlockCipher engine = null)
        {
            return cipher.DecryptWithPrivateKey(AsymmetricKey.ToKeyPair().Private, null, engine);
        }

        public AesKeyVectorPair GetAesKeyVectorPair()
        {
            return new AesKeyVectorPair { Key = AesKey, IV = AesIv };
        }

        public AsymmetricCipherKeyPair GetAsymmetricKeys()
        {
            return AsymmetricKey.ToKeyPair();
        }

        private static FileInfo GetFile(string name)
        {
            return new FileInfo(Path.Combine(Paths.AppData, $"{name}.keyset"));
        }
        
        private void Init()
        {
            _asymmetricKeys = RsaKeyGen.GenerateKeyPair(RsaKeyLength._2048);
            AsymmetricKey = _asymmetricKeys.ToPem();

            AesKeyVectorPair akvp = new AesKeyVectorPair();
            AesKeyCipher = PublicKeyEncrypt(akvp.Key);
            AesIvCipher = PublicKeyEncrypt(akvp.IV);
        }
    }
}
