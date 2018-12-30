using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.Schema;

namespace Bam.Net.Breve
{
    public class BreveTypes
    {
        static Dictionary<Languages, Dictionary<DataTypes, string>> _typeMap;
        static BreveTypes()
        {
            _typeMap = new Dictionary<Languages, Dictionary<DataTypes, string>>
            {
                {
                    Languages.java, new Dictionary<DataTypes, string>
                    {
                        { DataTypes.Default, "Object" },
                        { DataTypes.Boolean, "boolean" },
                        { DataTypes.Int, "int" },
                        { DataTypes.UInt, "uint" },
                        { DataTypes.Long, "long" },
                        { DataTypes.ULong, "ulong" },
                        { DataTypes.Decimal, "double" },
                        { DataTypes.String, "String" },
                        { DataTypes.ByteArray, "byte[]" },
                        { DataTypes.DateTime, "Date" }
                    }
                },
                {
                    Languages.cs, new Dictionary<DataTypes, string>
                    {
                        { DataTypes.Default, "object" },
                        { DataTypes.Boolean, "bool" },
                        { DataTypes.Int, "int" },
                        { DataTypes.UInt, "uint" },
                        { DataTypes.Long, "long" },
                        { DataTypes.ULong, "ulong" },
                        { DataTypes.Decimal, "decimal" },
                        { DataTypes.String, "string" },
                        { DataTypes.ByteArray, "byte[]" },
                        { DataTypes.DateTime, "DateTime" }
                    }
                }
            };
        }
        public static BreveTypes Map
        {
            get
            {
                return new BreveTypes();
            }
        }
        public string this[Languages language, DataTypes type]
        {
            get
            {
                return _typeMap[language][type];
            }
        }
    }
}
