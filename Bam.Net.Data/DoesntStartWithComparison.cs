/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public class DoesntStartWithComparison : Comparison
    {
        public DoesntStartWithComparison(string columnName, object value, int? number = null)
            : base(columnName, "NOT LIKE", "{0}%"._Format(value), number)
        { }
    }
}