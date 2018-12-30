using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AccessControl.Data.Dao
{
    public class PermissionSpecificationColumns: QueryFilter<PermissionSpecificationColumns>, IFilterToken
    {
        public PermissionSpecificationColumns() { }
        public PermissionSpecificationColumns(string columnName)
            : base(columnName)
        { }
		
		public PermissionSpecificationColumns KeyColumn
		{
			get
			{
				return new PermissionSpecificationColumns("Id");
			}
		}	

				
        public PermissionSpecificationColumns Id
        {
            get
            {
                return new PermissionSpecificationColumns("Id");
            }
        }
        public PermissionSpecificationColumns Uuid
        {
            get
            {
                return new PermissionSpecificationColumns("Uuid");
            }
        }
        public PermissionSpecificationColumns Cuid
        {
            get
            {
                return new PermissionSpecificationColumns("Cuid");
            }
        }
        public PermissionSpecificationColumns RoleIdentifier
        {
            get
            {
                return new PermissionSpecificationColumns("RoleIdentifier");
            }
        }
        public PermissionSpecificationColumns UserIdentifier
        {
            get
            {
                return new PermissionSpecificationColumns("UserIdentifier");
            }
        }
        public PermissionSpecificationColumns CreatedBy
        {
            get
            {
                return new PermissionSpecificationColumns("CreatedBy");
            }
        }
        public PermissionSpecificationColumns ModifiedBy
        {
            get
            {
                return new PermissionSpecificationColumns("ModifiedBy");
            }
        }
        public PermissionSpecificationColumns Modified
        {
            get
            {
                return new PermissionSpecificationColumns("Modified");
            }
        }
        public PermissionSpecificationColumns Deleted
        {
            get
            {
                return new PermissionSpecificationColumns("Deleted");
            }
        }
        public PermissionSpecificationColumns Created
        {
            get
            {
                return new PermissionSpecificationColumns("Created");
            }
        }

        public PermissionSpecificationColumns ResourceId
        {
            get
            {
                return new PermissionSpecificationColumns("ResourceId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(PermissionSpecification);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}