/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    public class EventIdProvider
    {
        public virtual int GetEventId(string applicationName, string messageSignature)
        {
            return (applicationName + messageSignature).GetHashCode();
        }
    }
}
