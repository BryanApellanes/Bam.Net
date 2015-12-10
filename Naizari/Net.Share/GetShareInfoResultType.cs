/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Net.Share
{
    public enum GetShareInfoResultType
    {
        Invalid = -1,
        ErrorAccessDenied = 5,
        ErrorInvalidLevel = 124, // unimplemented level for info
        ErrorInvalidParameter = 87,
        ErrorMoreData = 234,
        ErrorNotEnoughMemory = 8,
        NerrBufferTooSmall = 2123, // The API return buffer is too small.
        NerrNetNameNotFound = 2310, // This shared resource does not exist.
        NerrSuccess = 0
    }
}
