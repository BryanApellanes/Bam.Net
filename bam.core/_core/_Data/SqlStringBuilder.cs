using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Bam.Net.Data
{
    public partial class SqlStringBuilder // core
    {
        public IEnumerable<dynamic> ExecuteDynamicReader(Database db)
        {
            throw new NotImplementedException($"{nameof(ExecuteDynamicReader)} not implemented on this platform");
        }
    }
}
