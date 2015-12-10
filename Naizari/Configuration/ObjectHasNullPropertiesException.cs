/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Configuration
{
    public class ObjectHasNullPropertiesException: Exception
    {
        public ObjectHasNullPropertiesException(string message)
            : base(message)
        { }
    }
}
