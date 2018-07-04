using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class VaultCredentialProvider : CredentialProvider
    {
        public VaultCredentialProvider(Vault vault, string userNameKey = "User", string passwordKey = "Password")
        {
            Vault = vault;
            UserNameKey = userNameKey;
            PasswordKey = passwordKey;
        }

        public VaultCredentialProvider(VaultInfo vaultInfo, string userNameKey = "User", string passwordKey = "Password") : this(Vault.Load(vaultInfo))
        { }
        
        static VaultCredentialProvider _instance;
        static object _instanceLock = new object();
        public static VaultCredentialProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new VaultCredentialProvider(Vault.System));
            }
            set
            {
                _instance = value;
            }
        }
                
        public string UserNameKey { get; set; }
        public string PasswordKey { get; set; }
        public Vault Vault { get; set; }

        public override string GetPassword()
        {
            return Vault[PasswordKey];
        }

        public override string GetUserName()
        {
            return Vault[UserNameKey];
        }
        
        public override string GetUserNameFor(string targetIdentifier)
        {
            return Vault[$"{targetIdentifier}.{UserNameKey}"];
        }

        public override string GetPasswordFor(string targetIdentifier)
        {
            return Vault[$"{targetIdentifier}.{PasswordKey}"];
        }

        public override string GetUserNameFor(string machineName, string serviceName)
        {
            return Vault[$"{machineName}.{serviceName}.{UserNameKey}"];
        }

        public override string GetPasswordFor(string machineName, string serviceName)
        {
            return Vault[$"{machineName}.{serviceName}.{PasswordKey}"];
        }

    }
}
