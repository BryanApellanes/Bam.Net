/*
	Copyright © Bryan Apellanes 2015  
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

		public DaoReferenceObjectWithForeignKeyCollection Execute()
		{
			return new DaoReferenceObjectWithForeignKeyCollection(this, true);
		}
    }
}