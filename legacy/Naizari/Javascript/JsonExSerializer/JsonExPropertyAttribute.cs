/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace Naizari.Javascript.JsonExSerialization
{
    /// <summary>
    /// Force serialization of a property that would otherwise be ignored
    /// </summary>
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class JsonExPropertyAttribute : Attribute
    {
        public JsonExPropertyAttribute()
        {
        }
    }

}
