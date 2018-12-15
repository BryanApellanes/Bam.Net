using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public interface IDataTypeTranslator
    {
        Type TypeFromDbDataType(string dbDataType);
        Type TypeFromDataType(DataTypes dataType);
        DataTypes TranslateDataType(string sqlDataType);
    }
}
