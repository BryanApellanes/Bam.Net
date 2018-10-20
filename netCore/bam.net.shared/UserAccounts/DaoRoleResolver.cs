using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts.Data;

namespace Bam.Net.UserAccounts
{
    public class DaoRoleResolver : IRoleResolver
    {
        public IHttpContext HttpContext
        {
            get; set;
        }
        [Exclude]
        public object Clone()
        {
            DaoRoleResolver clone = new DaoRoleResolver();
            clone.CopyProperties(this);
            return clone;
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

        public string[] GetRoles(IUserResolver userResolver)
        {
            User user = User.OneWhere(c => c.UserName == userResolver.GetCurrentUser(), Database);
            List<string> results = new List<string>();
            if(user != null)
            {
                results.AddRange(user.Roles.Select(r => r.Name));
            }

            return results.ToArray();
        }

        public bool IsInRole(IUserResolver userResolver, string roleName)
        {
            return new List<string>(GetRoles(userResolver)).Contains(roleName);
        }
    }
}
