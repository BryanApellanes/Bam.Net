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
    /// <summary>
    /// A Schema Manager augmentation that will 
    /// add a Created and Modified column and 
    /// optionally a ModifiedBy column.
    /// </summary>
    public class AddAuditColumnsAugmentation : SchemaManagerAugmentation
    {
        public AddAuditColumnsAugmentation()
        {

        }

        public bool IncludeModifiedBy { get; set; }
		public bool IncludeCreatedBy { get; set; }
        public override void Execute(string tableName, SchemaManager manager)
        {
            manager.AddColumn(tableName, new Column("Created", DataTypes.DateTime, false));
            manager.AddColumn(tableName, new Column("Modified", DataTypes.DateTime, false));
            if (IncludeModifiedBy)
            {
                manager.AddColumn(tableName, new Column("ModifiedBy", DataTypes.String, false));
            }
			if (IncludeCreatedBy)
			{
				manager.AddColumn(tableName, new Column("CreatedBy", DataTypes.String, false));
			}
        }
    }
}
