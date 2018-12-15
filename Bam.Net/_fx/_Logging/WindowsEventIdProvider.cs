/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Logging
{
    /// <summary>
    /// Event Id provider for use with windows log, always returns 0
    /// </summary>
    public class WindowsEventIdProvider: IEventIdProvider
    {
        public int GetEventId(string applicationName, string messageSignature)
        {
            return 0;
        }
    }
}
