using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class DaoSubItemColumns: QueryFilter<DaoSubItemColumns>, IFilterToken
    {
        public DaoSubItemColumns() { }
        public DaoSubItemColumns(string columnName)
            : base(columnName)
        { }
		
		public DaoSubItemColumns KeyColumn
		{
			get
			{
				return new DaoSubItemColumns("Id");
			}
		}	

				
        public DaoSubItemColumns Id
        {
            get
            {
                return new DaoSubItemColumns("Id");
            }
        }
        public DaoSubItemColumns Uuid
        {
            get
            {
                return new DaoSubItemColumns("Uuid");
            }
        }
        public DaoSubItemColumns Name
        {
            get
            {
                return new DaoSubItemColumns("Name");
            }
        }
        public DaoSubItemColumns Created
        {
            get
            {
                return new DaoSubItemColumns("Created");
            }
        }

        public DaoSubItemColumns DaoBaseItemId
        {
            get
            {
                return new DaoSubItemColumns("DaoBaseItemId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DaoSubItem);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}