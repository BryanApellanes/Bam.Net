/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Bam.Net.Data.Schema;
using Bam.Net.Incubation;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.MySql;

namespace Bam.Net.Data
{
    public class MySqlSqlStringBuilder : SchemaWriter
    {
        public MySqlSqlStringBuilder()
            : base()
        {
            GoText = ";\r\n";
            CreateTableFormat = "CREATE TABLE {0} ({1})";
            AddForeignKeyColumnFormat = "ALTER TABLE {0} ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES {3} ({4})";
            TableNameFormatter = (s) => "`{0}`"._Format(s);
            ColumnNameFormatter = (s) => s;
        }

        public override SqlStringBuilder Id(string idAs)
        {
            Builder.AppendFormat("{0}SELECT last_insert_id() AS {1}", this.GoText, idAs);
            return this;
        }

        public override void Reset()
        {
            base.Reset();
            this.GoText = ";\r\n";
        }

        public static void Register(Incubator incubator)
        {
            MySqlSqlStringBuilder builder = new MySqlSqlStringBuilder();
            incubator.Set(typeof(SqlStringBuilder), builder);
            incubator.Set<SqlStringBuilder>(builder);
        }

        public override string GetKeyColumnDefinition(KeyColumnAttribute keyColumn)
        {
            return string.Format("{0} AUTO_INCREMENT PRIMARY KEY ", GetColumnDefinition(keyColumn));
        }

        public override string GetColumnDefinition(ColumnAttribute column)
        {
            string max = string.Format("({0})", column.MaxLength);
            string type = column.DbDataType.ToLowerInvariant();

            if (type.Equals("bigint") ||
                type.Equals("int") ||
                type.Equals("bit"))
            {
                type = "INT";
                max = "";
            }
            else if (type.Equals("decimal"))
            {
                max = string.Format("({0}, 2)", column.MaxLength);
            }
            else if (type.Equals("datetime"))
            {
                max = "(6)";
            }
            else if (type.Equals("varbinary"))
            {
                type = "BLOB";
                max = "";
            }

            return string.Format("{0} {1}{2}{3}", column.Name, type, max, column.AllowNull ? "" : " NOT NULL");
        }

        public override SqlStringBuilder Where(IQueryFilter filter)
        {
            WhereFormat where = MySqlFormatProvider.GetWhereFormat(filter, StringBuilder, NextNumber);
            NextNumber = where.NextNumber;
            this.parameters.AddRange(where.Parameters);
            return this;
        }

        public override SqlStringBuilder Update(string tableName, params AssignValue[] values)
        {
            Builder.AppendFormat("UPDATE {0} ", TableNameFormatter(tableName));
            SetFormat set = MySqlFormatProvider.GetSetFormat(tableName, StringBuilder, NextNumber, values);
            NextNumber = set.NextNumber;
            this.parameters.AddRange(set.Parameters);
            return this;
        }

        protected override void WriteDropForeignKeys(Type daoType)
        {
            if (daoType.HasCustomAttributeOfType(out TableAttribute table))
            {
                PropertyInfo[] properties = daoType.GetProperties();
                foreach (PropertyInfo prop in properties)
                {
                    if (prop.HasCustomAttributeOfType(out ForeignKeyAttribute fk))
                    {
                        Builder.AppendFormat("ALTER TABLE `{0}` DROP FOREIGN KEY {1}", table.TableName, fk.ForeignKeyName);
                        Go();
                    }
                }
            }
        }

        public override SchemaWriter WriteDropTable(string tableName)
        {
            Builder.AppendFormat("DROP TABLE IF EXISTS `{0}`", tableName);
            Go();
            return this;
        }
    }
}
