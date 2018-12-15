using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using Bam.Net.Data.Schema;

namespace Bam.Net.Data.Repositories
{
    public class PropertyColumn
    {
        public PropertyColumn(PropertyInfo property)
        {
            PropertyInfo = property;
            string columnName = property.Name.LettersOnly();
            ColumnAttribute attr = null;
            if(property.HasCustomAttributeOfType<ColumnAttribute>(out attr))
            {
                columnName = attr.Name;
            }
            Column = new Column(columnName, TypeSchemaGenerator.GetColumnDataType(property));
            if(attr != null)
            {
                Column.AllowNull = attr.AllowNull;
            }
        }

        [JsonIgnore]
        [Exclude]
        public PropertyInfo PropertyInfo { get; }
        public Column Column { get; set; }
        public override string ToString()
        {
            return $"{{{Column.Name}}}";
        }
    }
}
