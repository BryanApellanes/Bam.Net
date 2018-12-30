/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Naizari.Extensions
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple=false, Inherited=true)]
    public class ConsoleAction: Attribute, IInfoAttribute
    {
        public ConsoleAction()
        {

        }

        public ConsoleAction(string information)
        {
            Information = information;
        }

        public string Information { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Information))
                return Information;

            return base.ToString();
        }
    }
}
