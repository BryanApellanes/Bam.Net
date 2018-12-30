using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IHashedObjectReader
    {
        T ReadByHash<T>(string hash);
        object ReadByHash(Type type, string hash);
    }
}
