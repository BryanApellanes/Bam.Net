using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public interface INamedCredentialProvider
    {
        string GetUserNameFor(string targetIdentifier);
        string GetPasswordFor(string targetIdentifier);
    }
}
