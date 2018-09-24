/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public class DecryptedVaultItem
    {
        public DecryptedVaultItem(VaultItem item, VaultKey key)
        {
            this.Item = item;
            this.VaultKey = key;
        }

        protected VaultItem Item
        {
            get;
            set;
        }

        protected VaultKey VaultKey
        {
            get;
            set;
        }

        string _key;
        object _keySync = new object();
        public string Key
        {
            get
            {
                return _keySync.DoubleCheckLock(ref _key, () => VaultKey.Password.AesPasswordDecrypt(Item.Key));
            }
        }

        string _value;
        object _valueSync = new object();
        public string Value
        {
            get
            {
                return _valueSync.DoubleCheckLock(ref _value, () =>
                {
                    string password = VaultKey.PrivateKeyDecrypt(VaultKey.Password);
                    return Item.Value.AesPasswordDecrypt(password);
                });                    
            }
            set
            {
                string password = VaultKey.PrivateKeyDecrypt(VaultKey.Password);
                Item.Value = value.AesPasswordEncrypt(password);
                Item.Save();
                _value = null; // force reinit
            }
        }
    }
}
