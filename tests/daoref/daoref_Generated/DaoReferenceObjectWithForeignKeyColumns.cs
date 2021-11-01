using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class DaoReferenceObjectWithForeignKeyColumns: QueryFilter<DaoReferenceObjectWithForeignKeyColumns>, IFilterToken
    {
        public DaoReferenceObjectWithForeignKeyColumns() { }
        public DaoReferenceObjectWithForeignKeyColumns(string columnName)
            : base(columnName)
        { }
		
		public DaoReferenceObjectWithForeignKeyColumns KeyColumn
		{
			get
			{
				return new DaoReferenceObjectWithForeignKeyColumns("Id");
			}
		}	

        public DaoReferenceObjectWithForeignKeyColumns Id
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Id");
            }
        }
        public DaoReferenceObjectWithForeignKeyColumns Uuid
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Uuid");
            }
        }
        public DaoReferenceObjectWithForeignKeyColumns Cuid
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Cuid");
            }
        }
        public DaoReferenceObjectWithForeignKeyColumns Name
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Name");
            }
        }


        public DaoReferenceObjectWithForeignKeyColumns DaoReferenceObjectId
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("DaoReferenceObjectId");
            }
        }

		protected internal Type TableType
		{
			get
			{
				return typeof(DaoReferenceObjectWithForeignKey);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}