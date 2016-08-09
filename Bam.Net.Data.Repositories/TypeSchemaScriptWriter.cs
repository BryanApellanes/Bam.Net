using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
    public class TypeSchemaScriptWriter
    {
        public SchemaDefinitionCreateResult LastSchemaDefinitionCreateResult { get; set; }
        public void CommitSchema(Database database, IEnumerable<Type> types)
        {
            database.ExecuteSql(WriteSchemaScript(database, types));
        }
        public void CommitSchema(Database database, params Type[] types)
        {
            database.ExecuteSql(WriteSchemaScript(database, types));
        }

        public SqlStringBuilder WriteSchemaScript(Database database, IEnumerable<Type> types)
        {
            return WriteSchemaScript(database, types.ToArray());
        }

        /// <summary>
        /// Write and return the sql schema script using a TypeInheritanceSchemaGenerator
        /// </summary>
        /// <param name="database"></param>
        /// <param name="types"></param>
        /// <returns></returns>
        public SqlStringBuilder WriteSchemaScript(Database database, params Type[] types)
        {
            TypeInheritanceSchemaGenerator schemaGenerator = new TypeInheritanceSchemaGenerator();
            schemaGenerator.Types = types;
            return WriteSchemaScript(database, schemaGenerator);
        }

        public SqlStringBuilder WriteSchemaScript(Database database, TypeSchemaGenerator typeSchemaGenerator, SchemaManager schemaManager = null)
        {
            schemaManager = schemaManager ?? new SchemaManager { AutoSave = false };
            typeSchemaGenerator.SchemaManager = schemaManager;
            LastSchemaDefinitionCreateResult = typeSchemaGenerator.CreateSchemaDefinition();
            return WriteSchemaScript(database, LastSchemaDefinitionCreateResult);
        }

        public SqlStringBuilder WriteSchemaScript(Database database, SchemaDefinitionCreateResult schemaDefinitionCreateResult)
        {
            SchemaDefinition schemaDefinition = schemaDefinitionCreateResult.SchemaDefinition;
            SchemaWriter writer = database.GetService<SchemaWriter>();
            IEnumerable<ForeignKeyAttribute> fks = GetForeignKeyAttributes(schemaDefinition);
            
            schemaDefinition.Tables.Each(table =>
            {
                string columnDefinitions = GetColumnDefinitions(table, writer);
                writer.WriteCreateTable(table.Name, columnDefinitions, fks.Where(fk=> fk.Table.Equals(table.Name)).ToArray());
                writer.Go();
            });

            schemaDefinition.ForeignKeys.Each(fk =>
            {
                writer.WriteAddForeignKey(fk.TableName, fk.ReferenceName, fk.Name, fk.ReferencedTable, fk.ReferencedKey);
                writer.Go();
            });

            return writer;
        }

        private static string GetColumnDefinitions(Table table, SchemaWriter writer)
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
            return string.Join(", ", columnSegments.ToArray());
        }

        private static T GetColumnAttribute<T>(Column column) where T : ColumnAttribute, new()
        {
            return new T { Name = column.Name, AllowNull = column.AllowNull, DbDataType = column.DbDataType, MaxLength = column.MaxLength, Table = column.TableName };
        }
        
        private static IEnumerable<ForeignKeyAttribute> GetForeignKeyAttributes(SchemaDefinition schema)
        {
            foreach(ForeignKeyColumn fk in schema.ForeignKeys)
            {
                yield return new ForeignKeyAttribute { Table = fk.TableName, Name = fk.Name, ReferencedTable = fk.ReferencedTable, ReferencedKey = fk.ReferencedKey };
            }
        }
    }
}
