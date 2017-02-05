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

namespace Bam.Net.Data
{
    public class OleDbSqlStringBuilder: SchemaWriter
    {
        public OleDbSqlStringBuilder() : base()
        {
            KeyColumnFormat = "{0} AUTOINCREMENT PRIMARY KEY";
        }

        public static void Register(Incubator incubator)
        {
            OleDbSqlStringBuilder builder = new OleDbSqlStringBuilder();
            incubator.Set(typeof(SqlStringBuilder), builder);
            incubator.Set<SqlStringBuilder>(builder);
        }

        public override string GetKeyColumnDefinition(KeyColumnAttribute keyColumn)
        {
            return string.Format(KeyColumnFormat, keyColumn.Name);
        }

        public override string GetColumnDefinition(ColumnAttribute column)
        {
            string type = TranslateDataType(column.DbDataType.ToLowerInvariant()).ToString();
            if (type.Equals("Decimal"))
            {
                type = "Number";
            }
            else if (type.Equals("Boolean"))
            {
                type = "Bit";
            }
            else if (type.Equals("ByteArray"))
            {
                type = "VarBinary";
            }
            return string.Format("[{0}] {1}{2}", column.Name, type, column.AllowNull ? "" : " NOT NULL");
        }
        public override SchemaWriter WriteDropTable(string tableName)
        {
            Builder.AppendFormat("DROP TABLE [{0}]", tableName);
            Go();
            return this;
        }
        protected override void WriteDropForeignKeys(Type daoType)
        {
            TableAttribute table = null;
            if (daoType.HasCustomAttributeOfType<TableAttribute>(out table))
            {
                PropertyInfo[] properties = daoType.GetProperties();
                foreach (PropertyInfo prop in properties)
                {
                    ForeignKeyAttribute fk = null;
                    if (prop.HasCustomAttributeOfType<ForeignKeyAttribute>(out fk))
                    {
                        Builder.AppendFormat("ALTER TABLE [{0}] DROP CONSTRAINT {1}", table.TableName, fk.ForeignKeyName);
                        Go();
                    }
                }
            }
        }
        protected internal DataTypes TranslateDataType(string sqlDataType)
        {
            switch (sqlDataType)
            {
                case "bigint":
                    return DataTypes.Long;
                case "binary":
                    return DataTypes.ByteArray;
                case "bit":
                    return DataTypes.Boolean;
                case "char":
                    return DataTypes.String;
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "float":
                    return DataTypes.String;
                case "image":
                    return DataTypes.ByteArray;
                case "int":
                    return DataTypes.Int;
                case "money":
                    return DataTypes.Decimal;
                case "nchar":
                    return DataTypes.String;
                case "ntext":
                    return DataTypes.String;
                case "nvarchar":
                    return DataTypes.String;
                case "nvarcharmax":
                    return DataTypes.String;
                case "none":
                    return DataTypes.String;
                case "numeric":
                    return DataTypes.Long;
                case "real":
                    return DataTypes.String;
                case "smalldatetime":
                    return DataTypes.DateTime;
                case "smallint":
                    return DataTypes.Int;
                case "smallmoney":
                    return DataTypes.Decimal;
                case "sysname":
                    return DataTypes.String;
                case "text":
                    return DataTypes.String;
                case "timestamp":
                    return DataTypes.DateTime;
                case "tinyint":
                    return DataTypes.Int;
                case "uniqueidentifier":
                    return DataTypes.String;
                case "userdefineddatatype":
                    return DataTypes.String;
                case "userdefinedtype":
                    return DataTypes.String;
                case "varbinary":
                    return DataTypes.ByteArray;
                case "varbinarymax":
                    return DataTypes.ByteArray;
                case "varchar":
                    return DataTypes.String;
                case "varcharmax":
                    return DataTypes.String;
                case "variant":
                    return DataTypes.String;
                case "xml":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }

    }
}
