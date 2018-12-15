/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class OleDbQuerySet : QuerySet
    {
        public OleDbQuerySet() : base() { }
        public override SqlStringBuilder Insert(Dao instance)
        {
            ResultDataTables.Add(new InsertResult(instance, "Id"));
            return Insert(Dao.TableName(instance.GetType()), instance.GetNewAssignValues()).Id().Go();
        }

        public override void Execute(Database db)
        {
            DbConnection conn = db.GetOpenDbConnection();
            DbParameter[] parameters = db.GetParameters(this);
            DataSet = db.GetDataSetFromSql(this, CommandType.Text, false, conn, null, parameters);
            OnExecuted(db);
            Reset();
            db.ReleaseConnection(conn);
        }
    }
}
