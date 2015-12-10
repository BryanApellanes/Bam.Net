/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Helpers
{
    public abstract class CustomException: Exception
    {
        public CustomException(string messageFormat, params object[] formatArgs)
            : base(string.Format(messageFormat, formatArgs))
        {
        }
    
    }
}
