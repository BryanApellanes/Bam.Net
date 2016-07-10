using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class SonDaoColumns: QueryFilter<SonDaoColumns>, IFilterToken
    {
        public SonDaoColumns() { }
        public SonDaoColumns(string columnName)
            : base(columnName)
        { }
		
		public SonDaoColumns KeyColumn
		{
			get
			{
				return new SonDaoColumns("Id");
			}
		}	

				
        public SonDaoColumns Id
        {
            get
            {
                return new SonDaoColumns("Id");
            }
        }
        public SonDaoColumns Uuid
        {
            get
            {
                return new SonDaoColumns("Uuid");
            }
        }
        public SonDaoColumns Name
        {
            get
            {
                return new SonDaoColumns("Name");
            }
        }

        public SonDaoColumns ParentId
        {
            get
            {
                return new SonDaoColumns("ParentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(SonDao);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}