/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using Bam.Net.Configuration;
using Bam.Net.Encryption;
using Bam.Net.CommandLine;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Javascript;
using Bam.Net.Server;
using Bam.Net.ServiceProxy;
using Bam.Net.ServiceProxy.Secure;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Engines;
using FakeItEasy;
using FakeItEasy.Creation;
using Bam.Net.Testing.Unit;

namespace Bam.Net.ServiceProxy.Tests
{
    public partial class ServiceProxyTestContainer
    {
        class TestUserResolver: IUserResolver
        {
            [Exclude]
            public object Clone()
            {
                TestUserResolver clone = new TestUserResolver();
                clone.CopyProperties(this);
                return clone;
            }

            public string GetCurrentUser()
            {
                return "TestUser";
            }

            public string GetUser(IHttpContext context)
            {
                return "TestUser";
            }

            public IHttpContext HttpContext
            {
                get;
                set;
            }
        }

        [UnitTest]
        public void UserResolver_ShouldResolveToTheSameAsUserUtil()
        {
			UserResolvers.Default.Clear();
			UserResolvers.Default.AddResolver(new DefaultWebUserResolver());

            string user = UserUtil.GetCurrentWebUserName();
            string resolved = UserResolvers.Default.GetCurrentUser();

            Expect.AreEqual(user, resolved);
        }

        [UnitTest]
        public void UserResolver_ShouldResolveToTestUser()
        {
            UserResolvers.Default.Clear();
            UserResolvers.Default.AddResolver(new TestUserResolver());
            string userName = UserResolvers.Default.GetUser(A.Fake<IHttpContext>());
            Expect.AreEqual("TestUser", userName);
        }
    }
}
