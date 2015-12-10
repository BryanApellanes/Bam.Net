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
    public class DaoReferenceObjectWithForeignKeyColumns: QueryFilter<DaoReferenceObjectWithForeignKeyColumns>, IFilterToken
    {
        public DaoReferenceObjectWithForeignKeyColumns() { }
        public DaoReferenceObjectWithForeignKeyColumns(string columnName)
            : base(columnName)
        { }

        public DaoReferenceObjectWithForeignKeyColumns Id
        {
            get
            {
                return new DaoReferenceObjectWithForeignKeyColumns("Id");
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

		public string Operator { get; set; }

        public override string ToString()
        {
            return this.Operator;
        }
	}
}