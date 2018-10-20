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
    public class OracleSchemaInitializer: SchemaInitializer
    {
        public OracleSchemaInitializer() : base() { }

        public OracleSchemaInitializer(string schemaContextAssemblyQaulifiedName)
            : base(schemaContextAssemblyQaulifiedName, typeof(OracleRegistrarCaller).AssemblyQualifiedName)
        { }

        public OracleSchemaInitializer(Type schemaContextType)
            : base(schemaContextType, typeof(OracleRegistrarCaller))
        { }
    }
}
