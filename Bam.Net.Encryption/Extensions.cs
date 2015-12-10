/*
	Copyright © Bryan Apellanes 2015  
*/
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Encryption
{
    public static class Extensions
    {
        public static Email SetCredentials(this Email email, Vault credentialVault)
        {            
            email.SmtpHost(credentialVault["SmtpHost"])
                .UserName(credentialVault["UserName"])
                .Password(credentialVault["Password"]);

            return email;
        }

        public static string AesPasswordEncrypt(this string plainText, string password)
        {
            return new PasswordEncrypted(plainText, password);            
        }

        public static string AesPasswordDecrypt(this string cipher, string password)
        {
            return new PasswordDecrypted(cipher, password);            
        }

        public static AsymmetricCipherKeyPair RsaKeyPair(this RsaKeyLength size)
        {
            RsaKeyPairGenerator gen = new RsaKeyPairGenerator();
            RsaKeyGenerationParameters parameters = new RsaKeyGenerationParameters(new BigInteger("10001", 16), SecureRandom.GetInstance("SHA1PRNG"), (int)size, 80);
            gen.Init(parameters);
            return gen.GenerateKeyPair();
        }
        
        public static string EncryptWithPublicKey(this string input, string publicPemKey, Encoding encoding = null)
        {
            return EncryptWithPublicKey(input, publicPemKey.ToKey(), encoding);
        }
        
        public static string EncryptWithPublicKey(this string input, AsymmetricKeyParameter key, Encoding encoding = null, IAsymmetricBlockCipher engine = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] data = encoding.GetBytes(input);
            byte[] encrypted = data.Encrypt(key, engine);
            return Convert.ToBase64String(encrypted);
        }
                
        public static string DecryptWithPrivateKey(this string cipher, AsymmetricCipherKeyPair keys, Encoding encoding = null)
        {
            return DecryptWithPrivateKey(cipher, keys.Private, encoding);
        }

        public static string DecryptWithPrivateKey(this string cipher, AsymmetricKeyParameter key, Encoding encoding = null, IAsymmetricBlockCipher e = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] encrypted = Convert.FromBase64String(cipher);
            byte[] decrypted = Decrypt(encrypted, key, e);
            return encoding.GetString(decrypted);
        }
        
        public static string DecryptWithPrivateKey(this string cipher, string pemString, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return DecryptWithPrivateKey(cipher, pemString.ToKeyPair(), encoding);
        }

        /// <summary>
        /// Encrypt with the Public key of the specified keyPair
        /// </summary>
        /// <param name="input"></param>
        /// <param name="keyPair"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptWithPublicKey(this string input, AsymmetricCipherKeyPair keyPair, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] data = encoding.GetBytes(input);
            byte[] encrypted = data.EncryptWithPublicKey(keyPair);
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Encrypt the specified input and return the encrypted byte[] converted to 
        /// base 64
        /// </summary>
        /// <param name="input"></param>
        /// <param name="keyPair"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string EncryptWithPrivateKey(this string input, AsymmetricCipherKeyPair keyPair, Encoding encoding = null)
        {
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            byte[] data = encoding.GetBytes(input);
            byte[] encrypted = data.EncryptWithPrivateKey(keyPair);
            return Convert.ToBase64String(encrypted);
        }

        public static byte[] EncryptWithPrivateKey(this byte[] data, AsymmetricCipherKeyPair keyPair)
        {
            return Encrypt(data, keyPair.Private);
        }

        public static byte[] EncryptWithPublicKey(this byte[] data, AsymmetricCipherKeyPair keyPair)
        {
            return Encrypt(data, keyPair.Public);
        }


        public static byte[] Encrypt(this byte[] data, AsymmetricKeyParameter key)
        {
            RsaEngine e = new RsaEngine();
            return Encrypt(data, key, e);
        }

        public static byte[] Encrypt(this byte[] data, AsymmetricKeyParameter key, IAsymmetricBlockCipher e)
        {
            if (e == null)
            {
                e = new RsaEngine();
            }

            e.Init(true, key);

            int blockSize = e.GetInputBlockSize();
            List<byte> output = new List<byte>();
            for (int chunkPosition = 0; chunkPosition < data.Length; chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, data.Length - (chunkPosition * blockSize));
                output.AddRange(e.ProcessBlock(data, chunkPosition, chunkSize));
            }

            return output.ToArray();
        }

        public static byte[] DecryptWithPublicKey(this byte[] data, AsymmetricCipherKeyPair keyPair)
        {
            return Decrypt(data, keyPair.Public);
        }

        public static byte[] DecryptWithPrivateKey(this byte[] data, AsymmetricCipherKeyPair keyPair)
        {
            return Decrypt(data, keyPair.Private);
        }

        public static byte[] Decrypt(this byte[] data, AsymmetricKeyParameter key)
        {
            return Decrypt(data, key, null);
        }

        public static byte[] Decrypt(this byte[] data, AsymmetricKeyParameter key, IAsymmetricBlockCipher e)
        {
            if (e == null)
            {
                e = new RsaEngine();
            }

            e.Init(false, key);

            int blockSize = e.GetInputBlockSize();

            List<byte> output = new List<byte>();
            for (int chunkPosition = 0; chunkPosition < data.Length; chunkPosition += blockSize)
            {
                int chunkSize = Math.Min(blockSize, data.Length - (chunkPosition * blockSize));
                output.AddRange(e.ProcessBlock(data, chunkPosition, chunkSize));
            }

            return output.ToArray();
        }
    }
}
