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
    public class AddIdKeyColumnAugmentation: AddColumnAugmentation
    {
        public AddIdKeyColumnAugmentation(bool caps = false)
            : base()
        {
            this.ColumnName = caps ? "ID": "Id";
            this.DataType = DataTypes.ULong;
            this.AllowNull = false;
        }
        public override void Execute(string tableName, SchemaManager manager)
        {
            base.Execute(tableName, manager);
            manager.SetKeyColumn(tableName, ColumnName);
        }
    }
}
