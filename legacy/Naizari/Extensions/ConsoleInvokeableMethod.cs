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
    public class ConsoleInvokeableMethod
    {
        public ConsoleInvokeableMethod(MethodInfo method)
            : this(method, null)
        {
        }

        //Attribute action; // intended to be a ConsoleAction attribute but changed to Attribute so the TestMethod attribute can be passed in as well
        public ConsoleInvokeableMethod(MethodInfo method, Attribute actionInfo)
        {
            Method = method;
            //action = actionInfo;
            Attribute = actionInfo;
        }

        public MethodInfo Method { get; internal set; }
        public string Information
        {
            get
            {
                if (Attribute == null)
                {
                    return Method.Name.PascalSplit(" ");
                }

                ConsoleAction consoleAction = Attribute as ConsoleAction;
                if (consoleAction != null)
                {
                    string info = consoleAction.Information;//((ConsoleAction)Attribute).Information;
                    if (string.IsNullOrEmpty(info))
                    {
                        return Method.Name.PascalSplit(" ");
                    }
                    else
                    {
                        return info;
                    }
                }
                else
                {
                    return Method.Name.PascalSplit(" ");
                }
            }
        }

        public Attribute Attribute { get; set; }
    }
}
