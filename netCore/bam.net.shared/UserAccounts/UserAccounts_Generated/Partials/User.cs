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
using Bam.Net.Logging;
using Bam.Net.Encryption;
using Bam.Net.ServiceProxy;

namespace Bam.Net.UserAccounts.Data
{
    public partial class User
    {
        public bool IsOnline
        {
            get
            {
                Session session = Session.Get(UserName, UserDatabase);
                return session.IsActive.Value;
            }
            set
            {
                Session session = Session.Get(UserName, UserDatabase);
                session.IsActive = value;
                session.Save();
            }
        }     

        const string AnonymousUser = "Anonymous";
        static User _anon;
        static object _anonLock = new object();
        public static User Anonymous
        {
            get
            {
                return _anonLock.DoubleCheckLock(ref _anon, () =>
                {
                    User user = OneWhere(c => c.UserName == AnonymousUser, UserDatabase);
                    if (user == null)
                    {
                        user = new User()
                        {
                            UserName = AnonymousUser,
                            CreationDate = DateTime.UtcNow
                        };
                        user.Save(UserDatabase);
                    }

                    return user;
                });
            }
        }

        static Database _userDatabase;
        public static Database UserDatabase
        {
            get
            {
                if(_userDatabase == null)
                {
                    _userDatabase = Db.For<User>(UserAccountsDatabase.Default);
                }

                return _userDatabase;
            }
            set
            {
                _userDatabase = value;
                Db.For<User>(_userDatabase);
            }
        }

        public static User GetCurrent(IHttpContext context)
        {
            User result = null;
            string userName = UserResolvers.Default.GetUser(context);
            if (!string.IsNullOrEmpty(userName))
            {
                result = OneWhere(c => c.UserName == userName, UserDatabase);
            }

            if (result == null)
            {
                result = Anonymous;
            }

            return result;
        }

        public bool IsAuthenticated
        {
            get
            {
                Login login = Login.Where(c => c.UserId == this.Id, Order.By<LoginColumns>(c => c.DateTime, SortOrder.Descending), UserDatabase).FirstOrDefault();
                bool result = false;
                if (login != null)
                {
                    DateTime now = DateTime.UtcNow;
                    TimeSpan fifteenMinutes = TimeSpan.FromMinutes(15);
                    TimeSpan sinceLastLogin = now.Subtract(login.DateTime.Value);
                    result = sinceLastLogin < fifteenMinutes;
                }

                return result;
            }
        }

        public void AddLoginRecord(Database db = null)
        {
            Login.Add(this, db);
        }

        public DateTime LastAcitivtyDate
        {
            get
            {
                Session session = Session.Get(this);
                return session == null ? DateTime.MinValue : session.LastActivity.Value;
            }
            set
            {
                Session session = Session.Get(this);
                session.LastActivity = DateTime.UtcNow;
                session.Save();
            }
        }

        public bool IsLockedOut
        {
            get
            {
                LockOut lockout = LockOutsByUserId.FirstOrDefault();
                if (lockout != null)
                {
                    bool locked = !lockout.Unlocked.Value;
                    return locked;
                }

                return false;
            }
            set
            {
                LockOut lockout = LockOutsByUserId.FirstOrDefault();
                if (lockout != null)
                {
                    bool isLocked = value;
                    lockout.Unlocked = !isLocked;
                    if (!isLocked)
                    {
                        lockout.UnlockedDate = DateTime.UtcNow;
                    }
                    lockout.Save();
                }
            }
        }

        public bool IsFirstLogin()
        {
            LoginCollection logins = Login.Where(c => c.UserId == this.Id);
            return logins.Count == 1;
        }

        public DateTime LastLockoutDate
        {
            get
            {
                LockOut lockout = LockOut.Last(this);
                return lockout == null ? DateTime.MinValue : lockout.DateTime.Value;
            }
            set
            {
                LockOut lockout = LockOut.Last(this);
                lockout.DateTime = value;
                lockout.Save();
            }
        }

        public DateTime LastLoginDate
        {
            get
            {
                Login login = Login.Last(this);
                return login == null ? DateTime.MinValue : login.DateTime.Value;
            }
            set
            {
                Login login = Login.Last(this);
                login.DateTime = value;
                login.Save();
            }
        }

        public DateTime LastPasswordChangedDate
        {
            get
            {
                PasswordReset reset = PasswordReset.Last(this);
                return reset == null ? DateTime.MinValue : reset.DateTime.Value;
            }
            set
            {
                PasswordReset reset = PasswordReset.Last(this);
                reset.DateTime = value;
                reset.Save();
            }
        }
        
        public string PasswordQuestion
        {
            get
            {
                return PasswordQuestionsByUserId.JustOne().Value;
            }
            set
            {
                Bam.Net.UserAccounts.Data.PasswordQuestion q = PasswordQuestionsByUserId.JustOne();
                q.Value = value;
                q.Save();
            }
        }

        /// <summary>
        /// Get the User object for the specified userName or throw 
        /// an UserNameNotFoundException if they are not found
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User GetByUserNameOrDie(string userName, Database db = null)
        {
            User user = GetByUserName(userName, db);
            if (user == null)
            {                
                throw new UserNameNotFoundException(userName);
            }
            return user;
        }
        
