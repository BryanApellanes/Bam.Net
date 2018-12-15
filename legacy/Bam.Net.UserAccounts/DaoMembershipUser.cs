/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    [Obsolete("This class will be removed in the future to remove dependency on its dependents; use Bam.Net.UserAccounts.Data.User instead")]
    public class DaoMembershipUser: MembershipUser
    {
        public DaoMembershipUser(
            string providerName,
            string name,
            object providerUserKey,
            string email,
            string passwordQuestion,
            string comment,
            bool isApproved,
            bool isLockedOut,
            DateTime creationDate,
            DateTime lastLoginDate,
            DateTime lastActivityDate,
            DateTime lastPasswordChangedDate,
            DateTime lastLockoutDate)
            : base(
            providerName, 
            name, 
            providerUserKey, 
            email, 
            passwordQuestion, 
            comment, 
            isApproved, 
            isLockedOut,
            creationDate, 
            lastLoginDate,
            lastActivityDate, 
            lastPasswordChangedDate, 
            lastLockoutDate)
        {

        }

        public DaoMembershipUser(string providerName, string passwordQuestion, string comment, User user)
            : this(
            providerName, 
            user.UserName, 
            user.Id, 
            user.Email, 
            passwordQuestion, 
            comment, 
            user.IsApproved.Value, 
            user.IsLockedOut, 
            user.CreationDate.Value, 
            user.LastLoginDate,
            user.LastAcitivtyDate,
            user.LastPasswordChangedDate,
            user.LastLockoutDate)
        {

        }

        public virtual bool ChangePassword(string oldPassword, string newPassword)
        {
            User user = User.GetByUserNameOrDie(UserName);
            if (user.ValidatePassword(oldPassword))
            {
                user.SetPassword(newPassword);
                return true;
            }

            return false;
        }

        public virtual bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            User user = User.GetByUserNameOrDie(UserName);
            return user.ChangePasswordQuestionAndAnswer(password, newPasswordQuestion, newPasswordAnswer);
        }

        public virtual string GetPassword()
        {
            User user = User.GetByUserNameOrDie(UserName);
            return user.GetPassword();
        }
        public virtual string GetPassword(string passwordAnswer)
        {
            User user = User.GetByUserNameOrDie(UserName);
            return user.GetPassword(passwordAnswer);
        }

        public virtual string ResetPassword()
        {
            User user = User.GetByUserNameOrDie(UserName);
            return user.ResetPassword();
        }
        public virtual string ResetPassword(string passwordAnswer)
        {
            User user = User.GetByUserNameOrDie(UserName);
            return user.ResetPassword(passwordAnswer);
        }

        public virtual bool UnlockUser()
        {
            User user = User.GetByUserNameOrDie(UserName);
            user.IsLockedOut = false;
            return !user.IsLockedOut;
        }
    }
}
