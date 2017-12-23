using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class ReplicationOperationColumns: QueryFilter<ReplicationOperationColumns>, IFilterToken
    {
        public ReplicationOperationColumns() { }
        public ReplicationOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public ReplicationOperationColumns KeyColumn
		{
			get
			{
				return new ReplicationOperationColumns("Id");
			}
		}	

				
        public ReplicationOperationColumns Id
        {
            get
            {
                return new ReplicationOperationColumns("Id");
            }
        }
        public ReplicationOperationColumns Uuid
        {
            get
            {
                return new ReplicationOperationColumns("Uuid");
            }
        }
        public ReplicationOperationColumns Cuid
        {
            get
            {
                return new ReplicationOperationColumns("Cuid");
            }
        }
        public ReplicationOperationColumns SourceHost
        {
            get
            {
                return new ReplicationOperationColumns("SourceHost");
            }
        }
        public ReplicationOperationColumns SourcePort
        {
            get
            {
                return new ReplicationOperationColumns("SourcePort");
            }
        }
        public ReplicationOperationColumns DestinationHost
        {
            get
            {
                return new ReplicationOperationColumns("DestinationHost");
            }
        }
        public ReplicationOperationColumns DestinationPort
        {
            get
            {
                return new ReplicationOperationColumns("DestinationPort");
            }
        }
        public ReplicationOperationColumns BatchSize
        {
            get
            {
                return new ReplicationOperationColumns("BatchSize");
            }
        }
        public ReplicationOperationColumns FromCuid
        {
            get
            {
                return new ReplicationOperationColumns("FromCuid");
            }
        }
        public ReplicationOperationColumns TypeNamespace
        {
            get
            {
                return new ReplicationOperationColumns("TypeNamespace");
            }
        }
        public ReplicationOperationColumns TypeName
        {
            get
            {
                return new ReplicationOperationColumns("TypeName");
            }
        }
        public ReplicationOperationColumns CreatedBy
        {
            get
            {
                return new ReplicationOperationColumns("CreatedBy");
            }
        }
        public ReplicationOperationColumns ModifiedBy
        {
            get
            {
                return new ReplicationOperationColumns("ModifiedBy");
            }
        }
        public ReplicationOperationColumns Modified
        {
            get
            {
                return new ReplicationOperationColumns("Modified");
            }
        }
        public ReplicationOperationColumns Deleted
        {
            get
            {
                return new ReplicationOperationColumns("Deleted");
            }
        }
        public ReplicationOperationColumns Created
        {
            get
            {
                return new ReplicationOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ReplicationOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}