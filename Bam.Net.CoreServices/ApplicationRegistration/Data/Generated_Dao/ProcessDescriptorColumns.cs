using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ProcessDescriptorColumns: QueryFilter<ProcessDescriptorColumns>, IFilterToken
    {
        public ProcessDescriptorColumns() { }
        public ProcessDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ProcessDescriptorColumns KeyColumn
		{
			get
			{
				return new ProcessDescriptorColumns("Id");
			}
		}	

				
        public ProcessDescriptorColumns Id
        {
            get
            {
                return new ProcessDescriptorColumns("Id");
            }
        }
        public ProcessDescriptorColumns Uuid
        {
            get
            {
                return new ProcessDescriptorColumns("Uuid");
            }
        }
        public ProcessDescriptorColumns Cuid
        {
            get
            {
                return new ProcessDescriptorColumns("Cuid");
            }
        }
        public ProcessDescriptorColumns InstanceIdentifier
        {
            get
            {
                return new ProcessDescriptorColumns("InstanceIdentifier");
            }
        }
        public ProcessDescriptorColumns HashAlgorithm
        {
            get
            {
                return new ProcessDescriptorColumns("HashAlgorithm");
            }
        }
        public ProcessDescriptorColumns Hash
        {
            get
            {
                return new ProcessDescriptorColumns("Hash");
            }
        }
        public ProcessDescriptorColumns MachineName
        {
            get
            {
                return new ProcessDescriptorColumns("MachineName");
            }
        }
        public ProcessDescriptorColumns ProcessId
        {
            get
            {
                return new ProcessDescriptorColumns("ProcessId");
            }
        }
        public ProcessDescriptorColumns StartTime
        {
            get
            {
                return new ProcessDescriptorColumns("StartTime");
            }
        }
        public ProcessDescriptorColumns HasExited
        {
            get
            {
                return new ProcessDescriptorColumns("HasExited");
            }
        }
        public ProcessDescriptorColumns ExitTime
        {
            get
            {
                return new ProcessDescriptorColumns("ExitTime");
            }
        }
        public ProcessDescriptorColumns ExitCode
        {
            get
            {
                return new ProcessDescriptorColumns("ExitCode");
            }
        }
        public ProcessDescriptorColumns FilePath
        {
            get
            {
                return new ProcessDescriptorColumns("FilePath");
            }
        }
        public ProcessDescriptorColumns CommandLine
        {
            get
            {
                return new ProcessDescriptorColumns("CommandLine");
            }
        }
        public ProcessDescriptorColumns CreatedBy
        {
            get
            {
                return new ProcessDescriptorColumns("CreatedBy");
            }
        }
        public ProcessDescriptorColumns ModifiedBy
        {
            get
            {
                return new ProcessDescriptorColumns("ModifiedBy");
            }
        }
        public ProcessDescriptorColumns Modified
        {
            get
            {
                return new ProcessDescriptorColumns("Modified");
            }
        }
        public ProcessDescriptorColumns Deleted
        {
            get
            {
                return new ProcessDescriptorColumns("Deleted");
            }
        }
        public ProcessDescriptorColumns Created
        {
            get
            {
                return new ProcessDescriptorColumns("Created");
            }
        }

        public ProcessDescriptorColumns ApplicationId
        {
            get
            {
                return new ProcessDescriptorColumns("ApplicationId");
            }
        }
        public ProcessDescriptorColumns MachineId
        {
            get
            {
                return new ProcessDescriptorColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(ProcessDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}