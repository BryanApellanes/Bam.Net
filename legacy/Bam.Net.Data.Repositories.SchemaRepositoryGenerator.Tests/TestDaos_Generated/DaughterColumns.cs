using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class DaughterColumns: QueryFilter<DaughterColumns>, IFilterToken
    {
        public DaughterColumns() { }
        public DaughterColumns(string columnName)
            : base(columnName)
        { }
		
		public DaughterColumns KeyColumn
		{
			get
			{
				return new DaughterColumns("Id");
			}
		}	

				
        public DaughterColumns Id
        {
            get
            {
                return new DaughterColumns("Id");
            }
        }
        public DaughterColumns Uuid
        {
            get
            {
                return new DaughterColumns("Uuid");
            }
        }
        public DaughterColumns Name
        {
            get
            {
                return new DaughterColumns("Name");
            }
        }

        public DaughterColumns ParentId
        {
            get
            {
                return new DaughterColumns("ParentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Daughter);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}