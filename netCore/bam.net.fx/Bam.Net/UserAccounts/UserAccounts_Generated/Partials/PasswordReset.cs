/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;

namespace Bam.Net.UserAccounts.Data
{
    public partial class PasswordReset
    {
        public static PasswordReset Create(string userName, int expiresInMinutesFromNow)
        {
            return Create(User.OneWhere(c => c.UserName == userName), expiresInMinutesFromNow);
        }

        public static PasswordReset Create(long userId, int expiresInMinutesFromNow)
        {
            return Create(User.OneWhere(c => c.Id == userId), expiresInMinutesFromNow);
        }

        /// <summary>
        /// Create a PasswordReset window
        /// </summary>
        /// <param name="user"></param>
        /// <param name="expiresInMinutesFromNow"></param>
        /// <returns></returns>
        public static PasswordReset Create(User user, int expiresInMinutesFromNow)
        {
            Args.ThrowIfNull(user, "user");

            PasswordReset reset = PasswordReset.OneWhere(c => c.UserId == user.Id);
            if (reset == null)
            {
                reset = new PasswordReset();
            }

            reset.DateTime = System.DateTime.UtcNow;
            reset.ExpiresInMinutes = expiresInMinutesFromNow;
            reset.Save();

            return reset;
        }

        public static PasswordReset Last(string userName)
        {
            User user = User.GetByUserNameOrDie(userName);
            return Last(user);
        }

        public static PasswordReset Last(User user)
        {
            return PasswordReset.Top(1, c => c.UserId == user.Id && c.WasReset == true, Order.By<PasswordResetColumns>(c => c.DateTime)).FirstOrDefault();
            
            //return pwdReset == null ? System.DateTime.MinValue : pwdReset.DateTime.Value;

        }
    }
}
