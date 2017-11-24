/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using Bam.Net.CommandLine;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Testing;
using Bam.Net.Encryption;
using Bam.Net.Automation;
using Bam.Net.Automation.Nuget;
using System.Collections.ObjectModel;
using Bam.Net.Automation.ContinuousIntegration;
using Bam.Net.ServiceProxy;
using Bam.Net.Logging;
using Microsoft.Build.BuildEngine;
using Microsoft.Build;
using Microsoft.Build.Framework;
using Microsoft.Build.Evaluation;
using Microsoft.Build.Execution;
using Microsoft.Build.Logging;
using Bam.Net.Automation.ContinuousIntegration.Loggers;
using Bam.Net.Documentation;
using Bam.Net.Testing.Integration;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Automation.Tests
{
    [Serializable]
    public class SshUnitTests : CommandLineTestInterface
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
            string testPath = "C:\\bam\\data\\tmp";
            
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
