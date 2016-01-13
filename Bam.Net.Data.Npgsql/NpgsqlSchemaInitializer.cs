/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bryan.Apellanes.Data
{
    public class NpgsqlSchemaInitializer: SchemaInitializer
    {
        public NpgsqlSchemaInitializer() : base() { }

        public NpgsqlSchemaInitializer(string schemaContextAssemblyQaulifiedName)
            : base(schemaContextAssemblyQaulifiedName, typeof(NpgsqlRegistrarCaller).AssemblyQualifiedName)
        { }

        public NpgsqlSchemaInitializer(Type schemaContextType)
            : base(schemaContextType, typeof(NpgsqlRegistrarCaller))
        { }
    }
}
