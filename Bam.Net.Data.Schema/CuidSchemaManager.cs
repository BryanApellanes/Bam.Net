using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Schema
{
    public class CuidSchemaManager: UuidSchemaManager
    {
        public CuidSchemaManager(bool autoSave = true) : base(autoSave)
        {
            PreColumnAugmentations.Add(new AddColumnAugmentation { ColumnName = "Cuid", DataType = DataTypes.String, AllowNull = true });
        }
    }
}
