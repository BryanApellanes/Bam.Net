using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Intersystems
{
    public class InterSystemsFieldDescriptor
    {
        public string CacheID { get; set; }
        public string SqlTableName { get; set; }
        public string SqlFieldName { get; set; }
        public string OdbcType { get; set; }
        public string CacheType { get; set; }
    }
}
