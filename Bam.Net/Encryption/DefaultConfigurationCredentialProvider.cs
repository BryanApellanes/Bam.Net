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
