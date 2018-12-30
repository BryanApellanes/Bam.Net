using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class HouseColumns: QueryFilter<HouseColumns>, IFilterToken
    {
        public HouseColumns() { }
        public HouseColumns(string columnName)
            : base(columnName)
        { }
		
		public HouseColumns KeyColumn
		{
			get
			{
				return new HouseColumns("Id");
			}
		}	

				
        public HouseColumns Id
        {
            get
            {
                return new HouseColumns("Id");
            }
        }
        public HouseColumns Uuid
        {
            get
            {
                return new HouseColumns("Uuid");
            }
        }
        public HouseColumns Name
        {
            get
            {
                return new HouseColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(House);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}