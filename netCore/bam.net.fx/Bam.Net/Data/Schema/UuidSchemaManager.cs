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
    /// A schema manager that will automatically add an Id
    /// and Uuid column to every table when generating a 
    /// schema and related Data Access Objects from a *.db.js
    /// file.
    /// </summary>
    public class UuidSchemaManager: AutoIdSchemaManager
    {
        public UuidSchemaManager(bool autoSave = true) : base(autoSave)
        {
            PreColumnAugmentations.Add(new AddColumnAugmentation { ColumnName = "Uuid", DataType = DataTypes.String, AllowNull = false });
        }
    }
}
