/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace Bam.Net.CommandLine
{
    public static class Extensions
    {
        public static void InvokeInCurrentAppDomain(this ConsoleInvokeableMethod consoleInvokeableMethod)
        {
            CommandLineInterface.InvokeInCurrentAppDomain(consoleInvokeableMethod.Method, consoleInvokeableMethod.Provider, consoleInvokeableMethod.Parameters);
        }

        public static void InvokeInSeparateAppDomain(this ConsoleInvokeableMethod consoleInvokeableMethod)
        {
            CommandLineInterface.InvokeInSeparateAppDomain(consoleInvokeableMethod.Method, consoleInvokeableMethod.Provider, consoleInvokeableMethod.Parameters);
        }

        public static ProcessOutput Run(this string command, int timeout = 600000)
        {
            return command.Run(false, null, null, timeout);
        }

        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.
        /// </summary>
        /// <param name="command">a valid command line</param>
        /// <returns>ProcessOutput</returns>
        public static ProcessOutput Run(this string command, StringBuilder output, StringBuilder error, int timeout = 600000)
        {
            return command.Run(false, output, error, timeout);
        }

        public static ProcessOutput Run(this string command, bool promptForAdmin, int timeout = 600000)
        {
            return command.Run(promptForAdmin, null, null, timeout);
        }

        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="promptForAdmin"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, bool promptForAdmin, StringBuilder output = null, StringBuilder error = null, int timeout = 600000)
        {
            // fixed this to handle output correctly based on http://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
            ValidateCommand(command);

            string exe;
            string arguments;
            GetExeAndArguments(command, out exe, out arguments);

            return Run(string.IsNullOrEmpty(exe) ? command : exe, arguments, promptForAdmin, output, error, timeout);
        }

        public static ProcessOutput Run(this ProcessStartInfo startInfo, int timeout = 600000)
        {
            return Run(startInfo, null, null, timeout);
        }
        public static ProcessOutput Run(this ProcessStartInfo startInfo, StringBuilder output = null, StringBuilder error = null, int timeout = 600000)
        {
            output = output ?? new StringBuilder();
            error = error ?? new StringBuilder();
            ProcessOutputCollector receiver = new ProcessOutputCollector(d => output.Append(d), e => error.Append(e));
            return Run(startInfo, receiver, timeout);
        }

        public static ProcessOutput Run(this string command, Action<string> outputHandler, Action<string> errorHandler, bool promptForAdmin = false)
        {
            string exe;
            string arguments;
            GetExeAndArguments(command, out exe, out arguments);

            ProcessStartInfo startInfo = CreateStartInfo(promptForAdmin);
            startInfo.FileName = exe;
            startInfo.Arguments = arguments;
            ProcessOutputCollector receiver = new ProcessOutputCollector(outputHandler, errorHandler);
            return Run(startInfo, receiver);
        }

        public static ProcessOutput Run(this ProcessStartInfo startInfo, ProcessOutputCollector output = null, int timeout = 600000)
        {
            int exitCode = -1;
            bool timedOut = false;
            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
                using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
                {
                    process.OutputDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            outputWaitHandle.Set();
                        }
                        else
                        {
                            output.StandardOutput.AppendLine(e.Data);
                            output.DataHandler(e.Data);
                        }
                    };
                    process.ErrorDataReceived += (sender, e) =>
                    {
                        if (e.Data == null)
                        {
                            errorWaitHandle.Set();
                        }
                        else
                        {
                            output.StandardError.AppendLine(e.Data);
                            output.ErrorHandler(e.Data);
                        }
                    };

                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();

                    if (process.WaitForExit(timeout) &&
                        outputWaitHandle.WaitOne(timeout) &&
                        errorWaitHandle.WaitOne(timeout))
                    {
                        exitCode = process.ExitCode;
                        output.ExitCode = exitCode;
                    }
                    else
                    {
                        output.StandardError.AppendLine();
                        output.StandardError.AppendLine("Timeout elapsed prior to process completion");
                        timedOut = true;
                    }
                }
            }

            return new ProcessOutput(output.ToString(), output.StandardError.ToString(), exitCode, timedOut);
        }

        private static void GetExeAndArguments(string command, out string exe, out string arguments)
        {
            exe = string.Empty;
            arguments = string.Empty;
            string[] split = command.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (split.Length > 1)
            {
                exe = split[0];
                for (int i = 1; i < split.Length; i++)
                {
                    arguments += split[i];
                    if (i != split.Length - 1)
                        arguments += " ";
                }
            }
        }
        private static void ValidateCommand(string command)
        {
            Expect.IsFalse(string.IsNullOrEmpty(command), "command cannot be blank or null");
            Expect.IsFalse(command.Contains("\r"), "Multiple command lines not supported");
            Expect.IsFalse(command.Contains("\n"), "Multiple command lines not supported");
        }

        private static ProcessOutput Run(this string command, string arguments, bool promptForAdmin, StringBuilder output = null, StringBuilder error = null, int timeout = 600000)
        {
            ProcessStartInfo startInfo = CreateStartInfo(promptForAdmin);

            startInfo.FileName = command;
            startInfo.Arguments = arguments;

            return Run(startInfo, output, error, timeout);
        }
        private static ProcessStartInfo CreateStartInfo(bool promptForAdmin)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.ErrorDialog = false;
            startInfo.CreateNoWindow = true; ;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;

            if (promptForAdmin)
            {
                startInfo.Verb = "runas";
            }

            return startInfo;
        }
    }
}
