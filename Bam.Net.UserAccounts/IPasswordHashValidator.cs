using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public interface IPasswordHashValidator
    {
        bool IsPasswordHashValid(string userName, string passwordHash);
    }
}
