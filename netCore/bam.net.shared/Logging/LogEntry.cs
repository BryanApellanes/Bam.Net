/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Bam.Net.Logging
{
    [Serializable]
    public class LogEntry
    {
		public string Source
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}

		public string Computer
		{
			get;
			set;
		}

		public Severity Severity
		{
			get;
			set;
		}

		public string Category
		{
			get;
			set;
		}

		public int EventID
		{
			get;
			set;
		}

		public string User
		{
			get;
			set;
		}

		public DateTime Time
		{
			get;
			set;
		}

        public string MessageSignature { get; set; }

        public string[] MessageVariableValues { get; set; }

        public string StackTrace { get; set; }
    }
}
