/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Extensions
{
    public class InvalidStringExtensionsFormatException: Exception
    {
        public InvalidStringExtensionsFormatException(string inputString)
            : base(string.Format("Invalid string format provided: {0}", inputString))
        { }
    }
}
