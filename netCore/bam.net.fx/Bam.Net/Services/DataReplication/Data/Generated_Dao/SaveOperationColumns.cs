using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Services.DataReplication.Data.Dao
{
    public class SaveOperationColumns: QueryFilter<SaveOperationColumns>, IFilterToken
    {
        public SaveOperationColumns() { }
        public SaveOperationColumns(string columnName)
            : base(columnName)
        { }
		
		public SaveOperationColumns KeyColumn
		{
			get
			{
				return new SaveOperationColumns("Id");
			}
		}	

				
        public SaveOperationColumns Id
        {
            get
            {
                return new SaveOperationColumns("Id");
            }
        }
        public SaveOperationColumns Uuid
        {
            get
            {
                return new SaveOperationColumns("Uuid");
            }
        }
        public SaveOperationColumns Cuid
        {
            get
            {
                return new SaveOperationColumns("Cuid");
            }
        }
        public SaveOperationColumns TypeNamespace
        {
            get
            {
                return new SaveOperationColumns("TypeNamespace");
            }
        }
        public SaveOperationColumns TypeName
        {
            get
            {
                return new SaveOperationColumns("TypeName");
            }
        }
        public SaveOperationColumns CreatedBy
        {
            get
            {
                return new SaveOperationColumns("CreatedBy");
            }
        }
        public SaveOperationColumns ModifiedBy
        {
            get
            {
                return new SaveOperationColumns("ModifiedBy");
            }
        }
        public SaveOperationColumns Modified
        {
            get
            {
                return new SaveOperationColumns("Modified");
            }
        }
        public SaveOperationColumns Deleted
        {
            get
            {
                return new SaveOperationColumns("Deleted");
            }
        }
        public SaveOperationColumns Created
        {
            get
            {
                return new SaveOperationColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(SaveOperation);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}