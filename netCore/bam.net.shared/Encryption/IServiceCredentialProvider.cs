using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public interface IServiceCredentialProvider
    {
        string GetUserNameFor(string machineName, string serviceName);
        string GetPasswordFor(string machineName, string serviceName);
    }
}
