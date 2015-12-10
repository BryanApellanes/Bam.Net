/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class RunningJobColumns: QueryFilter<RunningJobColumns>, IFilterToken
    {
        public RunningJobColumns() { }
        public RunningJobColumns(string columnName)
            : base(columnName)
        { }
		
		public RunningJobColumns KeyColumn
		{
			get
			{
				return new RunningJobColumns("Id");
			}
		}	
				
﻿        public RunningJobColumns Id
        {
            get
            {
                return new RunningJobColumns("Id");
            }
        }
﻿        public RunningJobColumns Uuid
        {
            get
            {
                return new RunningJobColumns("Uuid");
            }
        }
﻿        public RunningJobColumns Success
        {
            get
            {
                return new RunningJobColumns("Success");
            }
        }
﻿        public RunningJobColumns Message
        {
            get
            {
                return new RunningJobColumns("Message");
            }
        }
﻿        public RunningJobColumns BuildJobId
        {
            get
            {
                return new RunningJobColumns("BuildJobId");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RunningJob);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}