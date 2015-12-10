/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net
{
	[Serializable]
    public class IpcMessageLockInfo
    {
        public IpcMessageLockInfo()
        {
            this.ProcessId = Process.GetCurrentProcess().Id;
            this.MachineName = Environment.MachineName;
        }

        public int ProcessId { get; set; }

        public string MachineName { get; set; }
    }
}
