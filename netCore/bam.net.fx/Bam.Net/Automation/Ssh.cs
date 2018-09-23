/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Renci.SshNet;

namespace Bam.Net.Automation
{
    /// <summary>
    /// Fluent wrapper around System.Net.SshClient
    /// </summary>
    public class Ssh
    {
        internal protected class SshConfig
        {
            public SshConfig()
            {
            }

            public string UserName { get; set; }
            public string Password { get; set; }
            public string ServerHost { get; set; }
            public int Port { get; set; }
            public string PublicKeyPath { get; set; }
            public ConnectionInfo ToConnectionInfo()
            {
                if (!string.IsNullOrEmpty(PublicKeyPath))
                {
                    PrivateKeyFile privateKeyFile = new PrivateKeyFile(PublicKeyPath);
                    PrivateKeyAuthenticationMethod privateKeyAuthenticationMethod = new PrivateKeyAuthenticationMethod(UserName, privateKeyFile);
                    return new ConnectionInfo(ServerHost, Port, UserName, privateKeyAuthenticationMethod);
                }
                if (!string.IsNullOrEmpty(Password))
                {
                    PasswordAuthenticationMethod passwordAuthenticationMethod = new PasswordAuthenticationMethod(UserName, Password);
                    return new ConnectionInfo(ServerHost, Port, UserName, passwordAuthenticationMethod);
                }
                throw new InvalidOperationException("PublicKeyPath or Password must be specified");
            }
        }

        public Ssh(string serverHost)
        {
            Config = new SshConfig()
            {
                ServerHost = serverHost
            };
        }

        internal protected SshConfig Config
        {
            get;
            set;
        }

        public static Ssh Server(string serverHost)
        {
            return new Ssh(serverHost);
        }

        public Ssh Port(int port)
        {
            Config.Port = port;
            return this;
        }

        public Ssh UserName(string userName)
        {
            Config.UserName = userName;
            return this;
        }

        public Ssh Password(string password)
        {
            Config.Password = password;
            return this;
        }

        public string Execute(string command, string terminalName = null)
        {
            SshClient client = new SshClient(Config.ToConnectionInfo());
            ShellStream stream = client.CreateShellStream(terminalName ?? "SshNetTerminal", 80, 30, 800, 600, 1024);

            StringBuilder response = new StringBuilder();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream)
            {
                AutoFlush = true
            };
            WriteRequest(command, writer, stream);
            response.AppendLine(ReadResponse(reader));
            return response.ToString().Trim();
        }
        
        private static void WriteRequest(string command, StreamWriter writer, ShellStream stream)
        {
            writer.WriteLine(command);
            while(stream.Length == 0)
            {
                Thread.Sleep(250);
            }
        }

        private static string ReadResponse(StreamReader reader)
        {
            StringBuilder result = new StringBuilder();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                result.AppendLine(line);
            }
            return result.ToString();
        }
    }
}
