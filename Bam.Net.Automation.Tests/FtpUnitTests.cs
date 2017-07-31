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

        [IntegrationTest]
        public void ShouldUpload()
        {
            string testPath = "C:\\inetpub\\ftproot\\subfolder";
            if (Directory.Exists(testPath))
            {
                Directory.Delete(testPath, true);
            };

            Expect.IsFalse(Directory.Exists(testPath));
            Ftp ftp = Ftp.Server("localhost");
            ftp.UserName("ftptest").Password("53cr3tP455w0rd1!").Upload(".\\Test");
            Expect.IsTrue(Directory.Exists(testPath));
        }

        [UnitTest]
        public void SettingUserNameAndPasswordShouldSetNetworkCredential()
        {
            Ftp ftp = Ftp.Server("localhost");
            Expect.IsNull(ftp.Config.Credentials);

            ftp.UserName("ftptest").Password("53cr3tP455w0rd1!");

            Expect.IsNotNull(ftp.Config.Credentials);
        }
    }
}
