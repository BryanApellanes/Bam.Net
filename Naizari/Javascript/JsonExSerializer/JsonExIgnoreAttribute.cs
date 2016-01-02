/*
	Copyright © Bryan Apellanes 2015  
*/
/*
 * Copyright (c) 2007, Ted Elliott
 * Code licensed under the New BSD License:
 * http://code.google.com/p/jsonexserializer/wiki/License
 */
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization
{
    [AttributeUsage(AttributeTargets.Property|AttributeTargets.Field,Inherited=false)]
    public sealed class JsonExIgnoreAttribute : System.Attribute
    {
    }
}
