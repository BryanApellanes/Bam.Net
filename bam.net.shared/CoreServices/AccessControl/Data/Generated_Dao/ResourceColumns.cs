using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class ResourceColumns: QueryFilter<ResourceColumns>, IFilterToken
    {
        public ResourceColumns() { }
        public ResourceColumns(string columnName)
            : base(columnName)
        { }
		
		public ResourceColumns KeyColumn
		{
			get
			{
				return new ResourceColumns("Id");
			}
		}	

				
        public ResourceColumns Id
        {
            get
            {
                return new ResourceColumns("Id");
            }
        }
        public ResourceColumns Uuid
        {
            get
            {
                return new ResourceColumns("Uuid");
            }
        }
        public ResourceColumns Cuid
        {
            get
            {
                return new ResourceColumns("Cuid");
            }
        }
        public ResourceColumns ParentId
        {
            get
            {
                return new ResourceColumns("ParentId");
            }
        }
        public ResourceColumns Name
        {
            get
            {
                return new ResourceColumns("Name");
            }
        }
        public ResourceColumns FullPath
        {
            get
            {
                return new ResourceColumns("FullPath");
            }
        }
        public ResourceColumns Children
        {
            get
            {
                return new ResourceColumns("Children");
            }
        }
        public ResourceColumns CreatedBy
        {
            get
            {
                return new ResourceColumns("CreatedBy");
            }
        }
        public ResourceColumns ModifiedBy
        {
            get
            {
                return new ResourceColumns("ModifiedBy");
            }
        }
        public ResourceColumns Modified
        {
            get
            {
                return new ResourceColumns("Modified");
            }
        }
        public ResourceColumns Deleted
        {
            get
            {
                return new ResourceColumns("Deleted");
            }
        }
        public ResourceColumns Created
        {
            get
            {
                return new ResourceColumns("Created");
            }
        }

        public ResourceColumns ResourceHostId
        {
            get
            {
                return new ResourceColumns("ResourceHostId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(Resource);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}