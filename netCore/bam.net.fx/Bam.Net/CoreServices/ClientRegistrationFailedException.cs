using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    public class ClientRegistrationFailedException: Exception
    {
        public ClientRegistrationFailedException(CoreServiceResponse response) : base(response?.Message ?? "No response") { }
    }
}
