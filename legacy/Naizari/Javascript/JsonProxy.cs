/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Naizari.Javascript
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JsonProxy: Attribute
    {
        public JsonProxy()
        {
            this.VarName = string.Empty;
        }

        public JsonProxy(string varName)
        {
            this.VarName = varName;
        }

        public string VarName { get; set; }
    }
}
