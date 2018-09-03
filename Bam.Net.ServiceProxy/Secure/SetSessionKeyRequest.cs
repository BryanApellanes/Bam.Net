/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;
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

namespace Bam.Net.ServiceProxy.Secure
{
    [Serializable]
    public class SetSessionKeyRequest
    {
        public SetSessionKeyRequest(string passwordCipher, string passwordHashCipher, string ivCipher, string ivHashCipher, bool? usePkcsPadding = false)
        {
            this.KeyCipher = passwordCipher;
            this.KeyHashCipher = passwordHashCipher;
            this.IVCipher = ivCipher;
            this.IVHashCipher = ivHashCipher;
            this.UsePkcsPadding = usePkcsPadding;
        }

        public string KeyCipher
        {
            get;
            set;
        }

        public string KeyHashCipher
        {
            get;
            set;
        }

        public string IVCipher
        {
            get;
            set;
        }

        public string IVHashCipher
        {
            get;
            set;
        }

        public bool? UsePkcsPadding
        {
            get;
            set;
        }

        protected internal IAsymmetricBlockCipher GetEngine()
        {
            return RsaCrypto.GetRsaEngine(UsePkcsPadding.Value);
        }
    }
}
