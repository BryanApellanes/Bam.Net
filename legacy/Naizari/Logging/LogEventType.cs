/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Logging
{
    public enum LogEventType : int
    {
        None = 0,
        Information = 1,
        Warning = 2,
        Error = 3,
        Fatal = 4,
        Custom = 5
    }
}
