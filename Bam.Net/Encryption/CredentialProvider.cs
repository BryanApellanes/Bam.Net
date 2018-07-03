using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public abstract class CredentialProvider : ICredentialProvider, INamedCredentialProvider, IServiceCredentialProvider
    {
        static CredentialProvider _default;
        static object _defaultLock = new object();
        public static CredentialProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => DefaultConfigurationCredentialProvider.Instance);
            }
            set
            {
                _default = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the key used to retrieve credentials.
        /// For example, "User" would imply that there is a key value
        /// pair with a key of User whose value is the name of the user and a key
        /// value pair with a key of UserPassword whose value is the password.
        /// </summary>
        /// <value>
        /// The credential key.
        /// </value>
        public string CredentialKey { get; set; }

        public abstract string GetPassword();

        public abstract string GetUserName();

        public abstract string GetUserNameFor(string targetIdentifier);

        public abstract string GetPasswordFor(string targetIdentifier);

        public abstract string GetUserNameFor(string machineName, string serviceName);

        public abstract string GetPasswordFor(string machineName, string serviceName);
    }
}
