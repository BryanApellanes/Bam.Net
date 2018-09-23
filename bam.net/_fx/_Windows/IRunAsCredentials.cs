/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Windows
{
    public interface IRunAsCredentials
    {
        string DomainAndUserName { get; }
        string Domain { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
    }
}
