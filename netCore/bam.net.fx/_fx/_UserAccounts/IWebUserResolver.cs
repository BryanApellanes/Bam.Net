using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bam.Net.UserAccounts
{
    public interface IWebUserResolver
    {
        string GetUser(HttpContext context);
    }
}
