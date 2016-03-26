using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public static class Delete
    {
        public static void From<T>(Database db = null) where T : Dao, new()
        {
            GetSqlStringBuilder<T>(db).Delete(Dao.TableName(typeof(T))).Execute(db);
        }

        public static void From<T>(QueryFilter filter, Database db = null) where T: Dao, new()
        {
            SqlStringBuilder sql = GetSqlStringBuilder<T>(db);
            sql.Delete(Dao.TableName(typeof(T))).Where(filter).Execute(db);
        }

        private static SqlStringBuilder GetSqlStringBuilder<T>(Database db) where T : Dao, new()
        {
            db = db ?? Db.For<T>();
            SqlStringBuilder sql = db.GetService<SqlStringBuilder>();
            return sql;
        }
    }
}
