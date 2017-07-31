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
    public class ConsoleMethod
    {
        public ConsoleMethod(MethodInfo method)
            : this(method, null)
        {
        }
        
        public ConsoleMethod(MethodInfo method, Attribute actionInfo)
        {
            Method = method;
            Attribute = actionInfo;
        }

        public ConsoleMethod(MethodInfo method, Attribute actionInfo, object provider, string switchValue = "")
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
                    if (Attribute is IInfoAttribute consoleAction && !string.IsNullOrEmpty(consoleAction.Information))
                    {
                        info = consoleAction.Information;
                    }
                }

                return info;
            }
        }

        public Attribute Attribute { get; set; }

        public void TryInvoke(Action<Exception> exceptionHandler = null)
        {
            try
            {
                Invoke();
            }
            catch (Exception ex)
            {
                Action<Exception> handler = exceptionHandler ?? ((e) => { });
                handler(ex);
            }
        }

		public object Invoke()
		{
			object result = null;
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

        public static List<ConsoleMethod> FromType(Type typeToAnalyze, Type attributeAddorningMethod)
        {
            List<ConsoleMethod> actions = new List<ConsoleMethod>();
            MethodInfo[] methods = typeToAnalyze.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.HasCustomAttributeOfType(attributeAddorningMethod, false, out object action))
                {
                    actions.Add(new ConsoleMethod(method, (Attribute)action));
                }
            }

            return actions;
        }
        public static List<ConsoleMethod> FromAssembly<TAttribute>(Assembly assembly)
        {
            return FromAssembly(assembly, typeof(TAttribute));
        }
        public static List<ConsoleMethod> FromAssembly(Assembly assembly, Type attrType)
        {
            List<ConsoleMethod> actions = new List<ConsoleMethod>();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                actions.AddRange(FromType(type, attrType));
            }
            return actions;
        }
    }
}
