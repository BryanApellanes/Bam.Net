using Bam.Net.CommandLine;
using Bam.Net.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Automation
{
    public abstract class Deployable: Loggable, IDeployable
    {
        public event EventHandler ProcessExecuting;
        public event EventHandler ProcessExecuted;

        public event EventHandler KillingProcess;
        public event EventHandler KilledProcess;

        public event EventHandler CopyingFiles;
        public event EventHandler CopiedFiles;

        public event EventHandler ExceptionDeletingDirectory;

        public event EventHandler CredentialsFound;
        public event EventHandler CredentialsNotFound;

        public event EventHandler ConfiguringService;
        public event EventHandler ConfiguredService;
        
        public IRemoteFileHandler FileHandler { get; set; }
        public IAppSettingsWriter AppSettingsWriter { get; set; }
        public abstract void Deploy();

        protected void OnProcessExecuting(EventArgs args)
        {
            FireEvent(ProcessExecuting, args);
        }

        protected void OnProcessExecuted(EventArgs args)
        {
            FireEvent(ProcessExecuted, args);
        }

        protected void OnKillingProcess(EventArgs args)
        {
            FireEvent(KillingProcess, args);
        }

        protected void OnKilledProcess(EventArgs args)
        {
            FireEvent(KilledProcess, args);
        }

        protected void OnCopyingFiles(EventArgs args)
        {
            FireEvent(CopyingFiles, args);
        }

        protected void OnCopiedFiles(EventArgs args)
        {
            FireEvent(CopiedFiles, args);
        }

        protected void OnExceptionDeletingDirectory(EventArgs args)
        {
            FireEvent(ExceptionDeletingDirectory, args);
        }

        protected void OnCredentialsFound(EventArgs args)
        {
            FireEvent(CredentialsFound, args);
        }

        protected void OnCredentialsNotFound(EventArgs args)
        {
            FireEvent(CredentialsNotFound, args);
        }

        protected void OnConfiguringService(EventArgs args)
        {
            FireEvent(ConfiguringService, args);
        }

        protected void OnConfiguredService(EventArgs args)
        {
            FireEvent(ConfiguredService, args);
        }

        protected void KillRemoteProcess(string host, string fileName)
        {
            ProcessOutputEventArgs args = new ProcessOutputEventArgs { Name = fileName, Description = $"{host}:{fileName}" };
            args.Name = fileName;
            OnKillingProcess(args);
            ProcessOutput killOutput = PsKill.Run(host, fileName);
            args.ProcessOutput = killOutput;
            OnKilledProcess(args);
        }

        protected void RemoteExecute(string host, string filePathOnRemote, string commandArgs)
        {
            ProcessOutputEventArgs args = new ProcessOutputEventArgs { Name = filePathOnRemote, Description = $"{host}:{filePathOnRemote}" };
            string command = $"{filePathOnRemote} {commandArgs}";
            args.Description += $" {command}";
            OnProcessExecuting(args);
            ProcessOutput killOutput = PsExec.Run(host, command);
            args.ProcessOutput = killOutput;
            OnProcessExecuted(args);
        }
    }
}
