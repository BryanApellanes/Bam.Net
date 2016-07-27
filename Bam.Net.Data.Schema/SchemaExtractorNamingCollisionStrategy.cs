using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public enum SchemaExtractorNamingCollisionStrategy
    {
        Invalid,
        TypeSuffix,
        TypePrefix,
        TrailingUnderscore,
        LeadingUnderscore,
        UnderscoreDelimit,
        Custom
    }
}
