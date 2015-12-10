/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Reflection;
using System.Management;
using Microsoft.Win32;
using System.Diagnostics;
using Bam.Net.Logging;
using System.IO;
using Bam.Net.Configuration;

namespace Bam.Net
{
    public class ServiceExe : ServiceBase
    {
        protected static WindowsLogger windowsLogger;
        protected static string serviceName;
        protected static string displayName;
        protected static string description;

        public static ServiceInfo Info
        {
            get
            {
                return new ServiceInfo(serviceName, displayName, description);
            }
            set
            {
                serviceName = value.ServiceName;
                displayName = value.DisplayName;
                description = value.Description;
            }
        }

        public static void RunService<T>() where T: ServiceBase, new()
        {
            RunService<T>(new T());
        }

        public static void RunService<T>(T serviceBaseImplementation) where T : ServiceBase
        {
            try
            {
                ValidateServiceInfo();
                CreateLog();
                ServiceBase.Run(serviceBaseImplementation);
            }
            catch (Exception ex)
            {
                FileLog(ex);
            }
        }

        static object lastDitchLock = new object();
        protected static void FileLog(Exception fatalEx)
        {
            lock (lastDitchLock)
            {
                string logPath = Path.Combine(Extensions.GetAppDataFolder(null), "{0}.fatal.log"._Format(serviceName));
                using (StreamWriter lastDitch = new StreamWriter(logPath, true))
                {
                    lastDitch.WriteLine(DateTime.Now.ToLongDateString() + ":\tFATAL ERROR STARTING SERVICE:\r\n\t" + fatalEx.Message);
                    lastDitch.WriteLine("\t" + fatalEx.StackTrace);
                }
            }
        }

        private static void ValidateServiceInfo()
        {
            Expect.IsNotNullOrEmpty(serviceName);
            Expect.IsNotNullOrEmpty(displayName);
            Expect.IsNotNullOrEmpty(description);            
        }

        /// <summary>
        /// Returns true if there were recognized command line
        /// arguments to be processed otherwise false.  Not to be confused
        /// with the command line arguments intended for CommandLineInterface
        /// implementations.  This method will process service 
        /// related comand line arguments.  A call to SetInfo should
        /// be made prior to this call to ensure all ServiceInfo 
        /// is already set.
        /// <br />
        /// <br />
        /// Valid values are:<br />
        /// -i [-ck:{credentialKey}]        Install the current ServiceExe implementation as a service. Using the specified appSettings key to look for credentials in the config file.  Expected keys would be key="{credentialKey}" and key="{credentialKey}Password".  Values will be retrieved using DefaultConfiguration<br />
        /// -i [true]                       Install the current ServiceExe implementation as a service allowing desktop interaction.  Cannot be combined with -ck option.<br />
        /// -s                              Start the service<br />
        /// -k                              Kill (stop) the service<br />
        /// -r                              Restart the service (stop and start)<br />
        /// -u                              Uninstall the service<br />
        /// -dl [-f]                        Delete the log.  Specify -f to force deletion without prompting to confirm<br />
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool ProcessCommandLineArgs(string[] args)
        {
            if (args.Length == 1)
            {
                if (args[0].Equals("-i"))
                {
                    Install(serviceName, displayName, description);
                    return true;
                }

                if (args[0].Equals("-s"))
                {
                    Start(serviceName);
                    return true;
                }

                if (args[0].Equals("-k"))
                {
                    Kill(serviceName);
                    return true;
                }

                if (args[0].Equals("-r"))
                {
                    Restart(serviceName);
                    return true;
                }

                if (args[0].Equals("-u"))
                {
                    Uninstall(serviceName);
                    return true;
                }

                if (args[0].Equals("-dl"))
                {
                    DeleteLog();
                    return true;
                }
            }

            if (args.Length == 2)
            {
                if ((args[0].Equals("-dl") || args[0].Equals("-f")) &&
                    (args[1].Equals("-dl") || args[1].Equals("-f")))
                {
                    DeleteLog(true);
                    return true;
                }

                // handle credentials -ck:'credential key'
                if ((args[0].Equals("-i") || args[0].StartsWith("-ck:")) &&
                    args[1].Equals("-i") || args[1].StartsWith("-ck:"))
                {
                    string ckArg = args[0].StartsWith("-ck:") ? args[0] : args[1];
                    string[] splitCk = ckArg.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splitCk.Length == 2)
                    {
                        Install(serviceName, displayName, description, splitCk[1]);
                        return true;
                    }
                }

                if ((args[0].Equals("-i") && (args[1].Equals("1") || args[1].ToLower().Equals("true"))))
                {
                    Install(serviceName, displayName, description, null, true);
                }
            }

