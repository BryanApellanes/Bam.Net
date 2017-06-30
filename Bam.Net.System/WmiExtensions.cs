using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Bam.Net.System
{
    public static class WmiExtensions
    {
        public static void StartProcess(this string computerName, string commandLine, string userName, string password)
        {
            StartProcess(computerName, commandLine, new ConnectionOptions { Username = userName, Password = password });
        }

        public static void StartProcess(this string computerName, string commandLine, ConnectionOptions connectionOptions = null)
        {
            object[] processToRun = new object[] { commandLine };
            string path = $@"\\{computerName}\root\cimv2";
            ManagementScope scope = connectionOptions == null ? new ManagementScope(path): new ManagementScope(path, connectionOptions);
            ManagementClass mgmtClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions());
            mgmtClass.InvokeMethod("Create", processToRun);
        }
    }
}
