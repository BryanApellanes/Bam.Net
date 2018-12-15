using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class TypeInheritanceDescriptor
    {
        public TypeInheritanceDescriptor() { }
        public TypeInheritanceDescriptor(Type type)
        {
            Type = type;
            RootType = type;
            Chain = new List<TypeTable>();
            Chain.Add(new TypeTable(type));
            Type baseType = type.BaseType;
            while(baseType != typeof(object))
            {
                RootType = baseType;
                Chain.Add(new TypeTable(baseType));
                baseType = baseType.BaseType;                
            }
        }
        public Type Type { get; set; }
        public Type RootType { get; set; }
        public List<TypeTable> Chain { get; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            int tabCount = 0;
            Chain.Each(typeTable =>
            {
                tabCount.Times(num => builder.Append("\t"));
                builder.AppendLine(typeTable.GetTableName());
                typeTable.PropertyColumns.Each(propertyColumn =>
                {
                    builder.Append("\t");
                    tabCount.Times(num => builder.Append("\t-"));
                    builder.AppendLine(propertyColumn.Column.Name);
                });
                tabCount++;
            });

            return builder.ToString();
        }
    }
}
