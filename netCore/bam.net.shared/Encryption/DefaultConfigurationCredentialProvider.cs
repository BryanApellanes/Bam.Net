using Bam.Net.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class DefaultConfigurationCredentialProvider : CredentialProvider
    {
        public DefaultConfigurationCredentialProvider(string credentialKey)
        {
            CredentialKey = credentialKey;
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

        static CredentialProvider _instance;
        static object _instanceLock = new object();
        public static CredentialProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationCredentialProvider("User"));
            }
        }

        public override string GetPassword()
        {
            return DefaultConfiguration.GetAppSetting($"{CredentialKey}Password", string.Empty);
        }

        public override string GetUserName()
        {
            return DefaultConfiguration.GetAppSetting(CredentialKey, string.Empty);
        }

        public override string GetUserNameFor(string targetIdentifier)
        {
            return DefaultConfiguration.GetAppSetting($"{targetIdentifier}.{CredentialKey}", string.Empty);
        }

        public override string GetPasswordFor(string targetIdentifier)
        {
            return DefaultConfiguration.GetAppSetting($"{targetIdentifier}.{CredentialKey}Password", string.Empty);
        }

        public override string GetUserNameFor(string machineName, string serviceName)
        {
            return DefaultConfiguration.GetAppSetting($"{machineName}.{serviceName}.{CredentialKey}");
        }

        public override string GetPasswordFor(string machineName, string serviceName)
        {
            return DefaultConfiguration.GetAppSetting($"{machineName}.{serviceName}.{CredentialKey}Password");
        }
    }
}
