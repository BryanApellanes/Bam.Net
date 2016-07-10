using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class HouseDaoParentDaoColumns: QueryFilter<HouseDaoParentDaoColumns>, IFilterToken
    {
        public HouseDaoParentDaoColumns() { }
        public HouseDaoParentDaoColumns(string columnName)
            : base(columnName)
        { }
		
		public HouseDaoParentDaoColumns KeyColumn
		{
			get
			{
				return new HouseDaoParentDaoColumns("Id");
			}
		}	

				
        public HouseDaoParentDaoColumns Id
        {
            get
            {
                return new HouseDaoParentDaoColumns("Id");
            }
        }
        public HouseDaoParentDaoColumns Uuid
        {
            get
            {
                return new HouseDaoParentDaoColumns("Uuid");
            }
        }

        public HouseDaoParentDaoColumns HouseDaoId
        {
            get
            {
                return new HouseDaoParentDaoColumns("HouseDaoId");
            }
        }
        public HouseDaoParentDaoColumns ParentDaoId
        {
            get
            {
                return new HouseDaoParentDaoColumns("ParentDaoId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HouseDaoParentDao);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}