/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.NetworkInformation;

namespace Naizari.Helpers
{
    public class HostNameHelper
    {
        public static string GetCurrentHostName()
        {
            return IPGlobalProperties.GetIPGlobalProperties().HostName;
        }
    }
}
