/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace Naizari.Data
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public class DaoForeignKeyColumn: DaoColumn
    {
        public DaoForeignKeyColumn() { }
        public DaoForeignKeyColumn(string columnName, int maxLength, string foreignKeyName, string referencedKey, string referencedTable)
            : base(columnName, maxLength)
        {
            ForeignKeyName = foreignKeyName;
            ReferencedKey = referencedKey;
            ReferencedTable = referencedTable;
        }

        public string ForeignKeyName { get; set; }
        public string ReferencedKey { get; set; }
        public string ReferencedTable { get; set; }
    }
}
