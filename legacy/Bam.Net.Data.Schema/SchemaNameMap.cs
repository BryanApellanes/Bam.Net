/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    /// <summary>
    /// A class used to de-sillyfy database naming conventions
    /// </summary>
    public class SchemaNameMap
    {
        public SchemaNameMap()
        {
            this.TableNamesToClassNames = new List<TableNameToClassName>();
            this.ColumnNamesToPropertyNames = new List<ColumnNameToPropertyName>();
        }

        public List<TableNameToClassName> TableNamesToClassNames { get; set; }
        public List<ColumnNameToPropertyName> ColumnNamesToPropertyNames { get; set; }

        public static SchemaNameMap Load(string filePath)
        {
            return filePath.FromJsonFile<SchemaNameMap>();
        }
        public void Save(string filePath)
        {
            this.ToJsonFile(filePath);
        }

        public string GetTableName(string className)
        {
            TableNameToClassName lookup = TableNamesToClassNames.FirstOrDefault(t => t.ClassName.Equals(className));
            if (lookup != null)
            {
                return lookup.TableName;
            }
            return className;
        }

        public string GetClassName(string tableName)
        {
            TableNameToClassName lookup = TableNamesToClassNames.FirstOrDefault(t => t.TableName.Equals(tableName));
            if (lookup != null)
            {
                return lookup.TableName;
            }
            return tableName;
        }
        public string GetColumnName(string className, string propertyName)
        {
            string tableName = GetTableName(className);
            ColumnNameToPropertyName lookup = ColumnNamesToPropertyNames.FirstOrDefault(c => c.TableName.Equals(tableName) && c.PropertyName.Equals(propertyName));
            if (lookup != null)
            {
                return lookup.ColumnName;
            }

            return propertyName;
        }

        public string GetPropertyName(string tableName, string columName)
        {
            ColumnNameToPropertyName lookup = ColumnNamesToPropertyNames.FirstOrDefault(c => c.TableName.Equals(tableName) && c.ColumnName.Equals(columName));
            if (lookup != null)
            {
                return lookup.PropertyName;
            }

            return columName;
        }

        public void Set(TableNameToClassName tableNameToClassName)
        {
            Remove(tableNameToClassName, true);
            Remove(tableNameToClassName, false);
            TableNamesToClassNames.Add(tableNameToClassName);
        }
        public void Set(ColumnNameToPropertyName columnNameToPropertyName)
        {
            Remove(columnNameToPropertyName, true);
            Remove(columnNameToPropertyName, false);
            ColumnNamesToPropertyNames.Add(columnNameToPropertyName);
        }

        private void Remove(ColumnNameToPropertyName columnNameToPropertyName, bool favorTable)
        {
            // favoring single return queries for readability; despite performance hit
            ColumnNameToPropertyName toRemove = favorTable ? ColumnNamesToPropertyNames.FirstOrDefault(c => c.TableName.Equals(columnNameToPropertyName.TableName) && c.ColumnName.Equals(columnNameToPropertyName.ColumnName)) : ColumnNamesToPropertyNames.FirstOrDefault(c => c.TableName.Equals(columnNameToPropertyName.TableName) && c.PropertyName.Equals(columnNameToPropertyName.PropertyName));
            if (toRemove != null)
            {
                ColumnNamesToPropertyNames.Remove(toRemove);
            }
        }

        private void Remove(TableNameToClassName tableNameToClassName, bool favorTable)
        {
            // favoring single return queries for readability; despite performance hit
            TableNameToClassName toRemove = favorTable ? TableNamesToClassNames.FirstOrDefault(c => c.TableName.Equals(tableNameToClassName.TableName)) : TableNamesToClassNames.FirstOrDefault(c => c.ClassName.Equals(tableNameToClassName.ClassName));
            if (toRemove != null)
            {
                TableNamesToClassNames.Remove(toRemove);
            }
        }
    }
}
