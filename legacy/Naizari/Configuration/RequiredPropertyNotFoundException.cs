/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Configuration
{
    public class RequiredPropertyNotFoundException : Exception
    {
        public RequiredPropertyNotFoundException(string message)
            : base(message)
        { }

        public RequiredPropertyNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
