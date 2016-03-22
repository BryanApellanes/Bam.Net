using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class DaoBaseItemColumns: QueryFilter<DaoBaseItemColumns>, IFilterToken
    {
        public DaoBaseItemColumns() { }
        public DaoBaseItemColumns(string columnName)
            : base(columnName)
        { }
		
		public DaoBaseItemColumns KeyColumn
		{
			get
			{
				return new DaoBaseItemColumns("Id");
			}
		}	

				
        public DaoBaseItemColumns Id
        {
            get
            {
                return new DaoBaseItemColumns("Id");
            }
        }
        public DaoBaseItemColumns Uuid
        {
            get
            {
                return new DaoBaseItemColumns("Uuid");
            }
        }
        public DaoBaseItemColumns Name
        {
            get
            {
                return new DaoBaseItemColumns("Name");
            }
        }
        public DaoBaseItemColumns Created
        {
            get
            {
                return new DaoBaseItemColumns("Created");
            }
        }
        public DaoBaseItemColumns IsCool
        {
            get
            {
                return new DaoBaseItemColumns("IsCool");
            }
        }
        public DaoBaseItemColumns IntValue
        {
            get
            {
                return new DaoBaseItemColumns("IntValue");
            }
        }
        public DaoBaseItemColumns LongValue
        {
            get
            {
                return new DaoBaseItemColumns("LongValue");
            }
        }
        public DaoBaseItemColumns DecimalValue
        {
            get
            {
                return new DaoBaseItemColumns("DecimalValue");
            }
        }
        public DaoBaseItemColumns ByteArrayValue
        {
            get
            {
                return new DaoBaseItemColumns("ByteArrayValue");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DaoBaseItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}