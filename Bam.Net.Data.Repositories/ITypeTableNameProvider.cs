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
        /// <summary>
        /// When implemented in a derived class gets
        /// the table name for the specified Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        string GetTableName(Type type);
    }
}
