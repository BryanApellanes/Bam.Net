using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class HouseParentColumns: QueryFilter<HouseParentColumns>, IFilterToken
    {
        public HouseParentColumns() { }
        public HouseParentColumns(string columnName)
            : base(columnName)
        { }
		
		public HouseParentColumns KeyColumn
		{
			get
			{
				return new HouseParentColumns("Id");
			}
		}	

				
        public HouseParentColumns Id
        {
            get
            {
                return new HouseParentColumns("Id");
            }
        }
        public HouseParentColumns Uuid
        {
            get
            {
                return new HouseParentColumns("Uuid");
            }
        }

        public HouseParentColumns HouseId
        {
            get
            {
                return new HouseParentColumns("HouseId");
            }
        }
        public HouseParentColumns ParentId
        {
            get
            {
                return new HouseParentColumns("ParentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(HouseParent);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}