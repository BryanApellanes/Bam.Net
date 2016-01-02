/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization
{
    public interface IContextAware 
    {
        SerializationContext Context { get; set; }
    }
}
