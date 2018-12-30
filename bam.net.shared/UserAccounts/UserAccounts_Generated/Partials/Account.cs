/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Configuration;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts.Data
{
    public partial class Account
    {
        /// <summary>
        /// Creates a new Account with the Created and
        /// Token properties set
        /// </summary>
        /// <returns></returns>
        public static Account Create(User user, string provider, string providerUserId, bool isConfirmed = false, Database db = null)
        {
            DateTime now = DateTime.UtcNow;
            Account result = new Account
            {
                CreationDate = now,
                Provider = provider,
                ProviderUserId = providerUserId,
                Comment = "Account for ({0})::confirmed({1})"._Format(user.UserName, isConfirmed ? "Y" : "N")
            };
            if (isConfirmed)
            {
                result.ConfirmationDate = now;
            }

            result.IsConfirmed = isConfirmed;
            result.UserId = user.Id;
            result.Token = ServiceProxySystem.GenerateId();
            result.IsConfirmed = false;
            result.Save(db);
            user.AccountsByUserId.Reload();
            return result;
        }

        public bool IsExpired
        {
            get
            {
                if (IsConfirmed.Value)
                {
                    return false;
                }
                else
                {
                    return DateTime.UtcNow.Subtract(CreationDate.Value).Days > 5;
                }
            }
        }

        public void Confirm()
        {
            ConfirmationDate = DateTime.UtcNow;
            IsConfirmed = true;
            Save();
        }

        /// <summary>
        /// Expires the confirmation by setting the Created property to DateTime.MinValue
        /// </summary>
        public void Expire()
        {
            CreationDate = DateTime.MinValue;
        }
    }
}
