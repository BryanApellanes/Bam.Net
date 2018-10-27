/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class FtpUnitTests : CommandLineTestInterface
    {
        [UnitTest]
        public void ShouldSetFtpServer()
        {
            Ftp ftp = Ftp.Server("localhost");
            Expect.AreEqual(ftp.Config.ServerHost, "localhost");
        }

        [UnitTest]
        public void ShouldSetFtpUserName()
        {
            string un = "userTest";
            Ftp ftp = Ftp.Server("localhost").UserName(un);
            Expect.AreEqual(ftp.Config.UserName, un, "UserName was not set properly");
        }

        [UnitTest]
        public void ShouldSetPassword()
        {
            string p = "password";
            Ftp ftp = Ftp.Server("localhost").Password(p);
            Expect.AreEqual(ftp.Config.Password, p, "Password was not set properly");
        }

        [UnitTest]
        public void SettingUserNameAndPasswordShouldSetNetworkCredential()
        {
            Ftp ftp = Ftp.Server("localhost");
            Expect.IsNull(ftp.Config.Credentials);

            ftp.UserName("ftptest").Password(16.RandomLetters());

            Expect.IsNotNull(ftp.Config.Credentials);
        }
    }
}
