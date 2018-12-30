/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ServiceProxy;
using Bam.Net.Data;
using Bam.Net.UserAccounts;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class DaoUserResolver : IUserResolver, IUserProvider
    {
        [Exclude]
        public object Clone()
        {
            DaoUserResolver clone = new DaoUserResolver();
            clone.CopyProperties(this);
            return clone;
        }

        static DaoUserResolver _instance;
        static object _instanceLock = new object();
        public static DaoUserResolver Instance
        {
            get
            {
                return _instanceLock.DoubleCheckLock(ref _instance, () => new DaoUserResolver());
            }
        }

        Database _database;
        public Database Database
        {
            get
            {
                if(_database == null)
                {
                    _database = Db.For<User>();
                }
                return _database;
            }
            set
            {
                _database = value;
                Db.For<User>(_database);
            }
        }

        public string GetCurrentUser()
        {
            Session session = Session.Get(HttpContext, Database);
            return session.UserOfUserId.UserName;
        }

        public string GetUser(IHttpContext context)
        {
            Session session = Session.Get(context, Database);
            return session.UserOfUserId?.UserName;
        }

        public void SetUser(IHttpContext context, string userName, bool isAuthenticated)
        {
            User user = User.GetByUserNameOrDie(userName);
            SetUser(context, user, isAuthenticated);
        }

        public void SetUser(IHttpContext context, User user, bool isAuthenticated, Database db = null)
        {
            Session session = Session.Get(context, Database);
            session.UserId = user.Id;
            session.Save(db);

            context.User = new DaoPrincipal(user, isAuthenticated);
        }

        public IHttpContext HttpContext
        {
            get;
            set;
        }
    }
}
