using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;
using Bam.Net.ServiceProxy;
using Bam.Net.Testing;
using Bam.Net.UserAccounts;
using NSubstitute;

namespace Bam.Net.CoreServices.Tests
{
    using System.IO;
    using Bam.Net.CoreServices.ApplicationRegistration;
    using Net.Data.SQLite;
    using Server;
    using ServiceProxySecure = ServiceProxy.Secure;

    [Serializable]
    public class ApplicationRegistryTests : CommandLineTestInterface
    {
        //[UnitTest("AppRegistry: ")]
    }
}
