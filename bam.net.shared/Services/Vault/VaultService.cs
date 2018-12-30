using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy.Secure;

namespace Bam.Net.Services
{
    [Proxy("vaultSvc")]
    [ApiKeyRequired]
    public class VaultService : AsyncProxyableService, IVaultService
    {
        public VaultService(VaultDatabase db)
        {
            Database = db;
            
        }

        [Local]
        public VaultDatabase Database { get; set; }

        public override object Clone()
        {
            VaultService clone = new VaultService(Database);
            clone.CopyProperties(this);
            clone.CopyEventHandlers(this);
            return clone;
        }

        Vault _applicationVault;
        object _applicationVaultLock = new object();
        protected Vault ApplicationVault
        {
            get
            {
                return _applicationVaultLock.DoubleCheckLock(ref _applicationVault, () => Vault.Create(Database, "ApplicationVault", Vault.GeneratePassword()));
            }
        }

        Vault _userVault;
        object _userVaultLock = new object();
        protected Vault UserVault
        {
            get
            {
                return _userVaultLock.DoubleCheckLock(ref _userVault, () => Vault.Create(Database, "UserVault", Vault.GeneratePassword()));
            }
        }

        public virtual string GetUserValue(string keyName)
        {
            return UserVault[keyName];
        }

        public virtual string GetValue(string keyName)
        {
            return ApplicationVault[keyName];
        }

        public virtual void SetUserValue(string keyName, string value)
        {
            UserVault[keyName] = value;
        }

        public virtual void SetValue(string keyName, string value)
        {
            ApplicationVault[keyName] = value;
        }

        public virtual Dictionary<string, string> ExportValues()
        {
            return ApplicationVault.ExportValues();
        }

        public virtual Dictionary<string, string> ExportUserValues()
        {
            return UserVault.ExportValues();
        }

        public virtual void ImportValues(Dictionary<string, string> values)
        {
            ApplicationVault.ImportValues(values);
        }

        public virtual void ImportUserValues(Dictionary<string, string> values)
        {
            UserVault.ImportValues(values);
        }
    }
}
