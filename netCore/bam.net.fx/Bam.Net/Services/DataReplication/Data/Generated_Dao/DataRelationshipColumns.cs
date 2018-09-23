using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class DataRelationshipColumns: QueryFilter<DataRelationshipColumns>, IFilterToken
    {
        public DataRelationshipColumns() { }
        public DataRelationshipColumns(string columnName)
            : base(columnName)
        { }
		
		public DataRelationshipColumns KeyColumn
		{
			get
			{
				return new DataRelationshipColumns("Id");
			}
		}	

				
        public DataRelationshipColumns Id
        {
            get
            {
                return new DataRelationshipColumns("Id");
            }
        }
        public DataRelationshipColumns Uuid
        {
            get
            {
                return new DataRelationshipColumns("Uuid");
            }
        }
        public DataRelationshipColumns Cuid
        {
            get
            {
                return new DataRelationshipColumns("Cuid");
            }
        }
        public DataRelationshipColumns LeftCuid
        {
            get
            {
                return new DataRelationshipColumns("LeftCuid");
            }
        }
        public DataRelationshipColumns RightCuid
        {
            get
            {
                return new DataRelationshipColumns("RightCuid");
            }
        }
        public DataRelationshipColumns RelationshipDescription
        {
            get
            {
                return new DataRelationshipColumns("RelationshipDescription");
            }
        }
        public DataRelationshipColumns Created
        {
            get
            {
                return new DataRelationshipColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(DataRelationship);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}