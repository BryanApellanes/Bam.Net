using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class DataTypeTranslator : IDataTypeTranslator
    {
        public virtual Type TypeFromDbDataType(string dbDataType)
        {
            return TypeFromDataType(TranslateDataType(dbDataType));
        }

        public virtual Type TypeFromDataType(DataTypes dataType)
        {
            switch (dataType)
            {
                case DataTypes.Default:
                    return typeof(object);
                case DataTypes.Boolean:
                    return typeof(bool);
                case DataTypes.Int:
                    return typeof(int);
                case DataTypes.UInt:
                    return typeof(uint);
                case DataTypes.Long:
                    return typeof(long);
                case DataTypes.ULong:
                    return typeof(ulong);
                case DataTypes.Decimal:
                    return typeof(decimal);
                case DataTypes.String:
                    return typeof(string);
                case DataTypes.ByteArray:
                    return typeof(byte[]);
                case DataTypes.DateTime:
                    return typeof(DateTime);
                default:
                    return typeof(object);
            }
        }

        public virtual DataTypes TranslateDataType(string dbDataType)
        {
            string dataType = dbDataType.ToLowerInvariant();
            switch (dataType)
            {
                case "bigint":
                    return DataTypes.ULong;
                case "binary":
                    return DataTypes.ByteArray;
                case "bit":
                    return DataTypes.Boolean;
                case "blob":
                    return DataTypes.ByteArray;
                case "char":
                    return DataTypes.String;
                case "date":
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "double":
                    return DataTypes.Decimal;
                case "enum":
                    return DataTypes.String;
                case "float":
                    return DataTypes.Decimal;
                case "int":
                    return DataTypes.Int;
                case "smallint":
                    return DataTypes.Int;
                case "text":
                    return DataTypes.String;
                case "time":
                    return DataTypes.DateTime;
                case "timestamp":
                    return DataTypes.String;
                case "tinyblob":
                    return DataTypes.ByteArray;
                case "tinyint":
                    return DataTypes.Int;
                case "tinytext":
                    return DataTypes.String;
                case "varbinary":
                    return DataTypes.ByteArray;
                case "varchar":
                    return DataTypes.String;
                case "year":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }
    }
}
