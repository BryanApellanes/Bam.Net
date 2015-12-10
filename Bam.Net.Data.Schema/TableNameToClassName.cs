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
    public class TableNameToClassName
    {
        public static implicit operator string(TableNameToClassName tntcn)
        {
            return tntcn.ClassName;
        }
        public string TableName { get; set; }
        public string ClassName { get; set; }
    }
}
