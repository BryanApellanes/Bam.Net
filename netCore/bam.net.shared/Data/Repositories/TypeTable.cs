using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Schema;
using Newtonsoft.Json;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// Used to describe the shape of a database table
    /// given a CLR type
    /// </summary>
    public class TypeTable
    {
        public TypeTable(Type type)
        {
            Type = type;
            PropertyColumns = GetColumns();
        }
        [JsonIgnore]
        [Exclude]
        public Type Type { get; set; }
        public string GetTableName(ITypeTableNameProvider tableNameProvider = null)
        {
            tableNameProvider = tableNameProvider ?? new EchoTypeTableNameProvider();
            return tableNameProvider.GetTableName(Type);
        }
        public PropertyColumn[] PropertyColumns { get; set; }
        private PropertyColumn[] GetColumns()
        {
            return Type.GetProperties().Where(pi => pi.DeclaringType == Type).Select(pi => new PropertyColumn(pi)).ToArray();
        }
    }
}
