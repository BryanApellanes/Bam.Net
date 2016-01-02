/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.AlphaOmega
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonIgnore: Attribute
    {
        public JsonIgnore() { }

    }
}
