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

        static ICredentialProvider _instance;
        static object _instanceLock = new object();
        public static ICredentialProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new DefaultConfigurationCredentialProvider("UserName"));
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
    }
}
