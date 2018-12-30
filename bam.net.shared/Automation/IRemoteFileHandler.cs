using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Bam.Net.Automation
{
    public interface IRemoteFileHandler
    {
        void CopyTo(string host, FileSystemInfo localData, string localPathOnRemote = null);
        void Delete(string host, string localPathOnRemote);
        bool Exists(string host, string localPathOnRemote);
    }
}
