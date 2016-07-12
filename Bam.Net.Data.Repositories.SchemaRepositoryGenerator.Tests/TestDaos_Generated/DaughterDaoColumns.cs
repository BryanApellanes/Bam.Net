using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class DaughterDaoColumns: QueryFilter<DaughterDaoColumns>, IFilterToken
    {
        public DaughterDaoColumns() { }
        public DaughterDaoColumns(string columnName)
            : base(columnName)
        { }
		
		public DaughterDaoColumns KeyColumn
		{
			get
			{
				return new DaughterDaoColumns("Id");
			}
		}	

				
        public DaughterDaoColumns Id
        {
            get
            {
                return new DaughterDaoColumns("Id");
            }
        }
        public DaughterDaoColumns Uuid
        {
            get
            {
                return new DaughterDaoColumns("Uuid");
            }
        }
        public DaughterDaoColumns Name
        {
            get
            {
                return new DaughterDaoColumns("Name");
            }
        }

        public DaughterDaoColumns ParentId
        {
            get
            {
                return new DaughterDaoColumns("ParentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DaughterDao);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}