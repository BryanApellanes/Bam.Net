/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.CommandLine
{
    public class ArgumentInfo
    {
        public ArgumentInfo(string name, bool allowNull, string description = null, string valueExample = null)
        {
            this.Name = name;
            this.AllowNullValue = allowNull;
            this.Description = description;
            this.ValueExample = valueExample;
        }

        public string ValueExample { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool AllowNullValue { get; set; }

        public static ArgumentInfo[] FromStringArray(string[] argumentNames)
        {
            return FromStringArray(argumentNames, false);
        }

        public static ArgumentInfo[] FromStringArray(string[] argumentNames, bool allowNulls)
        {
            List<ArgumentInfo> retVal = new List<ArgumentInfo>(argumentNames.Length);
            foreach (string name in argumentNames)
            {
                retVal.Add(new ArgumentInfo(name, allowNulls));
            }

            return retVal.ToArray();
        }
    }
}
