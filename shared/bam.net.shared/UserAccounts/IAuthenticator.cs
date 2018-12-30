using Bam.Net.UserAccounts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public interface IAuthenticator
    {
        bool IsPasswordValid(string userName, string password);     
    }
}

