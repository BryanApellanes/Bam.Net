/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.SourceControl
{
    public class UnableToConfigureGitException: Exception
    {
        public UnableToConfigureGitException(string message) : base(message) { }
    }
}
