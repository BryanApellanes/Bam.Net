/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using System.IO;

namespace Bam.Net.Encryption
{
    public static class Pem
    {
        public static string ToPem(this AsymmetricKeyParameter key)
        {
            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(key);
            return stringWriter.ToString();
        }

        /// <summary>
        /// Returns the public portion of the specified keyPair in 
        /// pem format (compatible with openssl)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        public static string PublicKeyToPem(this AsymmetricCipherKeyPair keyPair)
        {
            return FromPublicKey(keyPair);
        }

        /// <summary>
        /// Returns the public portion of the specified keyPair in 
        /// pem format (compatible with openssl)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        public static string FromPublicKey(AsymmetricCipherKeyPair keyPair)
        {
            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(keyPair.Public);
            return stringWriter.ToString();
        }

        /// <summary>
        /// Returns the specified keyPair in
        /// pem format (compatible with openssl)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        public static string ToPem(this AsymmetricCipherKeyPair keyPair)
        {
            return FromPrivateKey(keyPair);
        }

        /// <summary>
        /// Returns the private portion of the specified keyPair in
        /// pem format (compaitble with openssl)
        /// </summary>
        /// <param name="keyPair"></param>
        /// <returns></returns>
        public static string FromPrivateKey(AsymmetricCipherKeyPair keyPair)
        {
            StringWriter stringWriter = new StringWriter();
            PemWriter pemWriter = new PemWriter(stringWriter);
            pemWriter.WriteObject(keyPair.Private);
            return stringWriter.ToString();
        }

        public static AsymmetricKeyParameter ToKey(this string pemString)
        {
            TextReader reader = new StringReader(pemString);
            PemReader pemReader = new PemReader(reader);
            object pemObject = pemReader.ReadObject();
            return (AsymmetricKeyParameter)pemObject;
        }

        public static T FromPem<T>(this string pemString)
        {
            TextReader reader = new StringReader(pemString);
            PemReader pemReader = new PemReader(reader);
            object pemObject = pemReader.ReadObject();
            return (T)pemObject;
        }

        public static AsymmetricCipherKeyPair FromPem(this string pemString)
        {
            return pemString.ToKeyPair();
        }

        public static AsymmetricCipherKeyPair ToKeyPair(this string pemString)
        {
            TextReader reader = new StringReader(pemString);
            PemReader pemReader = new PemReader(reader);
            object pemObject = pemReader.ReadObject();
            return (AsymmetricCipherKeyPair)pemObject;
        }
    }
}
