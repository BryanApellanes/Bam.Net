using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public static class Delete
    {
        public static SqlStringBuilder From<T>(Database db = null) where T : Dao, new()
        {
            return GetSqlStringBuilder<T>(db).Delete(Dao.TableName(typeof(T)));
        }

        public static SqlStringBuilder From<T>(QueryFilter filter, Database db = null) where T: Dao, new()
        {
            SqlStringBuilder sql = GetSqlStringBuilder<T>(db);
            return sql.Delete(Dao.TableName(typeof(T))).Where(filter);
        }

        private static SqlStringBuilder GetSqlStringBuilder<T>(Database db) where T : Dao, new()
        {
            db = db ?? Db.For<T>();
            SqlStringBuilder sql = db.GetService<SqlStringBuilder>();
            return sql;
        }
    }
}
