using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class SchemaTypeModel
    {
        public Type Type { get; set; }
        public string DaoNamespace { get; set; }

        public static SchemaTypeModel FromType(Type type, string daoNamespace)
        {
            return new SchemaTypeModel { Type = type, DaoNamespace = daoNamespace };
        }
    }
}
