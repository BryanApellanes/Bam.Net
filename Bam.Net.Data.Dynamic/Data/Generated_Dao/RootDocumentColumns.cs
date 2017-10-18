using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.Data.Dynamic.Data.Dao
{
    public class RootDocumentColumns: QueryFilter<RootDocumentColumns>, IFilterToken
    {
        public RootDocumentColumns() { }
        public RootDocumentColumns(string columnName)
            : base(columnName)
        { }
		
		public RootDocumentColumns KeyColumn
		{
			get
			{
				return new RootDocumentColumns("Id");
			}
		}	

				
        public RootDocumentColumns Id
        {
            get
            {
                return new RootDocumentColumns("Id");
            }
        }
        public RootDocumentColumns Uuid
        {
            get
            {
                return new RootDocumentColumns("Uuid");
            }
        }
        public RootDocumentColumns Cuid
        {
            get
            {
                return new RootDocumentColumns("Cuid");
            }
        }
        public RootDocumentColumns FileName
        {
            get
            {
                return new RootDocumentColumns("FileName");
            }
        }
        public RootDocumentColumns Content
        {
            get
            {
                return new RootDocumentColumns("Content");
            }
        }
        public RootDocumentColumns ContentHash
        {
            get
            {
                return new RootDocumentColumns("ContentHash");
            }
        }
        public RootDocumentColumns Created
        {
            get
            {
                return new RootDocumentColumns("Created");
            }
        }


		protected internal Type TableType
		{
			get
			{
				return typeof(RootDocument);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}