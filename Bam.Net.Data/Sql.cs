using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public static class Sql
    { 
        public static void Execute(FileInfo file, Database db, object parameters)
        {
            Execute(file, db, parameters.ToDbParameters(db));
        }

        public static void Execute(FileInfo file, Database db, params DbParameter[] parameters)
        {
            Execute<object>(file, db, parameters);
        }

        public static void Execute(string file, Database db, params DbParameter[] parameters)
        {
            Execute<object>(file, db, parameters);
        }

        public static IEnumerable<T> Execute<T>(FileInfo file, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSqlFile<T>(file, db);
        }

        public static IEnumerable<T> Execute<T>(string sql, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSql<T>(sql, db);
        }

        public static IEnumerable<T> ExecuteSqlFile<T>(this string filePath, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSqlFile<T>(new FileInfo(filePath), db);
        }

        public static IEnumerable<T> ExecuteSqlFile<T>(this FileInfo file, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSql<T>(file.ReadAllText(), db);
        }

        public static IEnumerable<T> ExecuteSql<T>(this string sql, Database db, params DbParameter[] parameters) where T : class, new()
        {
            return db.ExecuteReader<T>(sql);
        }
    }
}
