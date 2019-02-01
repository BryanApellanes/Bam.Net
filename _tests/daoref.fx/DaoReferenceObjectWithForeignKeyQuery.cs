/*
	This file was generated and should not be modified directly
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class DaoReferenceObjectWithForeignKeyQuery: Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>
    { 
		public DaoReferenceObjectWithForeignKeyQuery(){}
		public DaoReferenceObjectWithForeignKeyQuery(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }
		public DaoReferenceObjectWithForeignKeyQuery(Func<DaoReferenceObjectWithForeignKeyColumns, QueryFilter<DaoReferenceObjectWithForeignKeyColumns>> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null, Database db = null) : base(where, orderBy, db) { }		
		public DaoReferenceObjectWithForeignKeyQuery(Delegate where, Database db = null) : base(where, db) { }
		
        public static DaoReferenceObjectWithForeignKeyQuery Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where)
        {
            return Where(where, null, null);
        }

        public static DaoReferenceObjectWithForeignKeyQuery Where(WhereDelegate<DaoReferenceObjectWithForeignKeyColumns> where, OrderBy<DaoReferenceObjectWithForeignKeyColumns> orderBy = null, Database db = null)
        {
            return new DaoReferenceObjectWithForeignKeyQuery(where, orderBy, db);
        }

		public DaoReferenceObjectWithForeignKeyCollection Execute()
		{
			return new DaoReferenceObjectWithForeignKeyCollection(this, true);
		}
    }
}