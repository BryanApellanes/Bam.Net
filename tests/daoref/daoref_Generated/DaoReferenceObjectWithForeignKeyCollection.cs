using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Bam.Net.Data;

namespace Bam.Net.DaoRef
{
    public class DaoReferenceObjectWithForeignKeyCollection: DaoCollection<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey>
    { 
		public DaoReferenceObjectWithForeignKeyCollection(){}
		public DaoReferenceObjectWithForeignKeyCollection(Database db, DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(db, table, dao, rc) { }
		public DaoReferenceObjectWithForeignKeyCollection(DataTable table, Bam.Net.Data.Dao dao = null, string rc = null) : base(table, dao, rc) { }
		public DaoReferenceObjectWithForeignKeyCollection(Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey> q, Bam.Net.Data.Dao dao = null, string rc = null) : base(q, dao, rc) { }
		public DaoReferenceObjectWithForeignKeyCollection(Database db, Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey> q, bool load) : base(db, q, load) { }
		public DaoReferenceObjectWithForeignKeyCollection(Query<DaoReferenceObjectWithForeignKeyColumns, DaoReferenceObjectWithForeignKey> q, bool load) : base(q, load) { }
    }
}