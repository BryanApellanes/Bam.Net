using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.FirebirdSql
{
    public class FirebirdSqlSchemaExtractor : SchemaExtractor
    {
        public FirebirdSqlSchemaExtractor(FirebirdSqlDatabase database)
        {
            Database = database;
            ConnectionString = database.ConnectionString;
        }
        public override DataTypes GetColumnDataType(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public override string GetColumnDbDataType(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public override string GetColumnMaxLength(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetColumnNames(string tableName)
        {
            throw new NotImplementedException();
        }

        public override bool GetColumnNullable(string tableName, string columnName)
        {
            throw new NotImplementedException();
        }

        public override ForeignKeyColumn[] GetForeignKeyColumns()
        {
            throw new NotImplementedException();
        }

        public override string GetKeyColumnName(string tableName)
        {
            throw new NotImplementedException();
        }

        public override string GetSchemaName()
        {
            throw new NotImplementedException();
        }

        public override string[] GetTableNames()
        {
            throw new NotImplementedException();
        }

        protected override void SetConnectionName(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
