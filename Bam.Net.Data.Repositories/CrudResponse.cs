﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class CrudResponse
    {
        public string CxName { get; set; }
        public bool Success { get; set; }
        public object Dao { get; set; } // json
        public string Message { get; set; }
    }
}
