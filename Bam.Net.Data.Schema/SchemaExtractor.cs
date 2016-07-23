/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Logging;
using System.IO;

namespace Bam.Net.Data.Schema
{
    public abstract class SchemaExtractor: Loggable, ISchemaExtractor
    {
        public SchemaExtractor()
        {
            NameFormatter = new EchoNameFormatter();
            NameMap = new SchemaNameMap();
        }

        public Database Database { get; protected set; }

        public event EventHandler ProcessingTable;
        public event EventHandler ProcessingTableComplete;
        public event EventHandler ProcessingColumn;
        public event EventHandler ProcessingColumnComplete;
        public event EventHandler ProcessingForeignKey;
        public event EventHandler ProcessingForeignComplete;

        public event EventHandler ClassNameFormatting;
        public event EventHandler ClassNameFormatted;

        public event EventHandler PropertyNameFormatting;
        public event EventHandler PropertyNameFormatted;

        public abstract string GetSchemaName();
        public abstract string[] GetTableNames();
        public abstract string GetKeyColumnName(string tableName);
        public abstract string[] GetColumnNames(string tableName);
        public abstract DataTypes GetColumnDataType(string tableName, string columnName);
        public abstract string GetColumnDbDataType(string tableName, string columnName);
        public abstract string GetColumnMaxLength(string tableName, string columnName);
        public abstract bool GetColumnNullable(string tableName, string columnName);
        public abstract ForeignKeyColumn[] GetForeignKeyColumns();
        protected abstract void SetConnectionName(string connectionString);

        string _connectionString;
        public virtual string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
                SetConnectionName(value);
            }
        }
        public SchemaNameMap NameMap { get; set; }
        public INameFormatter NameFormatter { get; set; }
        public virtual TableNameToClassName GetClassName(string tableName)
        {
            return new TableNameToClassName { TableName = tableName, ClassName = NameFormatter.FormatClassName(tableName) };
        }

        public virtual ColumnNameToPropertyName GetPropertyName(string tableName, string columnName)
        {
            return new ColumnNameToPropertyName { TableName = tableName, ColumnName = columnName, PropertyName = NameFormatter.FormatPropertyName(tableName, columnName) };
        }

        public Column CreateColumn(string tableName, string columnName)
        {
            FireEvent(PropertyNameFormatting, new SchemaExtractorEventArgs { Column = columnName });
            string propertyName = GetPropertyName(tableName, columnName);
            NameMap.Set(new ColumnNameToPropertyName { ColumnName = columnName, PropertyName = propertyName, TableName = tableName });
            FireEvent(PropertyNameFormatted, new SchemaExtractorEventArgs { Column = columnName });

            Column column = new Column(columnName, GetColumnDataType(tableName, columnName));
            column.PropertyName = propertyName;
            column.DataType = GetColumnDataType(tableName, columnName);
            column.DbDataType = GetColumnDbDataType(tableName, columnName);
            column.MaxLength = GetColumnMaxLength(tableName, columnName);
            column.AllowNull = GetColumnNullable(tableName, columnName);

            return column;
        }

        public virtual SchemaDefinition Extract()
        {
            SchemaManager schemaManager = new SchemaManager();
            schemaManager.SetSchema(GetSchemaName(), false);

            // GetTableNames
            GetTableNames().Each(tableName =>
            {
                FireEvent(ProcessingTable, new SchemaExtractorEventArgs { Table = tableName });
                
                FireEvent(ClassNameFormatting, new SchemaExtractorEventArgs { Table = tableName });
                string className = GetClassName(tableName);
                NameMap.Set(new TableNameToClassName { TableName = tableName, ClassName = className });
                FireEvent(ClassNameFormatted, new SchemaExtractorEventArgs { Table = tableName });

                schemaManager.AddTable(tableName, className);//  add each table
                // GetColumnNames
                GetColumnNames(tableName).Each(columnName =>
                {
                    FireEvent(ProcessingColumn, new SchemaExtractorEventArgs { Column = columnName });
                    //  add each column;                     
                    schemaManager.AddColumn(tableName, CreateColumn(tableName, columnName));
                    FireEvent(ProcessingColumnComplete, new SchemaExtractorEventArgs { Column = columnName });
                });

                string keyColumnName = GetKeyColumnName(tableName);
                if (!string.IsNullOrEmpty(keyColumnName))
                {
                    schemaManager.SetKeyColumn(tableName, keyColumnName);
                }

                FireEvent(ProcessingTableComplete, new SchemaExtractorEventArgs { Table = tableName });
            });

            // GetForeignKeyColumns
            GetForeignKeyColumns().Each(fk =>
            {
                FireEvent(ProcessingForeignKey, new SchemaExtractorEventArgs { ForeignKeyColumn = fk });
                //  set each foreignkey
                schemaManager.SetForeignKey(fk.ReferencedTable, fk.TableName, fk.Name, GetKeyColumnName(fk.ReferencedTable), NameFormatter);
                FireEvent(ProcessingForeignComplete, new SchemaExtractorEventArgs { ForeignKeyColumn = fk});
            });

            NameMap.Save(Path.Combine(this.GetAppDataFolder(), "{0}_NameMap.json"._Format(schemaManager.CurrentSchema.Name)));
            SchemaDefinition result = SetClassNamesOnColumns(schemaManager);
            return result;
        }

        private SchemaDefinition SetClassNamesOnColumns(SchemaManager schemaManager)
        {
            SchemaDefinition result = schemaManager.GetCurrentSchema();
            result.Tables.Each(table =>
            {
                table.Columns.Each(col =>
                {
                    col.TableClassName = NameMap.GetClassName(table.Name);
                });
            });
            return result;
        }
    }
}
