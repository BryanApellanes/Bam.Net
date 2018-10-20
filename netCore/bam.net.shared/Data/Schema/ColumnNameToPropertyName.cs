/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public class ColumnNameToPropertyName
    {
        public static implicit operator string(ColumnNameToPropertyName cntpn)
        {
            return cntpn.PropertyName;
        }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string PropertyName { get; set; }
    }
}
