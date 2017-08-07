/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using WebMatrix.WebData;
using Bam.Net.Data;
using Bam.Net.UserAccounts.Data;
using System.Linq.Expressions;
using Bam.Net.Configuration;
using Bam.Net.Logging;
using System.Reflection;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts
{
    /// <summary>
    /// This class should be moved to a different assembly for custom "Membership" providers
    /// </summary>
    [Obsolete("This class will be removed in the future to remove dependency on its dependents; use Bam.Net.UserAccounts.UserManager instead")]
    public class DaoMembershipProvider: ExtendedMembershipProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        static DaoMembershipProvider _default;
        static object _defaultLock = new object();
        public static DaoMembershipProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => new DaoMembershipProvider());
            }
        }

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            bool result = false;
            Account account = Account.OneWhere(c => c.Token == accountConfirmationToken);
            if (account != null)
            {
                if (!account.IsExpired)
                {
                    account.ConfirmationDate = DateTime.UtcNow;
                    account.IsConfirmed = true;
                    account.Save();
                    result = true;
                }                
            }

            return result;
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            bool result = false;
            User user = User.OneWhere(c => c.UserName == userName);
            if (user != null)
            {
                Account account = Account.OneWhere(c => c.Token == accountConfirmationToken && c.UserId == user.Id);
                if (account != null)
                {
                    if (!account.IsExpired)
                    {
                        account.ConfirmationDate = DateTime.UtcNow;
                        account.IsConfirmed = true;
                        account.Save();
                        result = true;
                    }
                }
            }

            return result;
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            return CreateUserAndAccount(userName, password, requireConfirmationToken, new Dictionary<string, object>());
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation, IDictionary<string, object> values)
        {
            User user = User.OneWhere(c => c.UserName == userName && c.IsDeleted != true);
            
            if (user != null)
            {
                throw new UserNameInUseException();
            }

            bool isApproved = !requireConfirmation;
            user = User.Create(userName, userName, password, isApproved, true, isApproved);

            if (values != null && values.Count > 0)
            {
                foreach (string key in values.Keys)
                {
                    object value = values[key];
                    if (value != null)
                    {
                        Setting setting = user.SettingsByUserId.AddNew();
                        setting.Key = key;
                        setting.ValueType = value.GetType().AssemblyQualifiedName;
                        setting.Value = value.ToBinaryBytes();
                    }
                }
            }

            user.Save();
            Account userAccount = user.AccountsByUserId.FirstOrDefault();
            if (userAccount == null)
            {
                throw new AccountCreationFailedException(userName);
            }

            return userAccount.Token;
        }

        public override bool DeleteAccount(string userName)
        {
            User user = User.OneWhere(c => c.UserName == userName);
            if (user != null)
            {
                user.IsDeleted = true;
                user.AccountsByUserId.Each((a) =>
                {
                    a.IsConfirmed = false;
                });
                user.Save();
                return true;
            }

            return false;
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            PasswordReset reset = PasswordReset.Create(userName, tokenExpirationInMinutesFromNow);
            return reset.Token;            
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            User user = User.GetByUserName(userName);
            List<OAuthAccountData> result = new List<OAuthAccountData>();

            if (user != null)
            {
                foreach (Account acct in user.AccountsByUserId)
                {
                    if (!acct.Provider.Equals(ApplicationName))
                    {
                        result.Add(new OAuthAccountData(acct.Provider, acct.ProviderUserId));
                    }
                }
            }

            return result;
        }

        public override DateTime GetCreateDate(string userName)
        {
            User user = User.GetByUserName(userName);
            if (user == null)
            {
                return DateTime.MinValue;
            }

            return user.CreationDate.Value;
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            User user = User.GetByUserName(userName);
            DateTime latest = DateTime.MinValue;

            if (user != null)
            {
                foreach (PasswordFailure failure in user.PasswordFailuresByUserId)
                {
                    if (failure.DateTime.Value > latest)
                    {
                        latest = failure.DateTime.Value;
                    }
                }
            }

            return latest;
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            User user = User.GetByUserName(userName);
            if (user == null)
            {
                return DateTime.MinValue;
            }

            PasswordResetCollection resets = PasswordReset.Where(c => c.UserId == user.Id && c.WasReset == true);
            DateTime result = user.CreationDate.Value;
            if (resets.Count > 0)
            {
                List<PasswordReset> sorted = resets.Sorted((l, r) => l.DateTime.Value.CompareTo(r.DateTime.Value));
                result = sorted[0].DateTime.Value;
            }

            return result;
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            User user = User.GetByUserName(userName);
            if (user == null)
            {
                return -1;
            }

            OrderBy<LoginColumns> ob = new OrderBy<LoginColumns>(c => c.DateTime, SortOrder.Descending);
            LoginCollection latestLogin = Login.Where(c => c.UserId == user.Id, ob);
            PasswordFailureCollection failures = PasswordFailure.Where(c => c.UserId == user.Id);
            List<PasswordFailure> sinceLast = new List<PasswordFailure>();
            if (latestLogin.Count > 0)
            {
                DateTime since = latestLogin[0].DateTime.Value;
                foreach (PasswordFailure failure in failures)
                {
                    if (failure.DateTime > since)
                    {
                        sinceLast.Add(failure);
                    }
                }
            }

            return sinceLast.Count;
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            PasswordReset reset = PasswordReset.OneWhere(c => c.Token == token);
            if (reset == null)
            {
                throw new InvalidOperationException("Specified token was not found ({0})"._Format(token));
            }

            return Convert.ToInt32(reset.UserId);
        }

        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            Account oauthAccount = Account.OneWhere(c => c.Provider == provider && c.ProviderUserId == providerUserId);
            if (oauthAccount != null && oauthAccount.UserOfUserId != null)
            {
                return (int)oauthAccount.UserOfUserId.Id.Value;
            }

            return -1;
        }

        public override string GetUserNameFromId(int userId)
        {
            User user = User.OneWhere(c => c.Id == userId);
            if (user != null)
            {
                return user.UserName;
            }

            return string.Empty;
        }

        public override bool IsConfirmed(string userName)
        {
            User user = User.GetByUserName(userName);
            if (user == null)
            {
                return false;
            }

            Account account = (from c in user.AccountsByUserId
                                         where c.IsConfirmed.Value
                                         select c).FirstOrDefault();
            return account != null;
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            PasswordReset reset = PasswordReset.OneWhere(c => c.Token == token);
            if (reset == null)
            {
                return false;
            }
            reset.WasReset = true;

            Password password = Password.OneWhere(c => c.UserId == reset.UserId);
            password.Value = newPassword;

            password.Save();
            reset.Save();

            return true;
        }

        public override string ApplicationName
        {
            get
            {
                return DefaultConfiguration.GetAppSetting("ApplicationName", DefaultConfiguration.DefaultApplicationName);
            }
            set
            {
                // must be in the config file
                Log.AddEntry("An attempt was made to set the ApplicationName property of {0}", this.GetType().Name);
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            User user = User.GetByUserName(username);
            if (user != null)
            {
                Password password = user.PasswordsByUserId.JustOne();
                if (password.Value.Equals(oldPassword))
                {
                    password.Value = newPassword;
                    password.Save();
                    return true;
                }
            }

            return false;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            User user = User.GetByUserName(username);
            bool result = false;
            if (user != null)
            {
                result = user.ChangePasswordQuestionAndAnswer(password, newPasswordQuestion, newPasswordAnswer);
            }

            return result;
        }

        public override void CreateOrUpdateOAuthAccount(string provider, string providerUserId, string userName)
        {
            User user = User.OneWhere(c => c.UserName == userName);
            if (user == null)
            {
                user = User.Create(userName);
            }

            Account account = Account.OneWhere(c => c.Provider == provider && c.ProviderUserId == providerUserId);
            if (account == null)
            {
                account = Account.Create(user, provider, providerUserId, true);
            }
            else
            {
                account.UserId = user.Id.Value;
            }

            account.Save();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;
            User user = User.OneWhere(c => c.Id == Convert.ToInt32(providerUserKey));
            if (user != null)
            {
                status = MembershipCreateStatus.DuplicateProviderUserKey;
            }

            if (!email.Contains("@") && !email.Contains(".")) // bare minimum email check
            {
                status = MembershipCreateStatus.InvalidEmail;
            }

            try
            {
                user = User.Create(username, email, password, isApproved);
            }
            catch (UserNameInUseException uniue)
            {
                Log.AddEntry("{0}.{1}::{2}", uniue, this.GetType().Name, MethodBase.GetCurrentMethod().Name, uniue.Message);
                status = MembershipCreateStatus.DuplicateUserName;
            }
            catch (EmailAlreadyRegisteredException eare)
            {
                Log.AddEntry("{0}.{1}::{2}", eare, this.GetType().Name, MethodBase.GetCurrentMethod().Name, eare.Message);
                status = MembershipCreateStatus.DuplicateEmail;
            }

            PasswordQuestion question = user.PasswordQuestionsByUserId.FirstOrDefault();
            if (status == MembershipCreateStatus.Success)
            {
                if (question == null)
                {
                    question = user.PasswordQuestionsByUserId.AddNew();
                    question.Value = passwordQuestion;
                    question.Answer = passwordAnswer;
                    question.Save();
                    user.PasswordResetsByUserId.Reload();
                }
            }

            MembershipUser result = User.GetMembershipUser(user);

            return result;
        }       

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            User user = User.GetByUserName(username);
            bool result = false;
            try
            {
                if (user != null)
                {
                    user.IsDeleted = true;
                    user.IsApproved = false;
                    user.Save();
                    if (deleteAllRelatedData)
                    {
                        UserRoleCollection roles = UserRole.Where(c => c.UserId == user.Id);
                        AccountCollection accounts = Account.Where(c => c.UserId == user.Id);
                        PasswordCollection passwords = Password.Where(c => c.UserId == user.Id);
                        PasswordResetCollection resets = PasswordReset.Where(c => c.UserId == user.Id);
                        PasswordFailureCollection failures = PasswordFailure.Where(c => c.UserId == user.Id);
                        LockOutCollection lockouts = LockOut.Where(c => c.UserId == user.Id);
                        LoginCollection logins = Login.Where(c => c.UserId == user.Id);
                        PasswordQuestionCollection questions = PasswordQuestion.Where(c => c.UserId == user.Id);
                        SettingCollection settings = Setting.Where(c => c.UserId == user.Id);
                        
                        SessionCollection session = Session.Where(c => c.UserId == user.Id);

                        Database db = Db.For<User>();
                        SqlStringBuilder sql = db.ServiceProvider.Get<SqlStringBuilder>();
                        roles.WriteDelete(sql);
                        accounts.WriteDelete(sql);
                        passwords.WriteDelete(sql);
                        resets.WriteDelete(sql);
                        failures.WriteDelete(sql);
                        lockouts.WriteDelete(sql);
                        logins.WriteDelete(sql);
                        questions.WriteDelete(sql);
                        settings.WriteDelete(sql);
                        session.WriteDelete(sql);

                        sql.Execute(db);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Log.AddEntry("{0}.{1}::{2}", ex, this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex.Message);
            }
            return result;
        }

        public override bool EnablePasswordReset
        {
            get { return true; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return true; }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            UserCollection accounts = User.Where(c => c.Email.Contains(emailToMatch));            
            totalRecords = accounts.Count;
            accounts.PageSize = pageSize;
            int pageNumber = pageIndex + 1;// pageIndex is zero-based GetPage expects 1 based

            MembershipUserCollection results = new MembershipUserCollection();

            if (accounts.PageCount <= pageNumber)
            {
                List<User> accountPage = accounts.GetPage(pageNumber); 
                for (int i = 0; i < accountPage.Count; i++)
                {
                    User current = accountPage[i];
                    if (!current.IsDeleted.Value)
                    {
                        results.Add(User.GetMembershipUser(current));
                    }
                }
            }

            return results;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            UserCollection users = User.Where(c => c.UserName.Contains(usernameToMatch) && c.IsDeleted == false);
            return GetMembershipUsers(pageIndex, pageSize, users, out totalRecords);
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            UserCollection allUsers = User.Where(c => c.IsDeleted != true);
            return GetMembershipUsers(pageIndex, pageSize, allUsers, out totalRecords);
        }
        
        private static MembershipUserCollection GetMembershipUsers(int pageIndex, int pageSize, UserCollection users, out int totalRecords)
        {
            totalRecords = users.Count;
            users.PageSize = pageSize;
            int pageNumber = pageIndex + 1;

            MembershipUserCollection results = new MembershipUserCollection();

            if (users.PageCount <= pageNumber)
            {
                List<User> userPage = users.GetPage(pageNumber);
                for (int i = 0; i < userPage.Count; i++)
                {
                    User current = userPage[i];
                    results.Add(User.GetMembershipUser(current));
                }
            }

            return results;
        }

        public override int GetNumberOfUsersOnline()
        {
            return Session.Where(c => c.IsActive == true).Count;
        }

        public override string GetPassword(string username, string answer)
        {
            User user = User.GetByUserName(username);
            string result = string.Empty;
            if (user != null)
            {
                result = user.GetPassword(answer);//GetPassword(username, answer, user, result);
            }
            else
            {
                Log.AddEntry("Unable to retrieve password for {0}, user not found", username);
            }

            return result;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            User user = User.GetByUserName(username);
            if (user != null)
            {
                if (userIsOnline)
                {
                    Session.Get(username, userIsOnline);
                }

                return user.ToMembershipUser();
            }

            return null;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            Account account = Account.OneWhere(c => c.ProviderUserId == providerUserKey);
            return account.UserOfUserId.ToMembershipUser();
        }

        public override string GetUserNameByEmail(string email)
        {
            User user = User.GetByEmail(email);
            if (user != null)
            {
                return user.UserName;
            }

            return string.Empty;
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return 7; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return 1; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return 8; }
        }

        public override int PasswordAttemptWindow
        {
            get { return 5; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return MembershipPasswordFormat.Hashed; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return @"^.*(?=.{8,})(?=.*\d).*$"  ; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return true; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return true; }
        }

        public override string ResetPassword(string username, string answer)
        {
            User user = User.GetByUserNameOrDie(username);
            string result = user.ResetPassword(answer);//ResetPassword(username, answer, user);

            return result;
        }

        

        public override bool UnlockUser(string userName)
        {
            try
            {
                User user = User.GetByUserNameOrDie(userName);
                user.IsLockedOut = false;
                return true;
            }
            catch (Exception ex)
            {
                Log.AddEntry("{0}.{1}::Unable to unlock user ({0})", ex, userName);
                return false;
            }
        }
        
        public override void UpdateUser(MembershipUser user)
        {
            //MembershipUser result = new MembershipUser(DefaultConfiguration.GetAppSetting("ApplicationName", "UNKOWN"),
            //    user.UserName,
            //    user.Id,
            //    account.Email,
            //    user.PasswordQuestionsByUserId.First().Value,
            //    account.Comment,
            //    user.IsApproved.Value,
            //    lockedOut,
            //    user.CreationDate.Value,
            //    lastLogin,
            //    lastActivity,
            //    lastReset,
            //    lastLockoutDate);

            Account account = Account.Where(c => c.Provider == user.ProviderName && c.ProviderUserId == user.ProviderUserKey).FirstOrDefault();
            if (account != null)
            {
                account.Comment = user.Comment;
                account.UserOfUserId.Email = user.Email;
                account.UserOfUserId.IsApproved = user.IsApproved;               
                account.UserOfUserId.IsLockedOut = user.IsLockedOut;
                account.UserOfUserId.IsOnline = user.IsOnline;
                account.UserOfUserId.PasswordQuestion = user.PasswordQuestion;
                account.UserOfUserId.Save();                
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            return Password.Validate(username, password);
        }
    }
}
