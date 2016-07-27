/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
	public class SchemaExtractorEventArgs: EventArgs
	{
		public SchemaExtractorEventArgs() {}
		public string Table { get; set; }
		public string Column { get; set; }
        public string Property { get; set; }
        public ForeignKeyColumn ForeignKeyColumn { get; set; }
	}
}
