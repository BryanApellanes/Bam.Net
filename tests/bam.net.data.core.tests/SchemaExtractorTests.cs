using System;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Data.Tests
{
    [Serializable]
    public class SchemaExtractorTests: CommandLineTool
    {
        // TODO: finish this
        [UnitTest]
        public void CanGetTableColumnTypes()
        {
            string columnTypeQuery = @"SELECT *
FROM information_schema.columns
WHERE table_schema = '{0}'
  AND table_name   = '{1}'";
        }

        [UnitTest]
        public void CanGetForeignKeys()
        {
            string fkQuery = @"SELECT
    tc.table_schema, 
    tc.constraint_name, 
    tc.table_name, 
    kcu.column_name, 
    ccu.table_schema AS foreign_table_schema,
    ccu.table_name AS foreign_table_name,
    ccu.column_name AS foreign_column_name 
FROM 
    information_schema.table_constraints AS tc 
    JOIN information_schema.key_column_usage AS kcu
      ON tc.constraint_name = kcu.constraint_name
      AND tc.table_schema = kcu.table_schema
    JOIN information_schema.constraint_column_usage AS ccu
      ON ccu.constraint_name = tc.constraint_name
      AND ccu.table_schema = tc.table_schema
WHERE tc.constraint_type = 'FOREIGN KEY' AND tc.table_name='{0}';";
        }

        [UnitTest]
        public void ShowConfigPath()
        {
            OutLine(Config.Current.File.FullName);
            //Pause();
        }
    }
}