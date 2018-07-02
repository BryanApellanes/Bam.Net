using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class VaultCredentialProvider : ICredentialProvider
    {
        public VaultCredentialProvider(Vault vault, string userNameKey = "UserName", string passwordKey = "Password")
        {
            Vault = vault;
            UserNameKey = userNameKey;
            PasswordKey = passwordKey;
        }

        public VaultCredentialProvider(VaultInfo vaultInfo, string userNameKey = "UserName", string passwordKey = "Password") : this(Vault.Load(vaultInfo))
        { }

        static ICredentialProvider _instance;
        static object _instanceLock = new object();
        public static ICredentialProvider Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new VaultCredentialProvider(Vault.System));
            }
        }

        public string UserNameKey { get; set; }
        public string PasswordKey { get; set; }
        public Vault Vault { get; set; }

        public string GetPassword()
        {
            return Vault[PasswordKey];
        }

        public string GetUserName()
        {
            return Vault[UserNameKey];
        }
    }
}
