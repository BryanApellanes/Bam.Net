/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Automation.Data
{
    public class DeferredJobColumns: QueryFilter<DeferredJobColumns>, IFilterToken
    {
        public DeferredJobColumns() { }
        public DeferredJobColumns(string columnName)
            : base(columnName)
        { }
		
		public DeferredJobColumns KeyColumn
		{
			get
			{
				return new DeferredJobColumns("Id");
			}
		}	
				
﻿        public DeferredJobColumns Id
        {
            get
            {
                return new DeferredJobColumns("Id");
            }
        }
﻿        public DeferredJobColumns Uuid
        {
            get
            {
                return new DeferredJobColumns("Uuid");
            }
        }
﻿        public DeferredJobColumns Name
        {
            get
            {
                return new DeferredJobColumns("Name");
            }
        }
﻿        public DeferredJobColumns JobDirectory
        {
            get
            {
                return new DeferredJobColumns("JobDirectory");
            }
        }
﻿        public DeferredJobColumns HostName
        {
            get
            {
                return new DeferredJobColumns("HostName");
            }
        }
﻿        public DeferredJobColumns LastStepNumber
        {
            get
            {
                return new DeferredJobColumns("LastStepNumber");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DeferredJob);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}