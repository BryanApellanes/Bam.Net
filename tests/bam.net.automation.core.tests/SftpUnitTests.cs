using Bam.Net.CommandLine;
using Bam.Net.Testing;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;
using System;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class SftpUnitTests : CommandLineTool
    {
        [UnitTest]
        public void ShouldSetSshServer()
        {
            Sftp ssh = Sftp.Server("localhost");
            Expect.AreEqual(ssh.Config.ServerHost, "localhost");
        }

        [UnitTest]
        public void ShouldSetSshUserName()
        {
            string un = "userTest";
            Sftp ssh = Sftp.Server("localhost").UserName(un);
            Expect.AreEqual(ssh.Config.UserName, un, "UserName was not set properly");
        }

        [UnitTest]
        public void ShouldSetPassword()
        {
            string p = "password";
            Sftp ssh = Sftp.Server("localhost").Password(p);
            Expect.AreEqual(ssh.Config.Password, p, "Password was not set properly");
        }

        [ConsoleAction]
        [IntegrationTest]
        public void ShouldUpload()
        {
            string testPath = "/bam/data/tmp";
            
            Sftp ssh = Sftp
                .Server("192.168.0.81")
                .Port(22)
                .UserName("bryan")
                .Password(GetPassword())
                .Upload(testPath);
        }

        private string GetPassword()
        {
            // TODO: implement this securely
            return string.Empty;
        }
    }
}
