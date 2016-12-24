using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.Data.Daos
{
    public class NicColumns: QueryFilter<NicColumns>, IFilterToken
    {
        public NicColumns() { }
        public NicColumns(string columnName)
            : base(columnName)
        { }
		
		public NicColumns KeyColumn
		{
			get
			{
				return new NicColumns("Id");
			}
		}	

				
        public NicColumns Id
        {
            get
            {
                return new NicColumns("Id");
            }
        }
        public NicColumns Uuid
        {
            get
            {
                return new NicColumns("Uuid");
            }
        }
        public NicColumns Cuid
        {
            get
            {
                return new NicColumns("Cuid");
            }
        }
        public NicColumns AddressFamily
        {
            get
            {
                return new NicColumns("AddressFamily");
            }
        }
        public NicColumns Value
        {
            get
            {
                return new NicColumns("Value");
            }
        }
        public NicColumns MacAddress
        {
            get
            {
                return new NicColumns("MacAddress");
            }
        }
        public NicColumns CreatedBy
        {
            get
            {
                return new NicColumns("CreatedBy");
            }
        }
        public NicColumns Created
        {
            get
            {
                return new NicColumns("Created");
            }
        }
        public NicColumns ModifiedBy
        {
            get
            {
                return new NicColumns("ModifiedBy");
            }
        }
        public NicColumns Modified
        {
            get
            {
                return new NicColumns("Modified");
            }
        }
        public NicColumns Deleted
        {
            get
            {
                return new NicColumns("Deleted");
            }
        }

        public NicColumns MachineId
        {
            get
            {
                return new NicColumns("MachineId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Nic);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}