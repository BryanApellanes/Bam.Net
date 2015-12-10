/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;

namespace Bam.Net.Encryption
{
    public class RsaCrypto
    {

        public static IAsymmetricBlockCipher GetRsaEngine(bool usePkcsPadding)
        {
            IAsymmetricBlockCipher result = new RsaEngine();
            if (usePkcsPadding)
            {
                result = new Pkcs1Encoding(result); // wrap the engine in a padded encoding
            }

            return result;
        }

        public static AsymmetricCipherKeyPair GenerateKeyPair(RsaKeyLength size)
        {
            return RsaKeyGen.GenerateKeyPair(size);
        }
    }
}
