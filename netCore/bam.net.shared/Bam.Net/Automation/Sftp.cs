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
    /// Fluent wrapper around System.Net.SftpClient
    /// </summary>
    public class Sftp
    {
        internal protected class SftpConfig
        {
            public SftpConfig()
            {
                RemoteRoot = "C:\\bam\\files\\in";
                LocalRoot = "C:\\bam\\files\\out";
            }
            
            public string UserName { get; set; }
            public string Password { get; set; }            
            public string ServerHost { get; set; }
            public int Port { get; set; }
            public string PublicKeyPath { get; set; }
            public string RemoteRoot { get; set; }
            public string LocalRoot { get; set; }
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

        public Sftp(string serverHost)
        {
            Config = new SftpConfig()
            {
                ServerHost = serverHost
            };
            _current = new DirectoryInfo(Config.LocalRoot);
        }

        internal protected SftpConfig Config
        {
            get;
            set;
        }

        public static Sftp Server(string serverHost)
        {
            return new Sftp(serverHost);
        }

        public Sftp Port(int port)
        {
            Config.Port = port;
            return this;
        }

        public Sftp RemoteRoot(string directoryPath)
        {
            Config.RemoteRoot = directoryPath;
            return this;
        }

        public Sftp UserName(string userName)
        {
            Config.UserName = userName;
            return this;
        }

        public Sftp Password(string password)
        {
            Config.Password = password;
            return this;
        }

        public Sftp Upload(string localRoot)
        {
            return Upload(new DirectoryInfo(localRoot));
        }

        public Sftp Upload(DirectoryInfo localRoot)
        {
            _current = localRoot;
            return Upload();
        }

        protected Sftp Upload()
        {
            UploadFiles();

            return this;
        }

        DirectoryInfo _current;

        private void UploadFiles()
        {
            using (SftpClient client = new SftpClient(Config.ToConnectionInfo()))
            {
                client.Connect();
                FileInfo[] files = _current.GetFiles();
                files.Each(file =>
                {
                    using (FileStream fs = new FileStream(file.FullName, FileMode.Open))
                    {
                        client.BufferSize = 1024;
                        client.UploadFile(fs, Path.Combine(Config.RemoteRoot, file.Name));
                    }
                });
            }

            Traverse();
        }

        private void Traverse()
        {
            DirectoryInfo[] subDirs = _current.GetDirectories();
            subDirs.Each(subDir =>
            {
                _current = subDir;
                UploadFiles();
            });
        }
    }
}
