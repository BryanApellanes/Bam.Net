using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class ProcessRuntimeDescriptorColumns: QueryFilter<ProcessRuntimeDescriptorColumns>, IFilterToken
    {
        public ProcessRuntimeDescriptorColumns() { }
        public ProcessRuntimeDescriptorColumns(string columnName)
            : base(columnName)
        { }
		
		public ProcessRuntimeDescriptorColumns KeyColumn
		{
			get
			{
				return new ProcessRuntimeDescriptorColumns("Id");
			}
		}	

				
        public ProcessRuntimeDescriptorColumns Id
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("Id");
            }
        }
        public ProcessRuntimeDescriptorColumns Uuid
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("Uuid");
            }
        }
        public ProcessRuntimeDescriptorColumns Cuid
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("Cuid");
            }
        }
        public ProcessRuntimeDescriptorColumns ApplicationName
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("ApplicationName");
            }
        }
        public ProcessRuntimeDescriptorColumns MachineName
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("MachineName");
            }
        }
        public ProcessRuntimeDescriptorColumns CommandLine
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("CommandLine");
            }
        }
        public ProcessRuntimeDescriptorColumns FilePath
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("FilePath");
            }
        }
        public ProcessRuntimeDescriptorColumns Created
        {
            get
            {
                return new ProcessRuntimeDescriptorColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ProcessRuntimeDescriptor);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}