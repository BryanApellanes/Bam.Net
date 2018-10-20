using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public partial class Database
    {
        public virtual IEnumerable<dynamic> ExecuteDynamicReader(SqlStringBuilder sqlStatement, Action<DbDataReader> onExecuted = null)
        {
            return ExecuteDynamicReader(sqlStatement.ToString(), GetParameters(sqlStatement), null, true, onExecuted);
        }

        public virtual IEnumerable<dynamic> ExecuteDynamicReader(string sqlStatement, object dbParameters, Action<DbDataReader> onExecuted = null)
        {
            return ExecuteDynamicReader(sqlStatement, dbParameters.ToDbParameters(this).ToArray(), null, true, onExecuted);
        }

        public virtual IEnumerable<dynamic> ExecuteDynamicReader(string sqlStatement, DbParameter[] dbParameters, out DbConnection conn, Action<DbDataReader> onExecuted = null)
        {
            conn = GetOpenDbConnection();
            return ExecuteDynamicReader(sqlStatement, dbParameters, conn, false, onExecuted);
        }

        public virtual IEnumerable<dynamic> ExecuteDynamicReader(string sqlStatement, DbParameter[] dbParameters, DbConnection conn = null, bool closeConnection = true, Action<DbDataReader> onDataReaderExecuted = null)
        {
            return ExecuteDynamicReader(sqlStatement, CommandType.Text, dbParameters, conn ?? GetOpenDbConnection(), closeConnection, onDataReaderExecuted);
        }

        public virtual IEnumerable<dynamic> ExecuteDynamicReader(string sqlStatement, CommandType commandType, DbParameter[] dbParameters, DbConnection conn, bool closeConnection = true, Action<DbDataReader> onDataReaderExecuted = null)
        {
            DbDataReader reader = ExecuteReader(sqlStatement, commandType, dbParameters, conn);
            onDataReaderExecuted = onDataReaderExecuted ?? ((dr) => { });
            if (reader.HasRows)
            {
                List<string> columnNames = GetColumnNames(reader);
                Type type = sqlStatement.Sha256().BuildDynamicType("Database.ExecuteDynamicReader", columnNames.ToArray());
                while (reader.Read())
                {
                    object next = type.Construct();
                    columnNames.Each(new { Value = next, Reader = reader }, (ctx, cn) =>
                    {
                        ReflectionExtensions.Property(ctx.Value, cn, ctx.Reader[cn]);
                    });
                    yield return next;
                }
            }
            if (closeConnection)
            {
                ReleaseConnection(conn);
            }
            onDataReaderExecuted(reader);
            yield break;
        }

        public IEnumerable<dynamic> Query(string sqlQuery, DbParameter[] dbParameters, string typeName = null)
        {
            DataTable table = GetDataTable(sqlQuery, dbParameters);
            typeName = typeName ?? new SqlInfo(sqlQuery, dbParameters).ToInfoString();
            return table.ToDynamicEnumerable(typeName);
        }
    }
}
