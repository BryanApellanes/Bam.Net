/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    [AttributeUsage(AttributeTargets.Event)]
    public class VerbosityAttribute: Attribute
    {
		public VerbosityAttribute(VerbosityLevel eventType)
        {
            this.Value = eventType;
        }

		public VerbosityAttribute(LogEventType eventType)
		{
			this.Value = (VerbosityLevel)eventType;
		}

        public VerbosityLevel Value { get; set; }

		/// <summary>
		/// The "NamedFormat" message format to use when outputting messages
		/// </summary>
        public string MessageFormat { get; set; }

        [DebuggerStepThrough]
        public bool TryGetMessage(object value, out string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(MessageFormat))
                {
                    message = MessageFormat.NamedFormat(value);
                }
                else
                {
                    message = value.TryPropertiesToString();
                }

                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }
    }
}
