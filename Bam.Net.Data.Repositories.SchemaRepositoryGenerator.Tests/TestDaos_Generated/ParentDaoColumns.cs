using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes._Dao_
{
    public class ParentDaoColumns: QueryFilter<ParentDaoColumns>, IFilterToken
    {
        public ParentDaoColumns() { }
        public ParentDaoColumns(string columnName)
            : base(columnName)
        { }
		
		public ParentDaoColumns KeyColumn
		{
			get
			{
				return new ParentDaoColumns("Id");
			}
		}	

				
        public ParentDaoColumns Id
        {
            get
            {
                return new ParentDaoColumns("Id");
            }
        }
        public ParentDaoColumns Uuid
        {
            get
            {
                return new ParentDaoColumns("Uuid");
            }
        }
        public ParentDaoColumns Name
        {
            get
            {
                return new ParentDaoColumns("Name");
            }
        }
        public ParentDaoColumns Created
        {
            get
            {
                return new ParentDaoColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ParentDao);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}