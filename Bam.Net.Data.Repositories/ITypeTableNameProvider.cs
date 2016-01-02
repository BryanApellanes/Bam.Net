using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// An interface to be implemented by types that provide
    /// Type to table name translation
    /// </summary>
    public interface ITypeTableNameProvider
    {
        string GetTableName(Type type);
    }
}
