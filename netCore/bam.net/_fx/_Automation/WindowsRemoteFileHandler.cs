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
            DirectoryInfo adminSharePath = localPathOnRemote.GetAdminShareDirectory(host);
            adminSharePath.CopyTo(host, localPathOnRemote);
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
