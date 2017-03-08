/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data;

namespace Bam.Net.Data
{
    public class SQLiteSqlStringBuilder: SchemaWriter
    {
        public SQLiteSqlStringBuilder()
        {
            KeyColumnFormat = "{0} PRIMARY KEY AUTOINCREMENT";
        }

        public static void Register(Incubator incubator)
        {
            incubator.Set<SqlStringBuilder>(new SQLiteSqlStringBuilder());

            SQLiteSqlStringBuilder builder = new SQLiteSqlStringBuilder();
            incubator.Set<SQLiteSqlStringBuilder>(builder);
        }

        protected override void WriteCreateTable(Type daoType)
        {
            ColumnAttribute[] columns = GetColumns(daoType);           
            string tableName = Dao.TableName(daoType);
            string columnDefinitions = GetColumnDefinitions(columns);
            WriteCreateTable(tableName, columnDefinitions, GetForeignKeys(daoType));
        }

        public override void WriteCreateTable(string tableName, string columnDefinitions, dynamic[] fks)
        {
            fks = fks ?? new dynamic[] { };
            Builder.AppendFormat("CREATE TABLE [{0}] ({1}{2}{3})",
                            tableName,
                            columnDefinitions,
                            fks.Length > 0 ? "," : "",
                            fks.ToDelimited(f =>
                            {
                                return string.Format("FOREIGN KEY({0}) REFERENCES [{1}]({2})", f.Name, f.ReferencedTable, f.ReferencedKey);
                            }));
            Go();
        }


        protected override void WriteForeignKeys(Type daoType)
        {
            // empty implementation 
            // SQLite can't alter a table to add foreign keys
        }

        protected override void WriteDropForeignKeys(Type daoType)
        {
            // empty implementation
            // SQLite drops Foreign keys implicitly when dropping tables
        }

		protected override void WriteForeignKeys(Assembly daoAssembly, Func<Type, bool> typePredicate = null)
		{
			// empty implementation 
			// SQLite can't alter a table to add foreign keys
		}
        public override void WriteAddForeignKey(string tableName, string nameOfReference, string nameOfColumn, string referencedTable, string referencedKey)
        {
            // empty implementation
            // SQLite can't alter a table to add foreign keys
        }

        public override SchemaWriter WriteDropTable(string tableName)
        {
            Builder.AppendFormat("DROP TABLE IF EXISTS {0}", tableName);
            Go();
            return this;
        }

        public override SqlStringBuilder Id(string idAs)
        {
            StringBuilder.AppendFormat("{0}SELECT last_insert_rowid() AS {1}", this.GoText, idAs);
            return this;
        }

        public override string GetKeyColumnDefinition(KeyColumnAttribute keyColumn)
        {
            return string.Format(KeyColumnFormat, string.Format("{0} INTEGER", keyColumn.Name));
        }

        public override string GetColumnDefinition(ColumnAttribute column)
        {
            string type = column.DbDataType;
            if (type.Equals("Bit"))
            {
                type = "INTEGER"; // sqlite doesn't have a separate Bit/bool
            }
            return string.Format("\"{0}\" {1}{2}", column.Name, type, column.AllowNull ? "" : " NOT NULL");
        }
    }
}
