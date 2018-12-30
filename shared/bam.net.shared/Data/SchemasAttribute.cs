using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    /// <summary>
    /// Attribute used to mark a database property
    /// with Dao types used to initialize the
    /// schema in the database
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SchemasAttribute: Attribute
    {
        public SchemasAttribute(params Type[] daoSchemaTypes)
        {
            DaoSchemaTypes = daoSchemaTypes;
        }
        /// <summary>
        /// Dao types to use to initialize schemas
        /// </summary>
        public Type[] DaoSchemaTypes { get; set; }
    }
}
