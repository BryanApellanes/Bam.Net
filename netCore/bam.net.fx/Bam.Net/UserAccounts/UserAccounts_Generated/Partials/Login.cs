/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Encryption;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public partial class Login
    {
        public static Login Last(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Last(user);
        }

        public static Login Last(User user)
        {
            return Login.Top(1, c => c.UserId == user.Id, Order.By<LoginColumns>(c => c.DateTime, SortOrder.Descending)).FirstOrDefault();            
        }

        public static Login Add(string userName)
        {
            return Add(User.GetByUserNameOrDie(userName));
        }
        public static Login Add(User user, Database db = null)
        {
            return Add(user.Id.Value, db);
        }

        public static Login Add(ulong userId, Database db = null)
        {
            Login result = new Login();
            result.UserId = userId;
            result.DateTime = new Instant().ToDateTime();
            result.Save(db);
            return result;
        }
    }
}
