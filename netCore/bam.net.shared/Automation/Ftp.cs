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
using System.Net.FtpClient;

namespace Bam.Net.Automation
{
    /// <summary>
    /// Fluent wrapper around System.Net.FtpClient
    /// </summary>
    public class Ftp
    {
        internal protected enum ExistingFileBehavior
        {
            Invalid,
            Replace,
            Rename
        }

        internal protected class FtpConfig
        {
            public FtpConfig()
            {
                this.ExistingFileBehavior = Ftp.ExistingFileBehavior.Replace;
            }

            NetworkCredential _networkCredential;
            public NetworkCredential Credentials
            {
                get
                {
                    if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                    {
                        _networkCredential = new NetworkCredential(UserName, Password);
                    }

                    return _networkCredential;
                }
            }

            public string UserName { get; set; }
            public string Password { get; set; }
            
            public DirectoryInfo LocalRoot { get; set; }
            public string ServerHost { get; set; }
            public ExistingFileBehavior ExistingFileBehavior { get; set; }
        }

        public Ftp(string serverHost)
        {
            this.Config = new FtpConfig
            {
                ServerHost = serverHost
            };
        }

        protected internal FtpConfig Config
        {
            get;
            set;
        }

        public static Ftp Server(string serverHost)
        {
            return new Ftp(serverHost);
        }

        public Ftp UserName(string userName)
        {
            Config.UserName = userName;
            return this;
        }

        public Ftp Password(string password)
        {
            Config.Password = password;
            return this;
        }

        public Ftp Upload(string localRoot)
        {
            return Upload(new DirectoryInfo(localRoot));
        }

        public Ftp Upload(DirectoryInfo localRoot)
        {
            Config.LocalRoot = localRoot;
            return Upload();
        }

        protected Ftp Upload()
        {
            _current = Config.LocalRoot;
            UploadFiles();

            return this;
        }

        DirectoryInfo _current;

        private void UploadFiles()
        {
            FileInfo[] files = _current.GetFiles();
            files.Each(file =>
            {
                using (FtpClient client = new FtpClient())
                {
                    client.Host = Config.ServerHost;
                    client.Credentials = Config.Credentials;

                    string serverFile = file.FullName.TruncateFront(Config.LocalRoot.FullName.Length).Replace("\\","/");
                    string serverDir = serverFile.Truncate(file.Name.Length);

                    if (!client.DirectoryExists(serverDir))
                    {
                        client.CreateDirectory(serverDir);
                    }

                    using (Stream writeStream = client.OpenWrite(serverFile))
                    {
                        byte[] fileBytes = File.ReadAllBytes(file.FullName);
                        writeStream.Write(fileBytes, 0, fileBytes.Length);
                    }
                }
            });

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
