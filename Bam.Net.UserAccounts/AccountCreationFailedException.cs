/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.UserAccounts
{
    public class AccountCreationFailedException: Exception
    {
        public AccountCreationFailedException(string userName)
            : base("The account was not found for ({0})"._Format(userName))
        {

        }
    }
}
