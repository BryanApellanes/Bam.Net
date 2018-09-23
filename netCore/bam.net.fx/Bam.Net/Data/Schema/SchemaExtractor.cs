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
using Bam.Net.Configuration;

namespace Bam.Net.Data.Schema
{
    public abstract class SchemaExtractor : Loggable, ISchemaExtractor, IHasSchemaTempPathProvider
    {
        Dictionary<SchemaExtractorNamingCollisionStrategy, Func<string, string, string, string>> _namingCollisionHandlers = new Dictionary<SchemaExtractorNamingCollisionStrategy, Func<string, string, string, string>>();
        public SchemaExtractor()
        {
            NameMap = new SchemaNameMap();
            NameFormatter = new SchemaNameMapNameFormatter(NameMap);
            SchemaTempPathProvider = sd => RuntimeSettings.AppDataFolder;
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.LeadingUnderscore, (tableName, columnName, propertyName) => $"_{columnName}");
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.TrailingUnderscore, (tableName, columnName, propertyName) => $"{columnName}_");
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.TypePrefix, (tableName, columnName, propertyName) => $"{GetColumnDataType(tableName, columnName).ToString()}{columnName}");
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.TypeSuffix, (tableName, columnName, propertyName) => $"{columnName}{GetColumnDataType(tableName, columnName).ToString()}");
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.UnderscoreDelimit, (tableName, columnName, propertyName) => $"_{columnName}_");
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.Custom, (tableName, columnName, propertyName) => CustomNamingCollisionHandler(tableName, columnName, propertyName));
            _namingCollisionHandlers.Add(SchemaExtractorNamingCollisionStrategy.Invalid, (tableName, columnName, propertyName) =>
            {
                throw new InvalidOperationException("Invalid SchemaExtractorNamingCollisionStrategy specified");
            });
            CustomNamingCollisionHandler = _namingCollisionHandlers[SchemaExtractorNamingCollisionStrategy.TrailingUnderscore];
            SchemaExtractorNamingCollisionStrategy = SchemaExtractorNamingCollisionStrategy.TrailingUnderscore;
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
        public event EventHandler PropertyNameCollisionAvoided;

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

        /// <summary>
        /// A function used to avoid naming collisions on property names and reserved
        /// Dao keywords.  Receives tableName, columnName and propertyName; should return
        /// desired propertyName
        /// </summary>
        public Func<string, string, string, string> CustomNamingCollisionHandler { get; set; }
        public SchemaExtractorNamingCollisionStrategy SchemaExtractorNamingCollisionStrategy { get; set; }
        public SchemaNameMap NameMap { get; set; }
        public INameFormatter NameFormatter { get; set; }

        public Func<SchemaDefinition, string> SchemaTempPathProvider { get; set; }

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
            string propertyName = AvoidCollision(GetPropertyName(tableName, columnName), tableName, columnName);
            propertyName = AvoidNameOfContainingType(propertyName, tableName, columnName);
            NameMap.Set(new ColumnNameToPropertyName { ColumnName = columnName, PropertyName = propertyName, TableName = tableName });
            FireEvent(PropertyNameFormatted, new SchemaExtractorEventArgs { Column = columnName });

            Column column = new Column(columnName, GetColumnDataType(tableName, columnName))
            {
                PropertyName = propertyName,
                DataType = GetColumnDataType(tableName, columnName),
                DbDataType = GetColumnDbDataType(tableName, columnName),
                MaxLength = GetColumnMaxLength(tableName, columnName),
                AllowNull = GetColumnNullable(tableName, columnName)
            };

            return column;
        }

        public virtual SchemaDefinition Extract()
        {
            SchemaManager schemaManager = new SchemaManager
            {
                AutoSave = false,
                SchemaTempPathProvider = SchemaTempPathProvider
            };
            SchemaDefinition result = Extract(schemaManager);
            return result;
        }

        public virtual SchemaDefinition Extract(SchemaManager schemaManager)
        {
            SchemaDefinition result = new SchemaDefinition { Name = GetSchemaName() };
            schemaManager.CurrentSchema = result;

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
                FireEvent(ProcessingForeignComplete, new SchemaExtractorEventArgs { ForeignKeyColumn = fk });
            });
            SaveNameMap(schemaManager);
            SetClassNamesOnColumns(schemaManager);
            return result;
        }

        protected virtual void SaveNameMap(SchemaManager schemaManager)
        {
            NameMap.Save(Path.Combine(RuntimeSettings.AppDataFolder, "{0}_NameMap.json"._Format(schemaManager.CurrentSchema.Name)));
        }

        protected HashSet<string> GetKeyWords()
        {
            return new HashSet<string>(new[] {
                "ProxyTypeProvider",
                "PostConstructActions",
                "Initializer",
                "GlobalInitializer",
                "Database",
                "AutoDeleteChildren",
                "Validator",
                "GlobalValidator",
                "UniqueFilterProvider",
                "IdValue",
                "KeyColumnName",
                "ForceInsert",
                "ForceUpdate",
                "IsNew",
                "ServiceProvider",
                "DataRow",
                "WriteDelete",
                "WriteCommit",
                "WriteUpdate",
                "WriteInsert",
                "Undo",
                "Undelete",
                "ToJsonSafe",
                "GetUniqueFilter",
                "ConnectionName",
                "UnproxyConnection",
                "ProxyConnection",
                "TableName",
                "GetKeyColumnName",
                "RegisterDaoTypes",
                "GetSchemaTypes",
                "OnInitialize",
                "GetHashCode",
                "Equals",
                "ResetChildren",
                "Validate",
                "Save",
                "Commit",
                "Update",
                "Insert",
                "WriteChildDeletes",
                "Delete",
                "PreLoadChildCollections",
                "ToString",
                "GetType",
                "LoadAll",
                "BatchAll",
                "BatchQuery",
                "GetById",
                "GetByUuid",
                "GetByCuid",
                "Query",
                "Where",
                "GetOneWhere",
                "OneWhere",
                "FirstOneWhere",
                "Top",
                "Count",
                "IsEmpty",
                "Filters",
                "Parameters",
                "Where",
                "Parse",
                "Add",
                "StartsWith",
                "EndsWith",
                "Contains",
                "In",
                "And",
                "Or",
                "Equals",
                "GetHashCode",
                "ToString",
                "GetType"

            });

        }

        private string AvoidNameOfContainingType(string propertyName, string tableName, string columnName)
        {
            string className = NameMap.GetClassName(tableName);
            string result = propertyName;
            if (propertyName.Equals(className))
            {
                result = _namingCollisionHandlers[SchemaExtractorNamingCollisionStrategy](propertyName, tableName, columnName);
                FireEvent(PropertyNameCollisionAvoided, new SchemaExtractorEventArgs { Table = tableName, Column = columnName, Property = propertyName });
            }
            return result;
        }

        private string AvoidCollision(string propertyName, string tableName, string columnName)
        {
            string result = propertyName;
            if (GetKeyWords().Contains(propertyName))
            {
                result = _namingCollisionHandlers[SchemaExtractorNamingCollisionStrategy](tableName, columnName, propertyName);
                FireEvent(PropertyNameCollisionAvoided, new SchemaExtractorEventArgs { Table = tableName, Column = columnName, Property = propertyName });
            }
            return result;
        }

        private void SetClassNamesOnColumns(SchemaManager schemaManager)
        {
            schemaManager.CurrentSchema.Tables.Each(table =>
            {
                table.Columns.Each(col =>
                {
                    col.TableClassName = NameMap.GetClassName(table.Name);
                });
            });
        }


    }
}
