using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ApplicationNameNotSpecifiedException: Exception
    {
        public ApplicationNameNotSpecifiedException() : base($"ApplicationName was not specified, add the header '{Bam.Net.Web.Headers.ApplicationName}' with your registered application name as the value")
        { }
    }
}
