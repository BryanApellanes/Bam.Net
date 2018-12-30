using System;
using Bam.Net;
using Bam.Net.CommandLine;
using Bam.Net.Server;
using Bam.Net.Testing;
using Bam.Net.Services.Clients;
using Bam.Net.Logging;
using Bam.Net.UserAccounts;
using Bam.Net.Automation;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Bam.Net.CoreServices;
using Bam.Net.UserAccounts.Data;
using Bam.Net.Data;
using System.Linq;
using Bam.Net.Messaging;
using Bam.Net.Testing.Integration;

namespace Bam.Net.Application
{
    [Serializable]
    public class RemoteRegistrationActions: CommandLineTestInterface
    {
        static CoreClient _client;
        static UserInfo _userInfo;
        static RemoteRegistrationActions()
        {
            _client = new CoreClient(ServiceTools.GetLogger());
        }

        /// <summary>
        /// Signs up.
        /// </summary>
        [ConsoleAction("signUp", "Sign Up for an account on bamapps.net")]
        public void SignUp()
        {
            UserInfo info = ServiceTools.GetUserInfo();            
            SignUpResponse response = _client.SignUp(info.Email, info.Password);
            if (!response.Success)
            {
                OutLine(response.Message, ConsoleColor.Magenta);
            }
            else
            {
                OutLineFormat("{0} signed up successfully", info.Email);
            }
        }

        [ConsoleAction("registerApplication", "Register a new application name on bamapps.net")]
        public void RegisterApplication()
        {
            string applicationName = GetArgument("registerApplication");
            if (string.IsNullOrEmpty(applicationName))
            {
                OutLineFormat("Please specify the name of the application to register: /registerApplication:{applicationName}");
                return;
            }
            
        }

        private void Login()
        {
            UserInfo userInfo = ServiceTools.GetUserInfo();
            LoginResponse loginResponse = _client.Login(userInfo.Email, userInfo.Password.Sha1());
            if (!loginResponse.Success)
            {
                OutLineFormat("Log in failed: {0}", ConsoleColor.Yellow, loginResponse.Message);
                return;
            }
        }
    }
}
