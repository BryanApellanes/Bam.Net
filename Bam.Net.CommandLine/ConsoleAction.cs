/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Bam.Net.CommandLine
{
    [Serializable]
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

        public ConsoleAction(string commandLineSwitch, string information)
            : this(information)
        {
            this.CommandLineSwitch = commandLineSwitch;
        }

        public ConsoleAction(string commandLineSwitch, string valueExample, string information)
            : this(commandLineSwitch, information)
        {
            this.ValueExample = valueExample;
        }

        /// <summary>
        /// A string representing an example of a valid value, for example [filename]
        /// </summary>
        public string ValueExample
        {
            get;
            private set;
        }

        public string CommandLineSwitch
        {
            get;
            private set;
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
