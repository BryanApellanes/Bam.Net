/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KLGates.Extensions;
using KLGates.Test;
using KLGates.Javascript;
using System.Diagnostics;

namespace KLGates.Javascript.DataControls
{
    [Serializable]
    public class JQueryOptions
    {
        protected Dictionary<string, string> options;
        public JQueryOptions(params string[] options)
        {
            foreach (string option in options)
            {
                string[] split = StringExtensions.DelimitSplit(option, ":");
                Expect.IsTrue(split.Length == 2, string.Format("Invalid JQuery option specified: {0}", option));
                this.options.Add(split[0], split[1]);
            }
        }

        public void AddOption(string key, string value)
        {
            this.options.Add(key, value);
        }

        public override string ToString()
        {
            StringBuilder retVal = new StringBuilder();
            retVal.Append("{");
            foreach (string key in this.options.Keys)
            {
                NewLineAndTab(retVal);
                retVal.Append(key + ": " + this.options[key]);
            }

            retVal.Append("\r\n}");
            return retVal.ToString();
        }

        [Conditional("DEBUG")]
        private void NewLineAndTab(StringBuilder builder)
        {
            builder.Append("\r\n\t");
        }
    }
}
