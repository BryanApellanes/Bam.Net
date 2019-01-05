using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Bam.Net.Data
{
    public partial class SqlStringBuilder // fx
    {        
        public IEnumerable<dynamic> ExecuteDynamicReader(Database db)
        {
            if (!string.IsNullOrWhiteSpace(this))
            {
                return db.ExecuteDynamicReader(this, (dr) => OnExecuted(db));
            }
            return new List<dynamic>();
        }
    }
}
