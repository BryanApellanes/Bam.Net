using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class HouseDaoColumns: QueryFilter<HouseDaoColumns>, IFilterToken
    {
        public HouseDaoColumns() { }
        public HouseDaoColumns(string columnName)
            : base(columnName)
        { }
		
		public HouseDaoColumns KeyColumn
		{
			get
			{
				return new HouseDaoColumns("Id");
			}
		}	

				
        public HouseDaoColumns Id
        {
            get
            {
                return new HouseDaoColumns("Id");
            }
        }
        public HouseDaoColumns Uuid
        {
            get
            {
                return new HouseDaoColumns("Uuid");
            }
        }
        public HouseDaoColumns Name
        {
            get
            {
                return new HouseDaoColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(HouseDao);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}