using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
    public static class DatabaseExtensions
    {
        public static StringBuilder WriteSchemaScript(this Database database, SchemaDefinitionCreateResult schemaDefinitionCreateResult)
        {
            SchemaDefinition schemaDefinition = schemaDefinitionCreateResult.SchemaDefinition;
            SchemaWriter writer = database.GetService<SchemaWriter>();
            StringBuilder sql = new StringBuilder();
            schemaDefinition.Tables.Each(table =>
            {
                List<string> columnSegments = new List<string>();
                table.Columns.Each(column =>
                {
                    if (column.Key)
                    {
                        KeyColumnAttribute keyAttr = GetColumnAttribute<KeyColumnAttribute>(column);
                        columnSegments.Add(writer.GetKeyColumnDefinition(keyAttr));
                    }
                    else
                    {
                        ColumnAttribute colAttr = GetColumnAttribute<ColumnAttribute>(column);
                        columnSegments.Add(writer.GetColumnDefinition(colAttr));
                    }
                });
                sql.AppendFormat(writer.CreateTableFormat, table.Name, string.Join(", ", columnSegments.ToArray()));                
                sql.AppendLine(writer.GoText);
            });

            schemaDefinition.ForeignKeys.Each(fk =>
            {
                sql.AppendFormat(writer.AddForeignKeyColumnFormat, fk.TableName, fk.ReferenceName, fk.Name, fk.ReferencedTable, fk.ReferencedKey);
                sql.AppendLine(writer.GoText);                                             
            });

            return sql;
        }

        private static T GetColumnAttribute<T>(Column column) where T : ColumnAttribute, new()
        {
            return new T { Name = column.Name, AllowNull = column.AllowNull, DbDataType = column.DbDataType, MaxLength = column.MaxLength, Table = column.TableName };
        }
    }
}
