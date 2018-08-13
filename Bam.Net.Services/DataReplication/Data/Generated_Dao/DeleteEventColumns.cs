using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DeleteEventColumns: QueryFilter<DeleteEventColumns>, IFilterToken
    {
        public DeleteEventColumns() { }
        public DeleteEventColumns(string columnName)
            : base(columnName)
        { }
		
		public DeleteEventColumns KeyColumn
		{
			get
			{
				return new DeleteEventColumns("Id");
			}
		}	

				
        public DeleteEventColumns Id
        {
            get
            {
                return new DeleteEventColumns("Id");
            }
        }
        public DeleteEventColumns Uuid
        {
            get
            {
                return new DeleteEventColumns("Uuid");
            }
        }
        public DeleteEventColumns Cuid
        {
            get
            {
                return new DeleteEventColumns("Cuid");
            }
        }
        public DeleteEventColumns TypeNamespace
        {
            get
            {
                return new DeleteEventColumns("TypeNamespace");
            }
        }
        public DeleteEventColumns Type
        {
            get
            {
                return new DeleteEventColumns("Type");
            }
        }
        public DeleteEventColumns InstanceCuid
        {
            get
            {
                return new DeleteEventColumns("InstanceCuid");
            }
        }
        public DeleteEventColumns Created
        {
            get
            {
                return new DeleteEventColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DeleteEvent);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}