            return false;
        }

        public static event ServiceInstallHandler StartInstall;
        public static event ServiceInstallHandler EndInstall;
        public static event ServiceInstallHandler StartUninstall;
        public static event ServiceInstallHandler EndUninstall;

        public static void OnStartInstall()
        {
            if (StartInstall != null)
            {
                try
                {
                    Console.WriteLine(string.Format("INFO:: Calling Pre-Install handlers of {0}...", serviceName));
                    StartInstall(new ServiceInfo(serviceName, displayName, description));
                }
                catch (Exception ex)
                {
                    string messageFormat = "ERROR:: An error occurred in Pre-Install handlers of {0}...";
                    Console.WriteLine(string.Format(messageFormat, serviceName));
                    Log.Default.AddEntry(messageFormat, ex, new string[] { serviceName });
                }
            }
        }

        public static void OnEndInstall()
        {
            if (EndInstall != null)
            {
                try
                {
                    Console.WriteLine(string.Format("INFO:: Calling Post-Install handlers of {0}...", serviceName));
                    EndInstall(new ServiceInfo(serviceName, displayName, description));
                }
                catch (Exception ex)
                {
                    string messageFormat = "ERROR:: An error occurred in Post-Install handlers of {0}...";
                    Console.WriteLine(string.Format(messageFormat, serviceName) + "\r\n\t" + ex.Message);
                    Log.Default.AddEntry(messageFormat, ex, new string[] { serviceName });
                }
            }
        }

        public static void OnStartUninstall()
        {
            if (StartUninstall != null)
            {
                try
                {
                    Console.WriteLine(string.Format("INFO:: Calling Pre-Uninstall handlers of {0}...", serviceName));
                    StartUninstall(new ServiceInfo(serviceName, displayName, description));
                }
                catch (Exception ex)
                {
                    string messageFormat = "ERROR:: An error occured in Pre-Uninstall handlers of {0}...";
                    Console.WriteLine(string.Format(messageFormat, serviceName) + "\r\n\t" + ex.Message);
                    Log.Default.AddEntry(messageFormat, ex, new string[] { serviceName });
                }
            }
        }

        public static void OnEndUninstall()
        {
            if (EndUninstall != null)
            {
                try
                {
                    Console.WriteLine(string.Format("INFO:: Calling Post-Uninstall handlers of {0}...", serviceName));
                    EndUninstall(new ServiceInfo(serviceName, displayName, description));
                }
                catch (Exception ex)
                {                    
                    string messageFormat = "ERROR:: An error occured in Post-Uninstall handlers of {0}...";
                    Console.WriteLine(string.Format(messageFormat, serviceName));
                    try
                    {
                        Log.Default.AddEntry(messageFormat, ex, new string[] { serviceName });
                    }
                    catch { }// logger was probably a WindowsLogger but it got deleted, no need to do anything about it here
                }
            }
        }

        /// <summary>
        /// Set the ServiceName, DisplayName and Description for the current 
        /// service.
        /// </summary>
        /// <param name="info"></param>
        public static void SetInfo(ServiceInfo info)
        {
            serviceName = info.ServiceName;
            displayName = info.DisplayName;
            description = info.Description;
        }

        public static void Install(ServiceInfo info, string credentialKey = "")
        {
            Install(info.ServiceName, info.DisplayName, info.Description, credentialKey);
        }

        public static void Install(string serviceName, string displayname, string description)
        {
            Install(serviceName, displayname, description, null, false);
        }

        public static void Install(string serviceName, string displayName, string description, string credentialKey)
        {
            Install(serviceName, displayName, description, credentialKey, false);
        }

