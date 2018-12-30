using Bam.Net.Application;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Bam.Net.Logging;
using Bam.Net.CommandLine;
using Bam.Net.Sys;
using Bam.Net.ExceptionHandling;
using Bam.Net.Encryption;
using Bam.Net.Configuration;

namespace Bam.Net.Automation
{
    public class WindowsServiceDeployer : Deployable
    {
        public WindowsServiceDeployer()
        {
            FileHandler = new WindowsRemoteFileHandler();
            AppSettingsWriter = new WindowsAppSettingsWriter();
        }

        public static void Deploy(DirectoryInfo localDirectory, WindowsServiceInfo svcInfo, ILogger logger = null)
        {
            WindowsServiceDeployer deployer = new WindowsServiceDeployer()
            {
                LocalDirectory = localDirectory,
                ServiceInfo = svcInfo
            };
            if(logger != null)
            {
                deployer.Subscribe(logger);
            }
            deployer.Deploy();
        }

        /// <summary>
        /// Gets or sets the local directory which is what is deployed.
        /// </summary>
        /// <value>
        /// The local directory.
        /// </value>
        public DirectoryInfo LocalDirectory { get; set; }

        public WindowsServiceInfo ServiceInfo { get; set; }

        public override void Deploy()
        {
            Deploy(ServiceInfo);
        }

        public void Deploy(WindowsServiceInfo svcInfo)
        {
            Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Host), "Host not specified");
            Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Name), "Name not specified");

            // the path on the target host specified as a local path
            string directoryPathOnRemote = Path.Combine(Paths.Sys, svcInfo.Name);
            string filePathOnRemote = Path.Combine(directoryPathOnRemote, svcInfo.FileName);
            DirectoryInfo adminShareDirectory = directoryPathOnRemote.GetAdminShareDirectory(svcInfo.Host);
            if (adminShareDirectory.Exists)
            {
                UninstallService(filePathOnRemote, adminShareDirectory);
            }

            OnCopyingFiles(EventArgs.Empty);

            FileHandler.CopyTo(svcInfo.Host, LocalDirectory, directoryPathOnRemote);

            OnCopiedFiles(EventArgs.Empty);
            InstallService(svcInfo, filePathOnRemote);

            if (svcInfo.AppSettings != null)
            {
                SetAppSettings(directoryPathOnRemote);
            }

            RemoteExecute(filePathOnRemote, "-s");
        }

        protected void InstallService()
        {
            string directoryPathOnRemote = Path.Combine(Paths.Sys, ServiceInfo.Name);
            string filePathOnRemote = Path.Combine(directoryPathOnRemote, ServiceInfo.FileName);
            InstallService(ServiceInfo, filePathOnRemote);
        }

        public event EventHandler InstallingService;
        public event EventHandler InstalledService;
        protected void InstallService(WindowsServiceInfo svcInfo, string filePathOnRemote)
        {
            FireEvent(InstallingService);
            string installSwitch = GetInstallSwitch(svcInfo.Host, svcInfo.Name);
            RemoteExecute(filePathOnRemote, installSwitch);
            FireEvent(InstalledService);
        }

        public event EventHandler UninstallingService;
        public event EventHandler UninstalledService;
        protected void UninstallService(string filePathOnRemote, DirectoryInfo adminShareDirectory)
        {
            FireEvent(UninstallingService);
            RemoteExecute(filePathOnRemote, "-k");
            RemoteExecute(filePathOnRemote, "-u");
            KillRemoteProcess(ServiceInfo.Host, ServiceInfo.FileName);
            try
            {
                adminShareDirectory.Delete(true);
            }
            catch (Exception ex)
            {
                OnExceptionDeletingDirectory(new ExceptionEventArgs(ex));
            }
            FireEvent(UninstalledService);
        }

        protected void RemoteExecute(string pathOnRemote, string commandArgs)
        {
            RemoteExecute(ServiceInfo.Host, pathOnRemote, commandArgs);
        }

        private string GetInstallSwitch(string machineName, string serviceName)
        {
            CredentialInfo credentialInfo = CredentialManager.Local.GetCredentials(machineName, serviceName);
            if (credentialInfo.IsNull) // if machine specific credentials aren't found, try just for the service
            {
                credentialInfo = CredentialManager.Local.GetCredentials(serviceName);
            }
            string installSwitch = credentialInfo.IsNull ? "-i" : $"-i -u:{credentialInfo.UserName} -p:{credentialInfo.Password}";
            CredentialEventArgs args = new CredentialEventArgs { HostName = machineName, ServiceName = serviceName };
            if (!credentialInfo.IsNull)
            {
                args.Found = true;
                OnCredentialsFound(args);
            }
            else
            {
                OnCredentialsNotFound(args);
            }            

            return installSwitch;
        }

        private void SetAppSettings(string directoryPathOnRemote)
        {
            string configPath = Path.Combine(directoryPathOnRemote, $"{ServiceInfo.FileName}.config");

            OnConfiguringService(EventArgs.Empty);
            AppSettingsWriter.SetAppSettings(ServiceInfo.Host, configPath, ServiceInfo.AppSettings);
            OnConfiguredService(EventArgs.Empty);
        }
    }
}
