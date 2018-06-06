﻿using Bam.Net.CommandLine;
using Bam.Net.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Application
{
    public class BamDaemonProcess: Loggable
    {
        public BamDaemonProcess()
        {
            WorkingDirectory = ".\\";
            StandardOutSoFar = string.Empty;
            StandardErrorSoFar = string.Empty;
            StandardOut += (o, a) =>
            {
                BamDaemonProcessEventArgs args = (BamDaemonProcessEventArgs)a;
                StandardOutSoFar += $"\r\n{args.Message}";
                StandardOutLineCount++;
            };

            ErrorOut += (o, a) =>
            {
                BamDaemonProcessEventArgs args = (BamDaemonProcessEventArgs)a;
                StandardErrorSoFar += $"\r\n{args.Message}";
                ErrorOutLineCount++;
            };
        }

        public BamDaemonProcess(string commandLine) : this()
        {
            string[] split = commandLine.Split(new char[] { ' ' }, 2);
            FileName = split[0];
            if (split.Length > 1)
            {
                Arguments = split[1];
            }
        }

        [Verbosity(VerbosityLevel.Information, MessageFormat = "StandardOut: {Message}")]
        public event EventHandler StandardOut;

        [Verbosity(VerbosityLevel.Information, MessageFormat = "ErrorOut: {Message}")]
        public event EventHandler ErrorOut;        

        string _name;
        public string Name
        {
            get
            {
                return _name ?? FileName;
            }
            set
            {
                _name = value;
            }
        }

        public string FileName { get; set; }
        public string Arguments { get; set; }
        public string WorkingDirectory { get; set; }

        [JsonIgnore]
        public ProcessOutput ProcessOutput { get; set; }

        public void Kill()
        {
            ProcessOutput.Process.Kill();
        }

        public ProcessOutput Start(EventHandler onExit = null)
        {
            ExitHandler = onExit;
            ProcessStartInfo startInfo = new ProcessStartInfo(FileName, Arguments)
            {
                WorkingDirectory = WorkingDirectory,
                UseShellExecute = false,
                ErrorDialog = false,
                CreateNoWindow = true
            };
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            ProcessOutputCollector collector = new ProcessOutputCollector((data) => FireEvent(StandardOut, new BamDaemonProcessEventArgs { BambotProcess = this, Message = data }), (error) => FireEvent(ErrorOut, new BamDaemonProcessEventArgs { BambotProcess = this, Message = error }));
            ProcessOutput = startInfo.Run(onExit, collector);
            return ProcessOutput;
        }

        public override string ToString()
        {
            return $"{WorkingDirectory}:{Name} > {FileName} {Arguments}";
        }

        public int StandardOutLineCount { get; internal set; }
        public int ErrorOutLineCount { get; internal set; }

        internal string StandardOutSoFar { get; set; }
        internal string StandardErrorSoFar { get; set; }

        protected int RetryCount { get; set; }
        protected EventHandler ExitHandler { get; set; }        
    }
}
