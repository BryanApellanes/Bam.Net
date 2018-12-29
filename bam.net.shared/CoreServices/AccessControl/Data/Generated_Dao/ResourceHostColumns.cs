using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class ResourceHostColumns: QueryFilter<ResourceHostColumns>, IFilterToken
    {
        public ResourceHostColumns() { }
        public ResourceHostColumns(string columnName)
            : base(columnName)
        { }
		
		public ResourceHostColumns KeyColumn
		{
			get
			{
				return new ResourceHostColumns("Id");
			}
		}	

				
        public ResourceHostColumns Id
        {
            get
            {
                return new ResourceHostColumns("Id");
            }
        }
        public ResourceHostColumns Uuid
        {
            get
            {
                return new ResourceHostColumns("Uuid");
            }
        }
        public ResourceHostColumns Cuid
        {
            get
            {
                return new ResourceHostColumns("Cuid");
            }
        }
        public ResourceHostColumns Name
        {
            get
            {
                return new ResourceHostColumns("Name");
            }
        }
        public ResourceHostColumns CreatedBy
        {
            get
            {
                return new ResourceHostColumns("CreatedBy");
            }
        }
        public ResourceHostColumns ModifiedBy
        {
            get
            {
                return new ResourceHostColumns("ModifiedBy");
            }
        }
        public ResourceHostColumns Modified
        {
            get
            {
                return new ResourceHostColumns("Modified");
            }
        }
        public ResourceHostColumns Deleted
        {
            get
            {
                return new ResourceHostColumns("Deleted");
            }
        }
        public ResourceHostColumns Created
        {
            get
            {
                return new ResourceHostColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ResourceHost);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}