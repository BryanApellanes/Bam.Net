using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data
{
    public interface IDataTypeTranslator
    {
        DataTypes TranslateDataType(string sqlDataType);
    }
}
