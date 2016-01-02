/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Configuration
{
    public class RequiredPropertyNotSetException: Exception
    {
        public RequiredPropertyNotSetException(Type target, PropertyInfo p) :
            base(@"" + target.Name + "." + p.Name + ": The required property '" + p.Name + "' for object of type '" + 
            target.Name + "' was not set." +
             "  Try adding an entry to the default config appSettings section where key=\"" + target.Name + "." + p.Name + "\"" +
             " and the value attribute is set to the required value.")
        {
            Type = target.Name;
            PropertyName = p.Name;
        }

        public string Type { get; set; }
        public string PropertyName { get; set; }
    }
}
