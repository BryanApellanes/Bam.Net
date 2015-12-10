/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Net.Share
{
    public enum CreateShareResultType
    {
        Success = 0,
        AccessDenied = 2,
        UnknownFailure = 8,
        InvalidName = 9,
        InvalidLevel = 10,
        InvalidParameter = 21,
        DuplicateShare = 22,
        RedirectedPath = 23,
        UnknownDeviceOrDirectory = 24,
        NetNameNotFound = 25
    }
}
