using System;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
    public interface IHasTypeSchemaTempPathProvider
    {
        Func<SchemaDefinition, TypeSchema, string> TypeSchemaTempPathProvider { get; set; }
    }
}