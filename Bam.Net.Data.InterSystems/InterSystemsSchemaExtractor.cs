using Bam.Net.Data.Schema;
using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Intersystems
{
    public class InterSystemsSchemaExtractor : SchemaExtractor
    {
        InterSystemsSchemaExtractorConfig _config;
        InterSystemsDatabase _cacheDatabase;
        List<InterSystemsFieldDescriptor> _fieldDescriptors;
        object _fieldDescriptorLock = new object();

        public InterSystemsSchemaExtractor(InterSystemsSchemaExtractorConfig config)
        {
            _config = config;
            _cacheDatabase = new InterSystemsDatabase(config.ConnectionString);
        }

        #region Don't look at me, I'm hideous
        private const string FieldDescriptorQuery =
@"SELECT cc.ID as CacheID, cc.SqlTableName, cp.SqlFieldName, cc.OdbcType, cp.Type as CacheType
  FROM %dictionary.compiledclass cc JOIN %dictionary.compiledproperty cp
  ON cc.ID = cp.parent
  WHERE
  cc.ID LIKE '{TableNameFilter}' AND ClassType = 'persistent'";
        #endregion

        protected List<InterSystemsFieldDescriptor> FieldDescriptors
        {
            get
            {
                return _fieldDescriptorLock.DoubleCheckLock(ref _fieldDescriptors, () => _cacheDatabase.ExecuteReader<InterSystemsFieldDescriptor>(FieldDescriptorQuery.NamedFormat(_config), new CacheParameter[] { }).ToList());
            }
        }

        protected List<string> TableNames
        {
            get
            {
                return FieldDescriptors.Select(fd => fd.SqlTableName).ToList();
            }
        }

        Dictionary<string, List<InterSystemsFieldDescriptor>> _fieldDescriptorsByTable;
        object _fieldDescriptorsByTableLock = new object();
        protected Dictionary<string, List<InterSystemsFieldDescriptor>> FieldDescriptorsByTable
        {
            get
            {
                return _fieldDescriptorsByTableLock.DoubleCheckLock(ref _fieldDescriptorsByTable, () =>
                {
                    Dictionary<string, List<InterSystemsFieldDescriptor>> result = new Dictionary<string, List<InterSystemsFieldDescriptor>>();
                    foreach (InterSystemsFieldDescriptor fieldDescriptor in FieldDescriptors)
                    {
                        if (!result.ContainsKey(fieldDescriptor.SqlTableName))
                        {
                            result.Add(fieldDescriptor.SqlTableName, new List<InterSystemsFieldDescriptor>());
                        }

                        result[fieldDescriptor.SqlTableName].Add(fieldDescriptor);
                    }
                    return result;
                });
            }
        }

        private SchemaNameMap SetSchemaNameMap()
        {
            NameMap = new SchemaNameMap();
            foreach(string tableName in FieldDescriptorsByTable.Keys)
            {
                NameMap.Set(new TableNameToClassName { TableName = tableName, ClassName = tableName.TrimNonLetters() });
                foreach(InterSystemsFieldDescriptor fieldDescriptor in FieldDescriptorsByTable[tableName])
                {
                    NameMap.Set(new ColumnNameToPropertyName { TableName = tableName, ColumnName = fieldDescriptor.SqlFieldName, PropertyName = fieldDescriptor.SqlFieldName.TrimNonLetters() });
                }
            }
            return NameMap;
        }

        public override SchemaDefinition Extract()
        {
            NameFormatter = new SchemaNameMapNameFormatter(SetSchemaNameMap());
            SchemaManager schemaManager = new SchemaManager
            {
                AutoSave = false,
                SchemaTempPathProvider = SchemaTempPathProvider
            };
            SchemaDefinition result = Extract(schemaManager);
            result.Tables.Each(t => t.AddColumn("ID", DataTypes.String, true));
            return result;
        }

        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            return TranslateDataType(GetColumnDbDataType(tableName, columnName).ToLowerInvariant());
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            InterSystemsFieldDescriptor cfd = FieldDescriptorsByTable[tableName].Where(f => f.SqlFieldName.Equals(columnName)).FirstOrDefault();
            if(cfd == null || cfd.OdbcType.Equals("varchar", StringComparison.InvariantCultureIgnoreCase))
            {
                return "VARCHAR";
            }
            if(cfd.CacheType.EndsWith(".integer", StringComparison.InvariantCultureIgnoreCase))
            {
                return "INTEGER";
            }
            if(cfd.CacheType.EndsWith(".boolean", StringComparison.InvariantCultureIgnoreCase))
            {
                return "BOOLEAN";
            }
            return "VARCHAR";
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            return string.Empty;
        }

        public override string[] GetColumnNames(string tableName)
        {
            List<string> results = new List<string>();
            foreach(InterSystemsFieldDescriptor fieldDescriptor in FieldDescriptorsByTable[tableName])
            {
                results.Add(fieldDescriptor.SqlFieldName);
            }
            return results.ToArray();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            return true;
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            // TODO: handle foreign keys by looking at the CacheType
            return new ForeignKeyColumn[] { };
        }

        public override string GetKeyColumnName(string tableName)
        {
            return string.Empty;
        }

        public override string GetSchemaName()
        {
            return string.Empty;
        }

        public override string[] GetTableNames()
        {
            HashSet<string> results = new HashSet<string>();
            foreach(InterSystemsFieldDescriptor fd in FieldDescriptors)
            {
                results.Add(fd.SqlTableName);
            }
            return results.ToArray();
        }

        protected override void SetConnectionName(string connectionString)
        {
            // not used
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
                case "boolean":
                    return DataTypes.Boolean;
                case "char":
                    return DataTypes.String;
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "float":
                    return DataTypes.Decimal;
                case "image":
                    return DataTypes.ByteArray;
                case "int":
                case "integer":
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
                    return DataTypes.Decimal;
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
