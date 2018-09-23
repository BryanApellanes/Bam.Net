/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net;
using System.Diagnostics;

namespace Bam.Net.CommandLine
{
    [Serializable]
    public class ConsoleMethod
    {
        public ConsoleMethod()
        {
        }
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
        public string SwitchValue { get; set; }
        public MethodInfo Method { get; set; }
        public object[] Parameters { get; set; }

        object _provider;
        public object Provider
        {
            get
            {
                if(_provider == null && !Method.IsStatic)
                {
                    _provider= Method.DeclaringType.Construct();
                }
                return _provider;
            }
            set
            {
                _provider = value;
            }
        }

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
                handler(ex.GetInnerException());
            }
        }

        [DebuggerStepThrough]
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
            return FromType<ConsoleMethod>(typeToAnalyze, attributeAddorningMethod);
        }

        public static List<TConsoleMethod> FromType<TConsoleMethod>(Type typeToAnalyze, Type attributeAddorningMethod) where TConsoleMethod: ConsoleMethod, new()
        {
            List<TConsoleMethod> actions = new List<TConsoleMethod>();
            MethodInfo[] methods = typeToAnalyze.GetMethods();
            foreach (MethodInfo method in methods)
            {
                if (method.HasCustomAttributeOfType(attributeAddorningMethod, false, out object action))
                {
                    actions.Add(new TConsoleMethod { Method = method, Attribute = (Attribute)action });
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
            return FromAssembly<ConsoleMethod>(assembly, attrType);
        }

        public static List<TConsoleMethod> FromAssembly<TConsoleMethod>(Assembly assembly, Type attrType) where TConsoleMethod : ConsoleMethod, new()
        {
            List<TConsoleMethod> actions = new List<TConsoleMethod>();
            Type[] types = assembly.GetTypes();
            foreach (Type type in types)
            {
                actions.AddRange(FromType<TConsoleMethod>(type, attrType));
            }
            return actions;
        }
    }
}
