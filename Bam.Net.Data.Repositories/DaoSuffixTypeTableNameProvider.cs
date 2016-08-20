using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A table name provider that returns the type name
    /// suffixed with "Dao"
    /// </summary>
    public class DaoSuffixTypeTableNameProvider: ITypeTableNameProvider
    {
        public string GetTableName(Type type)
        {
            return "{0}Dao"._Format(type.Name.TrimNonLetters());
        }
    }
}
