using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class EnsureSchemaResult
    {
        public Database Database { get; set; }
        public string SchemaName { get; set; }
        public EnsureSchemaStatus Status { get; set; }

        public override bool Equals(object obj)
        {
            EnsureSchemaResult compareTo = obj as EnsureSchemaResult;
            if(compareTo != null)
            {
                return Database.Equals(compareTo.Database) && SchemaName.Equals(compareTo.SchemaName);
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode(Database, SchemaName);
        }
    }
}
