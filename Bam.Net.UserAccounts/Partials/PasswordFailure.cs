/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;

namespace Bam.Net.UserAccounts.Data
{
    public partial class PasswordFailure
    {
        public static PasswordFailure Add(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);
            PasswordFailure failure = user.PasswordFailuresByUserId.AddNew();
            failure.DateTime = System.DateTime.UtcNow;
            failure.Save();
            return failure;
        }
    }
}
