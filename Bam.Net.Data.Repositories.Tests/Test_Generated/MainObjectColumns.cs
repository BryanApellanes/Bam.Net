using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests
{
    public class MainObjectColumns: QueryFilter<MainObjectColumns>, IFilterToken
    {
        public MainObjectColumns() { }
        public MainObjectColumns(string columnName)
            : base(columnName)
        { }
		
		public MainObjectColumns KeyColumn
		{
			get
			{
				return new MainObjectColumns("Id");
			}
		}	

				
        public MainObjectColumns Id
        {
            get
            {
                return new MainObjectColumns("Id");
            }
        }
        public MainObjectColumns Uuid
        {
            get
            {
                return new MainObjectColumns("Uuid");
            }
        }
        public MainObjectColumns Cuid
        {
            get
            {
                return new MainObjectColumns("Cuid");
            }
        }
        public MainObjectColumns Created
        {
            get
            {
                return new MainObjectColumns("Created");
            }
        }
        public MainObjectColumns Name
        {
            get
            {
                return new MainObjectColumns("Name");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(MainObject);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}