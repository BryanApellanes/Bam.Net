using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation.Windows
{
    public class WindowsService
    {
        //public static Deploy(DirectoryInfo directoryInfo, )
        //{
        //    //Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Host), "Host not specified");
        //    //Args.ThrowIf(string.IsNullOrEmpty(svcInfo.Name), "Name not specified");
        //    ////      copy the latest binaries to \\computer\c$\bam\sys\{Name}
        //    //string remoteDirectory = Path.Combine(Paths.Sys, svcInfo.Name);
        //    //string remoteFile = Path.Combine(remoteDirectory, svcInfo.FileName);

        //    //DirectoryInfo remoteDirectoryInfo = remoteDirectory.GetAdminShareDirectory(svcInfo.Host);
        //    //if (remoteDirectoryInfo.Exists)
        //    //{
        //    //    CallServiceExecutable(svcInfo, "Kill", remoteFile, "-k");
        //    //    CallServiceExecutable(svcInfo, "Un-install", remoteFile, "-u");
        //    //    KillProcess(svcInfo.Host, svcInfo.FileName);
        //    //    string host = svcInfo.Host;
        //    //    TryDeleteDirectory(remoteDirectoryInfo, host);
        //    //}

        //    //OutLineFormat("Copying files for {0} to {1}", ConsoleColor.Cyan, svcInfo.Name, svcInfo.Host);
        //    //latestBinaries.CopyTo(svcInfo.Host, remoteDirectory);
        //    //string installSwitch = GetInstallSwitch(svcInfo.Host, svcInfo.Name);
        //    //CallServiceExecutable(svcInfo, "Install", remoteFile, installSwitch);

        //    //if (svcInfo.AppSettings != null)
        //    //{
        //    //    SetAppSettings(svcInfo.Host, remoteDirectory, svcInfo.FileName, svcInfo.AppSettings);
        //    //}

        //    //CallServiceExecutable(svcInfo, "Start", remoteFile, "-s");
        //}
    }
}
