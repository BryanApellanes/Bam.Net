using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    /// <summary>
    /// A TypeTableNameProvider implementation that returns
    /// the name of the specified type with leading and trailing
    /// non letters removed
    /// </summary>
    public class EchoTypeTableNameProvider : ITypeTableNameProvider
    {
        public string GetTableName(Type type)
        {
            return type.Name.TrimNonLetters();
        }
    }
}
