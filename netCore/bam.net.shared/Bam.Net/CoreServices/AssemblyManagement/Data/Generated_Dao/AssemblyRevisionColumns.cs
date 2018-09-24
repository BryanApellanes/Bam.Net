using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.AssemblyManagement.Data.Dao
{
    public class AssemblyRevisionColumns: QueryFilter<AssemblyRevisionColumns>, IFilterToken
    {
        public AssemblyRevisionColumns() { }
        public AssemblyRevisionColumns(string columnName)
            : base(columnName)
        { }
		
		public AssemblyRevisionColumns KeyColumn
		{
			get
			{
				return new AssemblyRevisionColumns("Id");
			}
		}	

				
        public AssemblyRevisionColumns Id
        {
            get
            {
                return new AssemblyRevisionColumns("Id");
            }
        }
        public AssemblyRevisionColumns Uuid
        {
            get
            {
                return new AssemblyRevisionColumns("Uuid");
            }
        }
        public AssemblyRevisionColumns Cuid
        {
            get
            {
                return new AssemblyRevisionColumns("Cuid");
            }
        }
        public AssemblyRevisionColumns FileName
        {
            get
            {
                return new AssemblyRevisionColumns("FileName");
            }
        }
        public AssemblyRevisionColumns FileHash
        {
            get
            {
                return new AssemblyRevisionColumns("FileHash");
            }
        }
        public AssemblyRevisionColumns Number
        {
            get
            {
                return new AssemblyRevisionColumns("Number");
            }
        }
        public AssemblyRevisionColumns CreatedBy
        {
            get
            {
                return new AssemblyRevisionColumns("CreatedBy");
            }
        }
        public AssemblyRevisionColumns ModifiedBy
        {
            get
            {
                return new AssemblyRevisionColumns("ModifiedBy");
            }
        }
        public AssemblyRevisionColumns Modified
        {
            get
            {
                return new AssemblyRevisionColumns("Modified");
            }
        }
        public AssemblyRevisionColumns Deleted
        {
            get
            {
                return new AssemblyRevisionColumns("Deleted");
            }
        }
        public AssemblyRevisionColumns Created
        {
            get
            {
                return new AssemblyRevisionColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(AssemblyRevision);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}