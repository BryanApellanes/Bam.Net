using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class CredentialManager : ICredentialManager
    {
        public const string DefaultVaultName = "Credentials";

        /// <summary>
        /// Initializes a new instance of the <see cref="CredentialManager"/> class.
        /// The Vault and CredentialProvider are set to VaultCredentialProvider.Instance.Vault
        /// and VaultCredentialProvider.Instance.
        /// </summary>
        public CredentialManager()
        {
            UserNameKey = "User";
            PasswordKey = "Password";
            Vault = VaultCredentialProvider.Instance.Vault;
            CredentialProvider = VaultCredentialProvider.Instance;            
        }

        public CredentialManager(Vault vault)
        {
            Args.ThrowIfNull(vault, "vault");
            UserNameKey = "User";
            PasswordKey = "Password";
            Vault = vault;
            CredentialProvider = new VaultCredentialProvider(vault);
        }

        public CredentialManager(string vaultDatabaseFilePath, string vaultName = DefaultVaultName) 
            : this(Vault.Load(vaultDatabaseFilePath, vaultName))
        { }

        public virtual CredentialProvider CredentialProvider { get; }

        public static VaultInfo DefaultLocalCredentialVaultInfo
        {
            get
            {
                return new VaultInfo { FilePath = Path.Combine(Paths.Local, "Credentials.vaults.sqlite"), Name = DefaultVaultName };
            }
        }

        static Vault _localCredsVault;
        static object _lockLocalCreds = new object();
        public static Vault LocalCredentialVault
        {
            get
            {
                return _lockLocalCreds.DoubleCheckLock(ref _localCredsVault, () => Vault.Load(DefaultLocalCredentialVaultInfo));
            }
            set
            {
                _localCredsVault = value;
            }
        }

        static CredentialManager _local;
        static object _localLock = new object();
        public static CredentialManager Local
        {
            get
            {
                return _localLock.DoubleCheckLock(ref _local, () => new CredentialManager(LocalCredentialVault));
            }
            set
            {
                _local = value;
            }
        }

        public Vault Vault { get; set; }

        public void SetCredentials(CredentialInfo info)
        {
            if (!string.IsNullOrEmpty(info.TargetService) && !string.IsNullOrEmpty(info.MachineName))
            {
                SetUserNameFor(info.MachineName, info.TargetService, info.UserName);
                SetPasswordFor(info.MachineName, info.TargetService, info.Password);
                return;
            }

            if(!string.IsNullOrEmpty(info.TargetService) && string.IsNullOrEmpty(info.MachineName))
            {
                SetUserNameFor(info.TargetService, info.UserName);
                SetPasswordFor(info.TargetService, info.Password);
                return;
            }

            SetUserName(info.UserName);
            SetPassword(info.Password);
        }

        public CredentialInfo GetCredentials()
        {
            return new CredentialInfo
            {
                UserName = GetUserName(),
                Password = GetPassword()
            };
        }

        public CredentialInfo GetCredentials(string targetService)
        {
            return new CredentialInfo
            {
                UserName = GetUserNameFor(targetService),
                Password = GetPasswordFor(targetService),
                TargetService = targetService
            };
        }

        public CredentialInfo GetCredentials(string machineName, string targetService)
        {
            return new CredentialInfo
            {
                UserName = GetUserNameFor(machineName, targetService),
                Password = GetPasswordFor(machineName, targetService),
                MachineName = machineName,
                TargetService = targetService
            };
        }

        /// <summary>
        /// Gets or sets the user name key.  Not to be confused
        /// with an encryption key this value is the "Key" in 
        /// "Key Value pair".
        /// </summary>
        /// <value>
        /// The user name key.
        /// </value>
        public string UserNameKey { get; set; }

        /// <summary>
        /// Gets or sets the password key.  Not to be confused
        /// with an encryption key this value is the "Key" in 
        /// "Key Value pair".
        /// </summary>
        /// <value>
        /// The password key.
        /// </value>
        public string PasswordKey { get; set; }

        /// <summary>
        /// Sets the name of the default user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        public void SetUserName(string userName)
        {
            Vault[UserNameKey] = userName;
        }

        public void SetPassword(string password)
        {
            Vault[PasswordKey] = password;
        }

        public void SetUserNameFor(string targetIdentifier, string userName)
        {
            Vault[$"{targetIdentifier}.{UserNameKey}"] = userName;
        }

        public void SetPasswordFor(string targetIdentifier, string password)
        {
            Vault[$"{targetIdentifier}.{PasswordKey}"] = password;
        }

        public void SetUserNameFor(string machineName, string serviceName, string userName)
        {
            Vault[$"{machineName}.{serviceName}.{UserNameKey}"] = userName;
        }

        public void SetPasswordFor(string machineName, string serviceName, string password)
        {
            Vault[$"{machineName}.{serviceName}.{PasswordKey}"] = password;
        }

        public string GetUserName()
        {
            return CredentialProvider.GetUserName();
        }

        public string GetPassword()
        {
            return CredentialProvider.GetPassword();
        }

        public string GetUserNameFor(string targetIdentifier)
        {
            return CredentialProvider.GetUserNameFor(targetIdentifier);
        }

        public string GetPasswordFor(string targetIdentifier)
        {
            return CredentialProvider.GetPasswordFor(targetIdentifier);
        }

        public string GetUserNameFor(string machineName, string serviceName)
        {
            return CredentialProvider.GetUserNameFor(machineName, serviceName);
        }

        public string GetPasswordFor(string machineName, string serviceName)
        {
            return CredentialProvider.GetPasswordFor(machineName, serviceName);
        }
    }
}
