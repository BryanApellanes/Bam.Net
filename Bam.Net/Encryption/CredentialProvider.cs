using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public abstract class CredentialProvider : ICredentialProvider
    {
        /// <summary>
        /// Gets or sets the name of the key used to retrieve credentials.
        /// For example, "UserName" would imply that there is a key value
        /// pair with a key of UserName whose value is the name of the user and a key
        /// value pair with a key of UserNamePassword whose value is the password.
        /// </summary>
        /// <value>
        /// The credential key.
        /// </value>
        public string CredentialKey { get; set; }

        public abstract string GetPassword();

        public abstract string GetUserName();
    }
}
