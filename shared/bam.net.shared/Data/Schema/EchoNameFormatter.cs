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
    public class EchoNameFormatter: INameFormatter
    {
        public string FormatClassName(string tableName)
        {
            return tableName;
        }

        public string FormatPropertyName(string tableName, string columnName)
        {
            return columnName;
        }
    }
}
