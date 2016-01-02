/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;

namespace Bam.Net.CommandLine
{
    [Serializable]
    public class ConsoleInvokeableMethod
    {
        public ConsoleInvokeableMethod(MethodInfo method)
            : this(method, null)
        {
        }
        
        public ConsoleInvokeableMethod(MethodInfo method, Attribute actionInfo)
        {
            Method = method;
            Attribute = actionInfo;
        }

        public ConsoleInvokeableMethod(MethodInfo method, Attribute actionInfo, object provider, string switchValue = "")
            : this(method, actionInfo)
        {
            this.Provider = provider;
            this.SwitchValue = switchValue;
        }

        /// <summary>
        /// Used to help build usage examples for /? 
        /// </summary>
        public string SwitchValue { get; internal set; }
        public MethodInfo Method { get; internal set; }
        public object[] Parameters { get; set; }
        public object Provider { get; set; }

        public string Information
        {
            get
            {
                string info = Method.Name.PascalSplit(" ");
                if (Attribute != null)
                {
                    IInfoAttribute consoleAction = Attribute as IInfoAttribute;
                    if (consoleAction != null && !string.IsNullOrEmpty(consoleAction.Information))
                    {
                        info = consoleAction.Information;                        
                    }
                }

                return info;
            }
        }

        public Attribute Attribute { get; set; }

		public object Invoke()
		{
			object result = null;
			Exception thrown = null;
			try
			{
				if(!Method.IsStatic && Provider == null)
				{
					Provider = Method.DeclaringType.Construct();
				}
				result = Method.Invoke(Provider, Parameters);
			}
			catch (Exception ex)
			{
				throw ex.GetInnerException();				
			}

			return result;
		}
    }
}
