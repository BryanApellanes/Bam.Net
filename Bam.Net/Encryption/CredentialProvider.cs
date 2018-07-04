using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Encryption
{
    public abstract class CredentialProvider : ICredentialProvider, INamedCredentialProvider, IServiceCredentialProvider
    {
        static CredentialProvider _default;
        static object _defaultLock = new object();
        public static CredentialProvider Default
        {
            get
            {
                return _defaultLock.DoubleCheckLock(ref _default, () => DefaultConfigurationCredentialProvider.Instance);
            }
            set
            {
                _default = value;
            }
        }

        public abstract string GetPassword();

        public abstract string GetUserName();

        public abstract string GetUserNameFor(string targetIdentifier);

        public abstract string GetPasswordFor(string targetIdentifier);

        public abstract string GetUserNameFor(string machineName, string serviceName);

        public abstract string GetPasswordFor(string machineName, string serviceName);
    }
}
