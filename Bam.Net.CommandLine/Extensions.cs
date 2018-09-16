/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace Bam.Net.CommandLine
{
    public static class Extensions
    {
        /// <summary>
        /// Invokes the specified console method in current application domain.
        /// </summary>
        /// <param name="consoleMethod">The console method.</param>
        public static void InvokeInCurrentAppDomain(this ConsoleMethod consoleMethod)
        {
            CommandLineInterface.InvokeInCurrentAppDomain(consoleMethod.Method, consoleMethod.Provider, consoleMethod.Parameters);
        }

        public static void InvokeInSeparateAppDomain(this ConsoleMethod consoleInvokeableMethod)
        {
            CommandLineInterface.InvokeInSeparateAppDomain(consoleInvokeableMethod.Method, consoleInvokeableMethod.Provider, consoleInvokeableMethod.Parameters);
        }

        public static Task<ProcessOutput> RunAsync(this string command, int timeout = 600000)
        {
            return Task.Run(()=> Run(command, timeout));
        }

        /// <summary>
        /// Runs the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Executes the current string on the command line and returns the output.
        /// This operation will block if a timeout greater than 0 is specified
        /// </summary>
        /// <param name="command"></param>
        /// <param name="promptForAdmin"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, bool promptForAdmin, int timeout = 600000)
        {
            return command.Run(promptForAdmin, null, null, timeout);
        }

        /// <summary>
        /// Executes the current string on the command line
        /// and returns the output.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="promptForAdmin">if set to <c>true</c> [prompt for admin].</param>
        /// <param name="output">The output.</param>
        /// <param name="error">The error.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, bool promptForAdmin, StringBuilder output = null, StringBuilder error = null, int timeout = 600000)
        {
            // fixed this to handle output correctly based on http://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why
            ValidateCommand(command);

            GetExeAndArguments(command, out string exe, out string arguments);

            return Run(string.IsNullOrEmpty(exe) ? command : exe, arguments, promptForAdmin, output, error, timeout);
        }

        public static ProcessOutput Run(this string command, bool promptForAdmin, ProcessOutputCollector outputCollector, int timeout = 600000)
        {
            ValidateCommand(command);
            GetExeAndArguments(command, out string exe, out string arguments);
            ProcessStartInfo startInfo = CreateStartInfo(promptForAdmin);
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            return Run(startInfo, outputCollector, timeout);
        }

        /// <summary>
        /// Start a new process for the specified startInfo.  This 
        /// operation will block if a timeout greater than 0 is specified
        /// </summary>
        /// <param name="startInfo"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this ProcessStartInfo startInfo, int timeout = 600000)
        {
            return Run(startInfo, null, null, timeout);
        }

        /// <summary>
        /// Run the specified command in a separate process capturing the output
        /// and error streams if any
        /// </summary>
        /// <param name="startInfo"></param>
        /// <param name="output"></param>
        /// <param name="error"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this ProcessStartInfo startInfo, StringBuilder output, StringBuilder error = null, int timeout = 600000)
        {
            output = output ?? new StringBuilder();
            error = error ?? new StringBuilder();
            ProcessOutputCollector receiver = new ProcessOutputCollector(output, error);
            return Run(startInfo, receiver, timeout);
        }

        /// <summary>
        /// Run the specified command in a separate process capturing the output
        /// and error streams if any. This method will block if a timeout is specified, it will
        /// not block if timeout is null.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="onStandardOutput">The on standard output.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, Action<string> onStandardOutput, int? timeout = 600000)
        {
            return Run(command, null, onStandardOutput, (s) => { }, false, timeout);
        }

        public static ProcessOutput Run(this string command, Action<string> onStandardOut, Action<string> onErrorOut, int? timeout = null)
        {
            return Run(command, null, onStandardOut, onErrorOut, false, timeout);
        }

        public static ProcessOutput Run(this string command, Action<string> onStandardOut, Action<string> onErrorOut, bool promptForAdmin = false)
        {
            return Run(command, null, onStandardOut, onErrorOut, promptForAdmin);
        }
        /// <summary>
        /// Run the specified command in a separate process
        /// </summary>
        /// <param name="command">The command to run</param>
        /// <param name="onExit">EventHandler to execute when the process exits</param>
        /// <param name="timeout">The number of milliseconds to block before returning, specify 0 not to block</param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, EventHandler onExit, int? timeout)
        {
            return Run(command, onExit, null, null, false, timeout);
        }

        /// <summary>
        /// Run the specified command in a separate process capturing the output
        /// and error streams if any. This method will block if a timeout is specified, it will
        /// not block if timeout is null.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="onExit"></param>
        /// <param name="onStandardOut"></param>
        /// <param name="onErrorOut"></param>
        /// <param name="promptForAdmin"></param>
        /// <param name="timeout">The number of milliseconds to block before returning, specify 0 not to block</param>
        /// <returns></returns>
        public static ProcessOutput Run(this string command, EventHandler onExit, Action<string> onStandardOut = null, Action<string> onErrorOut = null, bool promptForAdmin = false, int? timeout = null)
        {
            GetExeAndArguments(command, out string exe, out string arguments);

            return Run(exe, arguments, onExit, onStandardOut, onErrorOut, promptForAdmin, timeout);
        }

        /// <summary>
        /// Execute the specified exe with the specified arguments.  This method will not block.
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="arguments"></param>
        /// <param name="onExit"></param>
        public static ProcessOutput Run(this string exe, string arguments, EventHandler onExit)
        {
            return Run(exe, arguments, onExit, null);
        }

        public static ProcessOutput RunAndWait(this ProcessStartInfo info, Action<string> standardOut = null, Action<string> errorOut = null, int timeOut = 60000)
        {
            ProcessOutputCollector output = new ProcessOutputCollector(standardOut, errorOut);
            return Run(info, output, timeOut);
        }

        public static ProcessStartInfo ToStartInfo(this string filePath, string arguments = null)
        {
            return ToStartInfo(filePath, new DirectoryInfo("."), arguments);
        }

        public static ProcessStartInfo ToStartInfo(this string filePath, DirectoryInfo workingDirectory, string arguments = null)
        {
            return ToStartInfo(new FileInfo(filePath), workingDirectory, arguments);
        }

        public static ProcessStartInfo ToStartInfo(this FileInfo fileInfo, DirectoryInfo workingDirectory, string arguments = null)
        {
            return new ProcessStartInfo
            {
                FileName = fileInfo.FullName,
                Arguments = arguments,
                WorkingDirectory = workingDirectory.FullName,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                ErrorDialog = false,
                CreateNoWindow = true
            };
        }

        public static ProcessStartInfo ToStartInfo(this string exe, string arguments, string workingDirectory, bool promptForAdmin = false)
        {
            ProcessStartInfo startInfo = CreateStartInfo(promptForAdmin);
            startInfo.FileName = exe;
            startInfo.Arguments = arguments;
            startInfo.WorkingDirectory = workingDirectory;
            return startInfo;
        }
        /// <summary>
        /// Runs the command and waits for it to complete.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="standardOut">The standard out.</param>
        /// <param name="errorOut">The error out.</param>
        /// <param name="timeOut">The time out.</param>
        /// <returns></returns>
        public static ProcessOutput RunAndWait(this string command, Action<string> standardOut = null, Action<string> errorOut = null, int timeOut = 60000)
        {
            GetExeAndArguments(command, out string exe, out string arguments);
            return Run(exe, arguments, (o, a) => { }, standardOut, errorOut, false, timeOut);
        }

        /// <summary>
        /// Runs the command and waits for it to complete.
        /// </summary>
        /// <param name="exe">The executable.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="onExit">The on exit.</param>
        /// <param name="timeOut">The time out.</param>
        public static void RunAndWait(this string exe, string arguments, EventHandler onExit = null, int timeOut = 60000)
        {
            Run(exe, arguments, onExit, timeOut);
        }

        /// <summary>
        /// Run the specified exe with the specified arguments, executing the specified onExit
        /// when the process completes.  This method will block if a timeout is specified, it will
        /// not block if timeout is null.
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="arguments"></param>
        /// <param name="onExit"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this string exe, string arguments, EventHandler onExit, int? timeout)
        {
            return Run(exe, arguments, onExit, null, null, false, timeout);
        }

        /// <summary>
        /// Run the specified exe with the specified arguments, executing the specified onExit
        /// when the process completes.  This method will block if a timeout is specified, it will
        /// not block if timeout is null.
        /// </summary>
        /// <param name="exe"></param>
        /// <param name="arguments"></param>
        /// <param name="onExit"></param>
        /// <param name="onStandardOut"></param>
        /// <param name="onErrorOut"></param>
        /// <param name="promptForAdmin"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this string exe, string arguments, EventHandler onExit, Action<string> onStandardOut = null, Action<string> onErrorOut = null, bool promptForAdmin = false, int? timeout = null)
        {
            ProcessStartInfo startInfo = CreateStartInfo(promptForAdmin);
            startInfo.FileName = exe;
            startInfo.Arguments = arguments;
            ProcessOutputCollector receiver = new ProcessOutputCollector(onStandardOut, onErrorOut);
            return Run(startInfo, onExit, receiver, timeout);
        }

        /// <summary>
        /// Run the specified command in a separate process capturing the output
        /// and error streams if any
        /// </summary>
        /// <param name="startInfo"></param>
        /// <param name="output"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this ProcessStartInfo startInfo, ProcessOutputCollector output, int? timeout = null)
        {
            return Run(startInfo, null, output, timeout);
        }

        /// <summary>
        /// Run the specified command in a separate process capturing the output
        /// and error streams if any. This method will block if a timeout is specified, it will
        /// not block if timeout is null.
        /// </summary>
        /// <param name="startInfo"></param>
        /// <param name="onExit"></param>
        /// <param name="output"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public static ProcessOutput Run(this ProcessStartInfo startInfo, EventHandler onExit, ProcessOutputCollector output = null, int? timeout = null)
        {
            int exitCode = -1;
            bool timedOut = false;
            output = output ?? new ProcessOutputCollector();

            Process process = new Process()
            {
                EnableRaisingEvents = true
            };
            if (onExit != null)
            {
                process.Exited += (o, a) => onExit(o, new ProcessExitEventArgs { EventArgs = a, ProcessOutput = new ProcessOutput(process, output.StandardOutput, output.StandardError) });
            }
            process.StartInfo = startInfo;
            AutoResetEvent outputWaitHandle = new AutoResetEvent(false);
            AutoResetEvent errorWaitHandle = new AutoResetEvent(false);
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data == null)
                {
                    outputWaitHandle.Set();
                }
                else
                {
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
                    output.ErrorHandler(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            if (timeout != null)
            {
                WaitForExit(output, timeout, ref exitCode, ref timedOut, process, outputWaitHandle, errorWaitHandle);
                return new ProcessOutput(output.StandardOutput.ToString(), output.StandardError.ToString(), exitCode, timedOut);
            }
            else
            {
                process.Exited += (o, a) =>
                {
                    Process p = (Process)o;
                    output.ExitCode = p.ExitCode;
                    p.Dispose();
                };
            }
            
            return new ProcessOutput(process, output.StandardOutput, output.StandardError);
        }

        private static void WaitForExit(ProcessOutputCollector output, int? timeout, ref int exitCode, ref bool timedOut, Process process, AutoResetEvent outputWaitHandle, AutoResetEvent errorWaitHandle)
        {
            if (process.WaitForExit(timeout.Value) &&
                outputWaitHandle.WaitOne(timeout.Value) &&
                errorWaitHandle.WaitOne(timeout.Value))
            {
                exitCode = process.ExitCode;
                output.ExitCode = exitCode;
                process.Dispose();
            }
            else
            {
                output.StandardError.AppendLine();
                output.StandardError.AppendLine("Timeout elapsed prior to process completion");
                timedOut = true;
            }
        }

        private static void GetExeAndArguments(string command, out string exe, out string arguments)
        {
            exe = command;
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
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                UseShellExecute = false,
                ErrorDialog = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            if (promptForAdmin)
            {
                startInfo.Verb = "runas";
            }

            return startInfo;
        }
    }
}
