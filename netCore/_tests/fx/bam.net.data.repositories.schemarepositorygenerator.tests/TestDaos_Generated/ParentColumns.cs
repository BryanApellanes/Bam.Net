using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class ParentColumns: QueryFilter<ParentColumns>, IFilterToken
    {
        public ParentColumns() { }
        public ParentColumns(string columnName)
            : base(columnName)
        { }
		
		public ParentColumns KeyColumn
		{
			get
			{
				return new ParentColumns("Id");
			}
		}	

				
        public ParentColumns Id
        {
            get
            {
                return new ParentColumns("Id");
            }
        }
        public ParentColumns Uuid
        {
            get
            {
                return new ParentColumns("Uuid");
            }
        }
        public ParentColumns Name
        {
            get
            {
                return new ParentColumns("Name");
            }
        }
        public ParentColumns Created
        {
            get
            {
                return new ParentColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(Parent);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}