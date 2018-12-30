using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Repositories.Tests.ClrTypes.Daos
{
    public class SonColumns: QueryFilter<SonColumns>, IFilterToken
    {
        public SonColumns() { }
        public SonColumns(string columnName)
            : base(columnName)
        { }
		
		public SonColumns KeyColumn
		{
			get
			{
				return new SonColumns("Id");
			}
		}	

				
        public SonColumns Id
        {
            get
            {
                return new SonColumns("Id");
            }
        }
        public SonColumns Uuid
        {
            get
            {
                return new SonColumns("Uuid");
            }
        }
        public SonColumns Name
        {
            get
            {
                return new SonColumns("Name");
            }
        }

        public SonColumns ParentId
        {
            get
            {
                return new SonColumns("ParentId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Son);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}