        public static void Install(string serviceName, string displayName, string description, string credentialKey, bool allowDesktopInteract)
        {
            OnStartInstall();
            try
            {
                bool withIssues = false;
                string startName = string.IsNullOrEmpty(credentialKey) ? null : DefaultConfiguration.GetAppSetting(credentialKey);
                string startPassword = string.IsNullOrEmpty(credentialKey) ? null : DefaultConfiguration.GetAppSetting(credentialKey + "Password");
                startName = string.IsNullOrEmpty(startName) ? null : startName;
                startPassword = string.IsNullOrEmpty(startPassword) ? null : startPassword;
                Assembly cur = Assembly.GetEntryAssembly();
                Console.WriteLine("INFO:: Creating service from " + cur.Location);
                ManagementClass win32Service = new ManagementClass(@"\\.\root\cimv2:Win32_Service");

                Console.WriteLine("INFO:: ServiceName={0},DisplayName={1},Description={2}",
                    serviceName, displayName, description);
                
                //string acctName = network ? ".\\Local System" : ".\\Network Service";
                
                object ret = win32Service.InvokeMethod("Create", new object[] { serviceName, displayName, cur.Location, 16, 0, "Automatic", allowDesktopInteract, startName, startPassword, null, null, null });
                if (ret.ToString().Equals("0"))
                {
                    Console.WriteLine("INFO:: Service was created successfully");
                }
                else
                {
                    Console.WriteLine("WARN:: Return code was: {0}", ret.ToString());
                    withIssues = true;
                }

                if (!string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("INFO:: Setting description for '{0}' to '{1}'", displayName, description);

                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + serviceName, true);
                    if (key == null)
                    {
                        withIssues = true;
                    }
                    else
                    {
                        key.SetValue("Description", description, RegistryValueKind.String);
                    }
                }

                if (!withIssues)
                {
                    Console.WriteLine("INFO:: Operation complete");
                }
                else
                {
                    Console.WriteLine("WARN:: Operation completed with some issues");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:: Unable to create service: " + ex.Message);
            }

            OnEndInstall();
        }

        public static void Uninstall(string serviceName)
        {
            OnStartUninstall();
            try
            {
                Console.WriteLine("INFO:: Un-installing service \"{0}\"", serviceName);
                try
                {
                    ServiceController sc = new ServiceController(serviceName);
                    if (sc.Status == ServiceControllerStatus.Running)
                    {
                        Console.WriteLine("INFO:: Stopping service '" + serviceName+ "'...");
                        sc.Stop();
                        sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("WARN:: Request to stop service caused an error: " + ex.Message);
                }

                ManagementObject win32Service = new ManagementObject("Win32_Service.Name='" + serviceName+ "'");

                uint ret;
                ret = (uint)win32Service.InvokeMethod("Delete", null);
                if (ret == 0)
                {
                    Console.WriteLine("INFO:: Uninstall complete");
                }
                else
                {
                    Console.WriteLine("WARN:: Uninstall returned code " + ret.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:: Unable to uninstall service: {0}", ex.Message);
            }

            OnEndUninstall();
        }

        public static void Restart(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                Console.WriteLine("INFO:: Stopping service \"{0}\"", serviceName);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
                Console.WriteLine("INFO:: Starting service \"{0}\"", serviceName);
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));
                Console.WriteLine("INFO:: Operation complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:: An error occurred restarting the service: {0}",
                    ex.Message);
            }
        }

        public static void Start(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                Console.WriteLine("INFO:: Starting service \"{0}\"", serviceName);
                sc.Start();
                sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 10));
                Console.WriteLine("INFO:: Operation complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:: An error occurred starting the service: {0}", ex.Message);
            }
        }

        public static void Kill(string serviceName)
        {
            try
            {
                ServiceController sc = new ServiceController(serviceName);
                Console.WriteLine("INFO:: Stopping service \"{0}\"", serviceName);
                sc.Stop();
                sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
                Console.WriteLine("INFO:: Operation complete");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR:: An error occurred stopping the service: {0}", ex.Message);
            }
        }

        public static void DeleteLog()
        {
            DeleteLog(false);
        }
        public static void DeleteLog(bool force)
        {
            DeleteLog(force, serviceName);
        }

        public static void DeleteLog(bool force, string logName)
        {
            if (force)
            {
                Console.WriteLine("INFO: Deleting log...");
                EventLog.Delete(logName);
                Console.WriteLine("INFO: Operation complete");
                return;
            }

            Console.WriteLine("You are about to delete the Windows event log {0}", logName);
            Console.WriteLine("Do you want to continue? (y/N)");
            string answer = Console.ReadLine();

            if (answer.Equals("Y") || answer.Equals("y"))
            {
                Console.WriteLine("INFO: Deleting log...");
                EventLog.Delete(logName);
                Console.WriteLine("INFO: Operation complete");
            }
        }

        public static ILogger CreateLog()
        {
            return CreateLog(serviceName);
        }
        /// <summary>
        /// Creates a Windows event log for the extender of ServiceExe.
        /// serviceName must be defined.
        /// </summary>
        public static ILogger CreateLog(string svcName)
        {
            windowsLogger = (WindowsLogger)Log.CreateLogger("Windows");
            windowsLogger.ApplicationName = svcName;
            WindowsLogger.CreateLog(svcName, svcName);
            return windowsLogger;
        }
    }
}
