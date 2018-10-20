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
    public class AddColumnAugmentation: SchemaManagerAugmentation
    {
        public string ColumnName { get; set; }
        public DataTypes DataType { get; set; }
        public bool AllowNull { get; set; }
        public override void Execute(string tableName, SchemaManager manager)
        {
            manager.AddColumn(tableName, new Column(ColumnName, DataType, AllowNull));
        }
    }
}
