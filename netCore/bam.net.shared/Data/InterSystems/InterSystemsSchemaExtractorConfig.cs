using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Intersystems
{
    public class InterSystemsSchemaExtractorConfig
    {
        public const string ConnectionStringConfigKey = "CacheConnectionString";

        public string TableNameFilter { get; set; }
        public string ConnectionString { get; set; }
    }
}
