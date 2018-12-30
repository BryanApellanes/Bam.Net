using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public interface ICredentialProvider
    {
        string GetUserName();
        string GetPassword();
    }
}
