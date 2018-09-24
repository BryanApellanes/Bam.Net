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
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;

namespace Bam.Net.ServiceProxy.Secure
{
    public class ClientSessionInfo
    {
        /// <summary>
        /// The database id of the SecureSession instance on the 
        /// server
        /// </summary>
        public ulong SessionId
        {
            get;
            set;
        }

        /// <summary>
        /// The value of the session cookie
        /// </summary>
        public string ClientIdentifier
        {
            get;
            set;
        }

        /// <summary>
        /// The server Rsa public key of the current session as a Pem string
        /// </summary>
        public string PublicKey
        {
            get;
            set;
        }

        public override bool Equals(object obj)
        {
            if (obj is ClientSessionInfo info)
            {
                return info.SessionId == SessionId && info.ClientIdentifier.Equals(ClientIdentifier) && info.PublicKey.Equals(PublicKey);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(SessionId, ClientIdentifier, PublicKey);
        }

        public override string ToString()
        {
            return $"SessionId={SessionId};ClientIdentifier={ClientIdentifier};PublicKey={PublicKey}";
        }
    }
}
