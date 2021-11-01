using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class DaoReferenceObjectColumns: QueryFilter<DaoReferenceObjectColumns>, IFilterToken
    {
        public DaoReferenceObjectColumns() { }
        public DaoReferenceObjectColumns(string columnName)
            : base(columnName)
        { }
		
		public DaoReferenceObjectColumns KeyColumn
		{
			get
			{
				return new DaoReferenceObjectColumns("Id");
			}
		}	

        public DaoReferenceObjectColumns Id
        {
            get
            {
                return new DaoReferenceObjectColumns("Id");
            }
        }
        public DaoReferenceObjectColumns Uuid
        {
            get
            {
                return new DaoReferenceObjectColumns("Uuid");
            }
        }
        public DaoReferenceObjectColumns Cuid
        {
            get
            {
                return new DaoReferenceObjectColumns("Cuid");
            }
        }
        public DaoReferenceObjectColumns IntProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("IntProperty");
            }
        }
        public DaoReferenceObjectColumns DecimalProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DecimalProperty");
            }
        }
        public DaoReferenceObjectColumns LongProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("LongProperty");
            }
        }
        public DaoReferenceObjectColumns ULongProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("ULongProperty");
            }
        }
        public DaoReferenceObjectColumns DateTimeProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DateTimeProperty");
            }
        }
        public DaoReferenceObjectColumns BoolProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("BoolProperty");
            }
        }
        public DaoReferenceObjectColumns ByteArrayProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("ByteArrayProperty");
            }
        }
        public DaoReferenceObjectColumns StringProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("StringProperty");
            }
        }



		protected internal Type TableType
		{
			get
			{
				return typeof(DaoReferenceObject);
			}
		}

		public string Operator { get; set; }

        public override string ToString()
        {
            return base.ColumnName;
        }
	}
}