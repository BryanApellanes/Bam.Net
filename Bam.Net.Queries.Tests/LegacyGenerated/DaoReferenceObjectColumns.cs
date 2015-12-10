/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Text;
using Bam.Net.Data;
using Bam.Net.Data;

namespace Bam.Net.DaoReferenceObjects.Data
{
    public class DaoReferenceObjectColumns: QueryFilter<DaoReferenceObjectColumns>, IFilterToken
    {
        public DaoReferenceObjectColumns() { }
        public DaoReferenceObjectColumns(string columnName)
            : base(columnName)
        { }

        public DaoReferenceObjectColumns Id
        {
            get
            {
                return new DaoReferenceObjectColumns("Id");
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
        public DaoReferenceObjectColumns GuidProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("GuidProperty");
            }
        }
        public DaoReferenceObjectColumns DoubleProperty
        {
            get
            {
                return new DaoReferenceObjectColumns("DoubleProperty");
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


		public string Operator { get; set; }

        public override string ToString()
        {
            return this.Operator;
        }
	}
}