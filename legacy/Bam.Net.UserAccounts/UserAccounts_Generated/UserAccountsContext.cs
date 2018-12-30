/*
	This file was generated and should not be modified directly
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.UserAccounts.Data
{
	// schema = UserAccounts 
    public static class UserAccountsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "UserAccounts";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}


	public class UserQueryContext
	{
			public UserCollection Where(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.Where(where, db);
			}
		   
			public UserCollection Where(WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.Where(where, orderBy, db);
			}

			public User OneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.OneWhere(where, db);
			}

			public static User GetOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.GetOneWhere(where, db);
			}
		
			public User FirstOneWhere(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.FirstOneWhere(where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.Top(count, where, db);
			}

			public UserCollection Top(int count, WhereDelegate<UserColumns> where, OrderBy<UserColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.User.Count(where, db);
			}
	}

	static UserQueryContext _users;
	static object _usersLock = new object();
	public static UserQueryContext Users
	{
		get
		{
			return _usersLock.DoubleCheckLock<UserQueryContext>(ref _users, () => new UserQueryContext());
		}
	}
	public class AccountQueryContext
	{
			public AccountCollection Where(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.Where(where, db);
			}
		   
			public AccountCollection Where(WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.Where(where, orderBy, db);
			}

			public Account OneWhere(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.OneWhere(where, db);
			}

			public static Account GetOneWhere(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.GetOneWhere(where, db);
			}
		
			public Account FirstOneWhere(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.FirstOneWhere(where, db);
			}

			public AccountCollection Top(int count, WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.Top(count, where, db);
			}

			public AccountCollection Top(int count, WhereDelegate<AccountColumns> where, OrderBy<AccountColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<AccountColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Account.Count(where, db);
			}
	}

	static AccountQueryContext _accounts;
	static object _accountsLock = new object();
	public static AccountQueryContext Accounts
	{
		get
		{
			return _accountsLock.DoubleCheckLock<AccountQueryContext>(ref _accounts, () => new AccountQueryContext());
		}
	}
	public class PasswordQueryContext
	{
			public PasswordCollection Where(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.Where(where, db);
			}
		   
			public PasswordCollection Where(WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.Where(where, orderBy, db);
			}

			public Password OneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.OneWhere(where, db);
			}

			public static Password GetOneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.GetOneWhere(where, db);
			}
		
			public Password FirstOneWhere(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.FirstOneWhere(where, db);
			}

			public PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.Top(count, where, db);
			}

			public PasswordCollection Top(int count, WhereDelegate<PasswordColumns> where, OrderBy<PasswordColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Password.Count(where, db);
			}
	}

	static PasswordQueryContext _passwords;
	static object _passwordsLock = new object();
	public static PasswordQueryContext Passwords
	{
		get
		{
			return _passwordsLock.DoubleCheckLock<PasswordQueryContext>(ref _passwords, () => new PasswordQueryContext());
		}
	}
	public class PasswordResetQueryContext
	{
			public PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.Where(where, db);
			}
		   
			public PasswordResetCollection Where(WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.Where(where, orderBy, db);
			}

			public PasswordReset OneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.OneWhere(where, db);
			}

			public static PasswordReset GetOneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.GetOneWhere(where, db);
			}
		
			public PasswordReset FirstOneWhere(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.FirstOneWhere(where, db);
			}

			public PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.Top(count, where, db);
			}

			public PasswordResetCollection Top(int count, WhereDelegate<PasswordResetColumns> where, OrderBy<PasswordResetColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordResetColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordReset.Count(where, db);
			}
	}

	static PasswordResetQueryContext _passwordResets;
	static object _passwordResetsLock = new object();
	public static PasswordResetQueryContext PasswordResets
	{
		get
		{
			return _passwordResetsLock.DoubleCheckLock<PasswordResetQueryContext>(ref _passwordResets, () => new PasswordResetQueryContext());
		}
	}
	public class PasswordFailureQueryContext
	{
			public PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.Where(where, db);
			}
		   
			public PasswordFailureCollection Where(WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.Where(where, orderBy, db);
			}

			public PasswordFailure OneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.OneWhere(where, db);
			}

			public static PasswordFailure GetOneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.GetOneWhere(where, db);
			}
		
			public PasswordFailure FirstOneWhere(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.FirstOneWhere(where, db);
			}

			public PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.Top(count, where, db);
			}

			public PasswordFailureCollection Top(int count, WhereDelegate<PasswordFailureColumns> where, OrderBy<PasswordFailureColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordFailureColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordFailure.Count(where, db);
			}
	}

	static PasswordFailureQueryContext _passwordFailures;
	static object _passwordFailuresLock = new object();
	public static PasswordFailureQueryContext PasswordFailures
	{
		get
		{
			return _passwordFailuresLock.DoubleCheckLock<PasswordFailureQueryContext>(ref _passwordFailures, () => new PasswordFailureQueryContext());
		}
	}
	public class LockOutQueryContext
	{
			public LockOutCollection Where(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.Where(where, db);
			}
		   
			public LockOutCollection Where(WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.Where(where, orderBy, db);
			}

			public LockOut OneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.OneWhere(where, db);
			}

			public static LockOut GetOneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.GetOneWhere(where, db);
			}
		
			public LockOut FirstOneWhere(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.FirstOneWhere(where, db);
			}

			public LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.Top(count, where, db);
			}

			public LockOutCollection Top(int count, WhereDelegate<LockOutColumns> where, OrderBy<LockOutColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LockOutColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.LockOut.Count(where, db);
			}
	}

	static LockOutQueryContext _lockOuts;
	static object _lockOutsLock = new object();
	public static LockOutQueryContext LockOuts
	{
		get
		{
			return _lockOutsLock.DoubleCheckLock<LockOutQueryContext>(ref _lockOuts, () => new LockOutQueryContext());
		}
	}
	public class LoginQueryContext
	{
			public LoginCollection Where(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.Where(where, db);
			}
		   
			public LoginCollection Where(WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.Where(where, orderBy, db);
			}

			public Login OneWhere(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.OneWhere(where, db);
			}

			public static Login GetOneWhere(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.GetOneWhere(where, db);
			}
		
			public Login FirstOneWhere(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.FirstOneWhere(where, db);
			}

			public LoginCollection Top(int count, WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.Top(count, where, db);
			}

			public LoginCollection Top(int count, WhereDelegate<LoginColumns> where, OrderBy<LoginColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<LoginColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Login.Count(where, db);
			}
	}

	static LoginQueryContext _logins;
	static object _loginsLock = new object();
	public static LoginQueryContext Logins
	{
		get
		{
			return _loginsLock.DoubleCheckLock<LoginQueryContext>(ref _logins, () => new LoginQueryContext());
		}
	}
	public class PasswordQuestionQueryContext
	{
			public PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.Where(where, db);
			}
		   
			public PasswordQuestionCollection Where(WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.Where(where, orderBy, db);
			}

			public PasswordQuestion OneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.OneWhere(where, db);
			}

			public static PasswordQuestion GetOneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.GetOneWhere(where, db);
			}
		
			public PasswordQuestion FirstOneWhere(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.FirstOneWhere(where, db);
			}

			public PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.Top(count, where, db);
			}

			public PasswordQuestionCollection Top(int count, WhereDelegate<PasswordQuestionColumns> where, OrderBy<PasswordQuestionColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PasswordQuestionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.PasswordQuestion.Count(where, db);
			}
	}

	static PasswordQuestionQueryContext _passwordQuestions;
	static object _passwordQuestionsLock = new object();
	public static PasswordQuestionQueryContext PasswordQuestions
	{
		get
		{
			return _passwordQuestionsLock.DoubleCheckLock<PasswordQuestionQueryContext>(ref _passwordQuestions, () => new PasswordQuestionQueryContext());
		}
	}
	public class SettingQueryContext
	{
			public SettingCollection Where(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.Where(where, db);
			}
		   
			public SettingCollection Where(WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.Where(where, orderBy, db);
			}

			public Setting OneWhere(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.OneWhere(where, db);
			}

			public static Setting GetOneWhere(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.GetOneWhere(where, db);
			}
		
			public Setting FirstOneWhere(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.FirstOneWhere(where, db);
			}

			public SettingCollection Top(int count, WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.Top(count, where, db);
			}

			public SettingCollection Top(int count, WhereDelegate<SettingColumns> where, OrderBy<SettingColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SettingColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Setting.Count(where, db);
			}
	}

	static SettingQueryContext _settings;
	static object _settingsLock = new object();
	public static SettingQueryContext Settings
	{
		get
		{
			return _settingsLock.DoubleCheckLock<SettingQueryContext>(ref _settings, () => new SettingQueryContext());
		}
	}
	public class SessionQueryContext
	{
			public SessionCollection Where(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.Where(where, db);
			}
		   
			public SessionCollection Where(WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.Where(where, orderBy, db);
			}

			public Session OneWhere(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.OneWhere(where, db);
			}

			public static Session GetOneWhere(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.GetOneWhere(where, db);
			}
		
			public Session FirstOneWhere(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.FirstOneWhere(where, db);
			}

			public SessionCollection Top(int count, WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.Top(count, where, db);
			}

			public SessionCollection Top(int count, WhereDelegate<SessionColumns> where, OrderBy<SessionColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SessionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Session.Count(where, db);
			}
	}

	static SessionQueryContext _sessions;
	static object _sessionsLock = new object();
	public static SessionQueryContext Sessions
	{
		get
		{
			return _sessionsLock.DoubleCheckLock<SessionQueryContext>(ref _sessions, () => new SessionQueryContext());
		}
	}
	public class SessionStateQueryContext
	{
			public SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.Where(where, db);
			}
		   
			public SessionStateCollection Where(WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.Where(where, orderBy, db);
			}

			public SessionState OneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.OneWhere(where, db);
			}

			public static SessionState GetOneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.GetOneWhere(where, db);
			}
		
			public SessionState FirstOneWhere(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.FirstOneWhere(where, db);
			}

			public SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.Top(count, where, db);
			}

			public SessionStateCollection Top(int count, WhereDelegate<SessionStateColumns> where, OrderBy<SessionStateColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SessionStateColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.SessionState.Count(where, db);
			}
	}

	static SessionStateQueryContext _sessionStates;
	static object _sessionStatesLock = new object();
	public static SessionStateQueryContext SessionStates
	{
		get
		{
			return _sessionStatesLock.DoubleCheckLock<SessionStateQueryContext>(ref _sessionStates, () => new SessionStateQueryContext());
		}
	}
	public class UserBehaviorQueryContext
	{
			public UserBehaviorCollection Where(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.Where(where, db);
			}
		   
			public UserBehaviorCollection Where(WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.Where(where, orderBy, db);
			}

			public UserBehavior OneWhere(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.OneWhere(where, db);
			}

			public static UserBehavior GetOneWhere(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.GetOneWhere(where, db);
			}
		
			public UserBehavior FirstOneWhere(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.FirstOneWhere(where, db);
			}

			public UserBehaviorCollection Top(int count, WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.Top(count, where, db);
			}

			public UserBehaviorCollection Top(int count, WhereDelegate<UserBehaviorColumns> where, OrderBy<UserBehaviorColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserBehaviorColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserBehavior.Count(where, db);
			}
	}

	static UserBehaviorQueryContext _userBehaviors;
	static object _userBehaviorsLock = new object();
	public static UserBehaviorQueryContext UserBehaviors
	{
		get
		{
			return _userBehaviorsLock.DoubleCheckLock<UserBehaviorQueryContext>(ref _userBehaviors, () => new UserBehaviorQueryContext());
		}
	}
	public class RoleQueryContext
	{
			public RoleCollection Where(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.Where(where, db);
			}
		   
			public RoleCollection Where(WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.Where(where, orderBy, db);
			}

			public Role OneWhere(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.OneWhere(where, db);
			}

			public static Role GetOneWhere(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.GetOneWhere(where, db);
			}
		
			public Role FirstOneWhere(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.FirstOneWhere(where, db);
			}

			public RoleCollection Top(int count, WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.Top(count, where, db);
			}

			public RoleCollection Top(int count, WhereDelegate<RoleColumns> where, OrderBy<RoleColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<RoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Role.Count(where, db);
			}
	}

	static RoleQueryContext _roles;
	static object _rolesLock = new object();
	public static RoleQueryContext Roles
	{
		get
		{
			return _rolesLock.DoubleCheckLock<RoleQueryContext>(ref _roles, () => new RoleQueryContext());
		}
	}
	public class TreeNodeQueryContext
	{
			public TreeNodeCollection Where(WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.Where(where, db);
			}
		   
			public TreeNodeCollection Where(WhereDelegate<TreeNodeColumns> where, OrderBy<TreeNodeColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.Where(where, orderBy, db);
			}

			public TreeNode OneWhere(WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.OneWhere(where, db);
			}

			public static TreeNode GetOneWhere(WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.GetOneWhere(where, db);
			}
		
			public TreeNode FirstOneWhere(WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.FirstOneWhere(where, db);
			}

			public TreeNodeCollection Top(int count, WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.Top(count, where, db);
			}

			public TreeNodeCollection Top(int count, WhereDelegate<TreeNodeColumns> where, OrderBy<TreeNodeColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<TreeNodeColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.TreeNode.Count(where, db);
			}
	}

	static TreeNodeQueryContext _treeNodes;
	static object _treeNodesLock = new object();
	public static TreeNodeQueryContext TreeNodes
	{
		get
		{
			return _treeNodesLock.DoubleCheckLock<TreeNodeQueryContext>(ref _treeNodes, () => new TreeNodeQueryContext());
		}
	}
	public class PermissionQueryContext
	{
			public PermissionCollection Where(WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.Where(where, db);
			}
		   
			public PermissionCollection Where(WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.Where(where, orderBy, db);
			}

			public Permission OneWhere(WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.OneWhere(where, db);
			}

			public static Permission GetOneWhere(WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.GetOneWhere(where, db);
			}
		
			public Permission FirstOneWhere(WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.FirstOneWhere(where, db);
			}

			public PermissionCollection Top(int count, WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.Top(count, where, db);
			}

			public PermissionCollection Top(int count, WhereDelegate<PermissionColumns> where, OrderBy<PermissionColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<PermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Permission.Count(where, db);
			}
	}

	static PermissionQueryContext _permissions;
	static object _permissionsLock = new object();
	public static PermissionQueryContext Permissions
	{
		get
		{
			return _permissionsLock.DoubleCheckLock<PermissionQueryContext>(ref _permissions, () => new PermissionQueryContext());
		}
	}
	public class GroupQueryContext
	{
			public GroupCollection Where(WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.Where(where, db);
			}
		   
			public GroupCollection Where(WhereDelegate<GroupColumns> where, OrderBy<GroupColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.Where(where, orderBy, db);
			}

			public Group OneWhere(WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.OneWhere(where, db);
			}

			public static Group GetOneWhere(WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.GetOneWhere(where, db);
			}
		
			public Group FirstOneWhere(WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.FirstOneWhere(where, db);
			}

			public GroupCollection Top(int count, WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.Top(count, where, db);
			}

			public GroupCollection Top(int count, WhereDelegate<GroupColumns> where, OrderBy<GroupColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<GroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.Group.Count(where, db);
			}
	}

	static GroupQueryContext _groups;
	static object _groupsLock = new object();
	public static GroupQueryContext Groups
	{
		get
		{
			return _groupsLock.DoubleCheckLock<GroupQueryContext>(ref _groups, () => new GroupQueryContext());
		}
	}
	public class UserRoleQueryContext
	{
			public UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.Where(where, db);
			}
		   
			public UserRoleCollection Where(WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.Where(where, orderBy, db);
			}

			public UserRole OneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.OneWhere(where, db);
			}

			public static UserRole GetOneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.GetOneWhere(where, db);
			}
		
			public UserRole FirstOneWhere(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.FirstOneWhere(where, db);
			}

			public UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.Top(count, where, db);
			}

			public UserRoleCollection Top(int count, WhereDelegate<UserRoleColumns> where, OrderBy<UserRoleColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserRoleColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserRole.Count(where, db);
			}
	}

	static UserRoleQueryContext _userRoles;
	static object _userRolesLock = new object();
	public static UserRoleQueryContext UserRoles
	{
		get
		{
			return _userRolesLock.DoubleCheckLock<UserRoleQueryContext>(ref _userRoles, () => new UserRoleQueryContext());
		}
	}
	public class UserGroupQueryContext
	{
			public UserGroupCollection Where(WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.Where(where, db);
			}
		   
			public UserGroupCollection Where(WhereDelegate<UserGroupColumns> where, OrderBy<UserGroupColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.Where(where, orderBy, db);
			}

			public UserGroup OneWhere(WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.OneWhere(where, db);
			}

			public static UserGroup GetOneWhere(WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.GetOneWhere(where, db);
			}
		
			public UserGroup FirstOneWhere(WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.FirstOneWhere(where, db);
			}

			public UserGroupCollection Top(int count, WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.Top(count, where, db);
			}

			public UserGroupCollection Top(int count, WhereDelegate<UserGroupColumns> where, OrderBy<UserGroupColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserGroupColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserGroup.Count(where, db);
			}
	}

	static UserGroupQueryContext _userGroups;
	static object _userGroupsLock = new object();
	public static UserGroupQueryContext UserGroups
	{
		get
		{
			return _userGroupsLock.DoubleCheckLock<UserGroupQueryContext>(ref _userGroups, () => new UserGroupQueryContext());
		}
	}
	public class GroupPermissionQueryContext
	{
			public GroupPermissionCollection Where(WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.Where(where, db);
			}
		   
			public GroupPermissionCollection Where(WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.Where(where, orderBy, db);
			}

			public GroupPermission OneWhere(WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.OneWhere(where, db);
			}

			public static GroupPermission GetOneWhere(WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.GetOneWhere(where, db);
			}
		
			public GroupPermission FirstOneWhere(WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.FirstOneWhere(where, db);
			}

			public GroupPermissionCollection Top(int count, WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.Top(count, where, db);
			}

			public GroupPermissionCollection Top(int count, WhereDelegate<GroupPermissionColumns> where, OrderBy<GroupPermissionColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<GroupPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.GroupPermission.Count(where, db);
			}
	}

	static GroupPermissionQueryContext _groupPermissions;
	static object _groupPermissionsLock = new object();
	public static GroupPermissionQueryContext GroupPermissions
	{
		get
		{
			return _groupPermissionsLock.DoubleCheckLock<GroupPermissionQueryContext>(ref _groupPermissions, () => new GroupPermissionQueryContext());
		}
	}
	public class UserPermissionQueryContext
	{
			public UserPermissionCollection Where(WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.Where(where, db);
			}
		   
			public UserPermissionCollection Where(WhereDelegate<UserPermissionColumns> where, OrderBy<UserPermissionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.Where(where, orderBy, db);
			}

			public UserPermission OneWhere(WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.OneWhere(where, db);
			}

			public static UserPermission GetOneWhere(WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.GetOneWhere(where, db);
			}
		
			public UserPermission FirstOneWhere(WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.FirstOneWhere(where, db);
			}

			public UserPermissionCollection Top(int count, WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.Top(count, where, db);
			}

			public UserPermissionCollection Top(int count, WhereDelegate<UserPermissionColumns> where, OrderBy<UserPermissionColumns> orderBy, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<UserPermissionColumns> where, Database db = null)
			{
				return Bam.Net.UserAccounts.Data.UserPermission.Count(where, db);
			}
	}

	static UserPermissionQueryContext _userPermissions;
	static object _userPermissionsLock = new object();
	public static UserPermissionQueryContext UserPermissions
	{
		get
		{
			return _userPermissionsLock.DoubleCheckLock<UserPermissionQueryContext>(ref _userPermissions, () => new UserPermissionQueryContext());
		}
	}    }
}																								
