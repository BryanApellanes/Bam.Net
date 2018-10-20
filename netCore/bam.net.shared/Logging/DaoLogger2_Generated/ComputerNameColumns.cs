using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Logging.Data
{
    public class ComputerNameColumns: QueryFilter<ComputerNameColumns>, IFilterToken
    {
        public ComputerNameColumns() { }
        public ComputerNameColumns(string columnName)
            : base(columnName)
        { }
		
		public ComputerNameColumns KeyColumn
		{
			get
			{
				return new ComputerNameColumns("Id");
			}
		}	

				
        public ComputerNameColumns Id
        {
            get
            {
                return new ComputerNameColumns("Id");
            }
        }
        public ComputerNameColumns Uuid
        {
            get
            {
                return new ComputerNameColumns("Uuid");
            }
        }
        public ComputerNameColumns Cuid
        {
            get
            {
                return new ComputerNameColumns("Cuid");
            }
        }
        public ComputerNameColumns Value
        {
            get
            {
                return new ComputerNameColumns("Value");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ComputerName);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}