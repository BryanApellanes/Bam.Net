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
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Enum | AttributeTargets.Struct,AllowMultiple=false,Inherited=false)]
    public sealed class JsonConvertAttribute : System.Attribute
    {
        private Type _converter;
        private object _context;

        public JsonConvertAttribute(Type converter)
        {
            _converter = converter;
        }

        public Type Converter
        {
            get { return this._converter; }
        }

        public object Context
        {
            get { return this._context; }
            set { this._context = value; }
        }


    }
}
