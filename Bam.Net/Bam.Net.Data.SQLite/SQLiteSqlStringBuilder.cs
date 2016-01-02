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
            ForeignKeyAttribute[] fks = GetForeignKeys(daoType);

            Builder.AppendFormat("CREATE TABLE [{0}] ({1}{2}{3})",
                daoType.GetCustomAttributeOfType<TableAttribute>().TableName,
                columns.ToDelimited(c =>
                {
                    if (c is KeyColumnAttribute)
                    {
                        return string.Format(KeyColumnFormat, string.Format("{0} INTEGER", c.Name)); 
                    }
                    else
                    {
                        string columnDef = GetColumnDefinition(c);
                        return columnDef;
                    }
                }),
                fks.Length > 0 ? ",": "",
                fks.ToDelimited(f =>
                {
                    return string.Format("FOREIGN KEY({0}) REFERENCES [{1}]({2})", f.Name, f.ReferencedTable, f.ReferencedKey);
                }));
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

        protected override void WriteDropTable(Type daoType)
        {
            TableAttribute attr = null;
            if (daoType.HasCustomAttributeOfType<TableAttribute>(out attr))
            {
                Builder.AppendFormat("DROP TABLE IF EXISTS {0}\r\n", attr.TableName);
                Go();
            }
        }

        public override SqlStringBuilder Id(string idAs)
        {
            StringBuilder.AppendFormat("{0}SELECT last_insert_rowid() AS {1}", this.GoText, idAs);
            return this;
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
