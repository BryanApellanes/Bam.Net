/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class SQLiteSchemaInitializer: SchemaInitializer
    {
        public SQLiteSchemaInitializer() : base() { }

        public SQLiteSchemaInitializer(string schemaContextAssemblyQaulifiedName)
            : base(schemaContextAssemblyQaulifiedName, typeof(SQLiteRegistrarCaller).AssemblyQualifiedName)
        { }

        public SQLiteSchemaInitializer(Type schemaContextType)
            : base(schemaContextType, typeof(SQLiteRegistrarCaller))
        { }
    }
}
