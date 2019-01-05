﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public static partial class Sql
    {
        public static IEnumerable<dynamic> ExecuteDynamicReader(this string sql, Database db, params DbParameter[] parameters)
        {
            return db.ExecuteDynamicReader(sql, parameters, out DbConnection ignore);
        }
    }
}