        public static bool Exists(string userName, Database db = null)
        {
            User ignore;
            return Exists(userName, out ignore);
        }

        public static bool Exists(string userName, out User user, Database db = null)
        {
            user = GetByUserName(userName, db);
            return user != null;
        }

        /// <summary>
        /// Ensures that the User with the specified userName 
        /// exists in the database
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <returns></returns>
        public static User Ensure(string userName, Database db = null)
        {
            User user = GetByUserName(userName, db);
            if (user == null)
            {
                user = User.Create(userName, db);
            }

            return user;
        }

        /// <summary>
        /// Get the user with the specified userName or null if the user is not found
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static User GetByUserName(string userName, Database db = null)
        {
            return User.OneWhere(c => c.UserName == userName && c.IsDeleted != true, db);
        }

        /// <summary>
        /// Get the user with the specified email address
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static User GetByEmail(string email, Database database = null)
        {
            return User.OneWhere(c => c.Email == email && c.IsDeleted != true, database); 
        }

        public static User Create(string userName, string email, string password, bool isApproved = true, bool createAccount = false, bool accountIsConfirmed = false, bool sendConfirmationEmail = false)
        {
            return Create(userName, email, password, DefaultConfigurationApplicationNameProvider.Instance, isApproved, createAccount, accountIsConfirmed);
        }

        public static User Create(string userName, string email, string password, IApplicationNameProvider appNameResolver, bool isApproved = true, bool createAccount = false, bool accountIsConfirmed = false, Database db = null)
        {
            ThrowIfUserNameInUse(userName, db);
            ThrowIfEmailInUse(email, db);

            User user = Create(userName, email, password, isApproved, db);

            if (createAccount)
            {
                Account.Create(user, appNameResolver.GetApplicationName(), user.Id.Value.ToString(), accountIsConfirmed, db);
            }

            return user;
        }

        public static User Create(string userName, Database db = null)
        {
            User user = new User
            {
                Cuid = NCuid.Cuid.Generate(),
                CreationDate = DateTime.UtcNow,
                UserName = userName,
                IsApproved = true,
                IsDeleted = false
            };
            user.Save(db);
            return user;
        }

        private static User Create(string userName, string email, string password, bool isApproved, Database db = null)
        {
            User user = new User
            {
                Cuid = NCuid.Cuid.Generate(),
                CreationDate = DateTime.UtcNow,
                UserName = userName,
                Email = email,
                IsApproved = isApproved,
                IsDeleted = false
            };
            user.Save(db);
            Password p = Password.Set(userName, password, db);
            return user;
        }

        private static void ThrowIfEmailInUse(string email, Database db = null)
        {
            User byEmail = GetByEmail(email, db);
            if (byEmail != null)
            {
                throw new EmailAlreadyRegisteredException(email);
            }
        }

        private static void ThrowIfUserNameInUse(string userName, Database db = null)
        {
            User byUserName = GetByUserName(userName, db);
            if (byUserName != null)
            {
                throw new UserNameInUseException(userName);
            }
        }


        public bool ValidatePassword(string password, bool updateFailures = true)
        {
            return Password.Validate(UserName, password, updateFailures);
        }

        public void SetPassword(string password)
        {
            Password.Set(UserName, password);
        }

        public bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            Password pass = PasswordsByUserId.FirstOrDefault();
            bool result = false;
            if (pass == null)
            {
                Log.AddEntry("Unable to change password question and answer, user password was not set");
            }
            else
            {
                string decrypted = Aes.Decrypt(pass.Value);
                if (decrypted.Equals(password))
                {
                    PasswordQuestion question = PasswordQuestionsByUserId.FirstOrDefault();
                    if (question == null)
                    {
                        question = PasswordQuestionsByUserId.AddNew();
                    }

                    question.Value = newPasswordQuestion;
                    question.Answer = newPasswordAnswer;
                    question.Save();

                    result = true;
                }
            }
            return result;
        }

        public string GetPassword()
        {
            return Password.Get(this);
        }


        public string GetPassword(string answer)
        {
            PasswordQuestion question = PasswordQuestionsByUserId.FirstOrDefault();
            string result = string.Empty;
            if (question == null)
            {
                Log.AddEntry("Unable to retrieve password for {0}, password question not set", UserName);
            }

            if (question.Answer.Equals(answer))
            {
                Password password = PasswordsByUserId.FirstOrDefault();
                if (password == null)
                {
                    Log.AddEntry("Unable to retrieve password for {0}, password not set", UserName);
                }

                result = Aes.Decrypt(password.Value);
            }
            else
            {
                Log.AddEntry("Unable to retrieve password for {0}, invalid password ", UserName);
            }
            return result;
        }

        public string ResetPassword(string answer)
        {
            string result = string.Empty;

            PasswordQuestion question = PasswordQuestionsByUserId.FirstOrDefault();
            if (question == null)
            {
                Log.AddEntry("Unable to retrieve password for {0}, password question not set", UserName);
            }

            if (question.Answer.Equals(answer))
            {
                string newPassword = ResetPassword();
            }
            else
            {
                Log.AddEntry("Unable to retrieve password for {0}, invalid password ", UserName);
            }
            return result;
        }

        public string ResetPassword()
        {
            string newPassword = "_".RandomLetters(7);
            Password.Set(UserName, newPassword);
            return newPassword;
        }
    }
}
