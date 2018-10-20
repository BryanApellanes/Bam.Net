using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Bam.Net.UserAccounts.Data
{
    public partial class User
    {
        public MembershipUser ToMembershipUser()
        {
            return User.GetMembershipUser(this);
        }

        public static MembershipUser GetMembershipUser(User user)
        {
            Args.ThrowIfNull(user, "user");

            DaoMembershipUser result = new DaoMembershipUser(
                "EasyMembershipProvider",
                user.PasswordQuestion,
                user.AccountsByUserId.First().Comment,
                user);

            return result;
        }
    }
}
