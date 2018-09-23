using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class WriteEventColumns: QueryFilter<WriteEventColumns>, IFilterToken
    {
        public WriteEventColumns() { }
        public WriteEventColumns(string columnName)
            : base(columnName)
        { }
		
		public WriteEventColumns KeyColumn
		{
			get
			{
				return new WriteEventColumns("Id");
			}
		}	

				
        public WriteEventColumns Id
        {
            get
            {
                return new WriteEventColumns("Id");
            }
        }
        public WriteEventColumns Uuid
        {
            get
            {
                return new WriteEventColumns("Uuid");
            }
        }
        public WriteEventColumns Cuid
        {
            get
            {
                return new WriteEventColumns("Cuid");
            }
        }
        public WriteEventColumns TypeNamespace
        {
            get
            {
                return new WriteEventColumns("TypeNamespace");
            }
        }
        public WriteEventColumns Type
        {
            get
            {
                return new WriteEventColumns("Type");
            }
        }
        public WriteEventColumns InstanceCuid
        {
            get
            {
                return new WriteEventColumns("InstanceCuid");
            }
        }
        public WriteEventColumns Created
        {
            get
            {
                return new WriteEventColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(WriteEvent);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}