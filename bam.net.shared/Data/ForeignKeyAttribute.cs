/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class ForeignKeyAttribute: ColumnAttribute
    {
        public ForeignKeyAttribute()
            : base()
        {
            this.Suffix = string.Empty;
			this.AllowNull = true;
        }

        public string ForeignKeyName
        {
            get
            {
                return string.Format("FK_{0}_{1}{2}", Table, ReferencedTable, Suffix);
            }
        }

        public string ReferencedKey { get; set; }
        public string ReferencedTable { get; set; }

        public string Suffix { get; set; }
    }
}
