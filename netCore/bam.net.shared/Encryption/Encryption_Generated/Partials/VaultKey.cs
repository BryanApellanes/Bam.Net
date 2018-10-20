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
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;

namespace Bam.Net.Encryption
{
	public partial class VaultKey
	{
        string _privateKey;
        object _privateKeySync = new object();
        protected string PrivateKey
        {
            get
            {
                return _privateKeySync.DoubleCheckLock(ref _privateKey, () => RsaKey.ToKeyPair().Private.ToPem());
            }
        }
        string _publicKey;
        object _publicKeySync = new object();
        protected string PublicKey
        {
            get
            {
                return _publicKeySync.DoubleCheckLock(ref _publicKey, () => RsaKey.ToKeyPair().Public.ToPem());
            }
        }

        string _passwordHash;
        object _passwordHashSync = new object();
        protected string PasswordHash
        {
            get
            {
                return _passwordHashSync.DoubleCheckLock(ref _passwordHash, () => PrivateKeyDecrypt(Password));
            }
        }

        public string PrivateKeyDecrypt(string cipher)
        {
            return cipher.DecryptWithPrivateKey(PrivateKey);
        }

        public string PublicKeyEncrypt(string plainText)
        {
            return plainText.EncryptWithPublicKey(PublicKey);
        }

        public string Encrypt(string plainText)
        {
            PasswordEncrypted aes = new PasswordEncrypted(plainText, PasswordHash);
            return aes.Value;
        }

        public string Decrypt(string cipher)
        {
            PasswordDecrypted aes = new PasswordDecrypted(cipher, PasswordHash);
            return aes.Value;
        }
	}
}																								
