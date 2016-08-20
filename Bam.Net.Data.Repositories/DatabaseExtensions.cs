using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public static class DatabaseExtensions
    {
        public static SqlStringBuilder WriteSchemaScript(this Database db, SchemaDefinitionCreateResult schemaInfo)
        {
            TypeSchemaScriptWriter writer = new TypeSchemaScriptWriter();
            return writer.WriteSchemaScript(db, schemaInfo);
        }

        public static void CommitSchema(this Database db, SchemaDefinitionCreateResult schemaInfo)
        {
            db.ExecuteSql(WriteSchemaScript(db, schemaInfo));
        }
    }
}
