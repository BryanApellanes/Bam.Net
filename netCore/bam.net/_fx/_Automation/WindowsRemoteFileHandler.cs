using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.ExceptionHandling;
using Bam.Net.Logging;
using Bam.Net.Sys;

namespace Bam.Net.Automation
{
    public class WindowsRemoteFileHandler: Loggable, IRemoteFileHandler
    {
        public void CopyTo(string host, FileSystemInfo localData, string localPathOnRemote = null)
        {
            localPathOnRemote = localPathOnRemote ?? localData.FullName;
            if (Directory.Exists(localData.FullName))
            {
                DirectoryInfo localDir = new DirectoryInfo(localData.FullName);
                localDir.CopyTo(host, localPathOnRemote);
            }
            else if (File.Exists(localData.FullName))
            {
                FileInfo localFile = new FileInfo(localData.FullName);
                localFile.CopyTo(host, localPathOnRemote);
            }
        }

        public void Delete(string host, string localPathOnRemote)
        {
            DirectoryInfo adminSharePath = localPathOnRemote.GetAdminShareDirectory(host);
            adminSharePath.Delete(true);
        }

        public bool Exists(string host, string localPathOnRemote)
        {
            return localPathOnRemote.GetAdminShareDirectory(host).Exists;
        }
    }
}
