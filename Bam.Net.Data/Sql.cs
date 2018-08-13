﻿using System;
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
        public static void Execute(string path, Database db, object parameters = null)
        {
            Execute(new FileInfo(path), db, parameters);
        }

        public static void Execute(FileInfo file, Database db, object parameters = null)
        {
            Execute(file, db, parameters?.ToDbParameters(db).ToArray() ?? new DbParameter[] { });
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
            return ExecuteSqlFile<T>(file, db, parameters);
        }

        public static IEnumerable<T> Execute<T>(string sql, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSql<T>(sql, db, parameters);
        }

        public static IEnumerable<T> ExecuteSqlFile<T>(this string filePath, Database db, object parameters) where T : class, new()
        {
            return ExecuteSqlFile<T>(filePath, db, parameters?.ToDbParameters(db).ToArray() ?? new DbParameter[] { });
        }

        public static IEnumerable<T> ExecuteSqlFile<T>(this string filePath, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSqlFile<T>(new FileInfo(filePath), db, parameters);
        }

        public static IEnumerable<T> ExecuteSqlFile<T>(this FileInfo file, Database db, params DbParameter[] parameters) where T: class, new()
        {
            return ExecuteSql<T>(file.ReadAllText(), db, parameters ?? new DbParameter[] { });
        }

        public static IEnumerable<T> ExecuteSql<T>(this string sql, Database db, params DbParameter[] parameters) where T : class, new()
        {
            return db.ExecuteReader<T>(sql, parameters ?? new DbParameter[] { });
        }

        public static IEnumerable<dynamic> ExecuteDynamicReaderSqlFile(this FileInfo file, Database db, object parameters = null)
        {
            return ExecuteDynamicReader(file.ReadAllText(), db, parameters?.ToDbParameters(db).ToArray() ?? new DbParameter[] { });
        }

        public static IEnumerable<dynamic> ExecuteDynamicReaderSqlFile(this FileInfo file, Database db, params DbParameter[] parameters)
        {
            return ExecuteDynamicReader(file.ReadAllText(), db, parameters ?? new DbParameter[] { });
        }

        public static IEnumerable<dynamic> ExecuteDynamicReaderSqlFile(this string filePath, Database db, params DbParameter[] parameters)
        {
            return ExecuteDynamicReader(File.ReadAllText(filePath), db, parameters ?? new DbParameter[] { });
        }

        public static IEnumerable<dynamic> ExecuteDynamicReader(this string sql, Database db, object dbParameters = null)
        {
            return ExecuteDynamicReader(sql, db, dbParameters?.ToDbParameters(db).ToArray() ?? new DbParameter[] { });
        }

        public static IEnumerable<dynamic> ExecuteDynamicReader(this string sql, Database db, params DbParameter[] parameters)
        {
            return db.ExecuteDynamicReader(sql, parameters, out DbConnection ignore);
        }
    }
}
