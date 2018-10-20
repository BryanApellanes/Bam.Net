using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data.Dao
{
    public class ActiveApiKeyIndexColumns: QueryFilter<ActiveApiKeyIndexColumns>, IFilterToken
    {
        public ActiveApiKeyIndexColumns() { }
        public ActiveApiKeyIndexColumns(string columnName)
            : base(columnName)
        { }
		
		public ActiveApiKeyIndexColumns KeyColumn
		{
			get
			{
				return new ActiveApiKeyIndexColumns("Id");
			}
		}	

				
        public ActiveApiKeyIndexColumns Id
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Id");
            }
        }
        public ActiveApiKeyIndexColumns Uuid
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Uuid");
            }
        }
        public ActiveApiKeyIndexColumns Cuid
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Cuid");
            }
        }
        public ActiveApiKeyIndexColumns ApplicationCuid
        {
            get
            {
                return new ActiveApiKeyIndexColumns("ApplicationCuid");
            }
        }
        public ActiveApiKeyIndexColumns Value
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Value");
            }
        }
        public ActiveApiKeyIndexColumns CreatedBy
        {
            get
            {
                return new ActiveApiKeyIndexColumns("CreatedBy");
            }
        }
        public ActiveApiKeyIndexColumns ModifiedBy
        {
            get
            {
                return new ActiveApiKeyIndexColumns("ModifiedBy");
            }
        }
        public ActiveApiKeyIndexColumns Modified
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Modified");
            }
        }
        public ActiveApiKeyIndexColumns Deleted
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Deleted");
            }
        }
        public ActiveApiKeyIndexColumns Created
        {
            get
            {
                return new ActiveApiKeyIndexColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(ActiveApiKeyIndex);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}