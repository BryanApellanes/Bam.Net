using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public class MsSqlDataTypeTranslator : DataTypeTranslator
    {
        public override DataTypes TranslateDataType(string sqlDataType)
        {
            switch (sqlDataType.ToLowerInvariant())
            {
                case "bigint":
                    return DataTypes.ULong;
                case "binary":
                    return DataTypes.ByteArray;
                case "bit":
                    return DataTypes.Boolean;
                case "char":
                    return DataTypes.String;
                case "datetime":
                    return DataTypes.DateTime;
                case "decimal":
                    return DataTypes.Decimal;
                case "float":
                    return DataTypes.Decimal;
                case "image":
                    return DataTypes.ByteArray;
                case "int":
                    return DataTypes.Int;
                case "money":
                    return DataTypes.Decimal;
                case "nchar":
                    return DataTypes.String;
                case "ntext":
                    return DataTypes.String;
                case "nvarchar":
                    return DataTypes.String;
                case "nvarcharmax":
                    return DataTypes.String;
                case "none":
                    return DataTypes.String;
                case "numeric":
                    return DataTypes.ULong;
                case "real":
                    return DataTypes.Decimal;
                case "smalldatetime":
                    return DataTypes.DateTime;
                case "smallint":
                    return DataTypes.Int;
                case "smallmoney":
                    return DataTypes.Decimal;
                case "sysname":
                    return DataTypes.String;
                case "text":
                    return DataTypes.String;
                case "timestamp":
                    return DataTypes.DateTime;
                case "tinyint":
                    return DataTypes.Int;
                case "uniqueidentifier":
                    return DataTypes.String;
                case "userdefineddatatype":
                    return DataTypes.String;
                case "userdefinedtype":
                    return DataTypes.String;
                case "varbinary":
                    return DataTypes.ByteArray;
                case "varbinarymax":
                    return DataTypes.ByteArray;
                case "varchar":
                    return DataTypes.String;
                case "varcharmax":
                    return DataTypes.String;
                case "variant":
                    return DataTypes.String;
                case "xml":
                    return DataTypes.String;
                default:
                    return DataTypes.String;
            }
        }
    }
}
