using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class EchoTypeTableNameProvider : ITypeTableNameProvider
    {
        public string GetTableName(Type type)
        {
            return type.Name;
        }
    }
}
