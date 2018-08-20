using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IObjectReader: IFileSystemPersister
    {   
        T Read<T>(long id);
        T Read<T>(ulong id);
        object Read(Type type, long id);
        object Read(Type type, ulong id);
        T Read<T>(string uuid);
        object Read(Type type, string uuid);
        T ReadByHash<T>(string hash);
        object ReadByHash(Type type, string hash);

        T ReadProperty<T>(PropertyInfo prop, long id);
        T ReadProperty<T>(PropertyInfo prop, ulong id);
        T ReadProperty<T>(PropertyInfo prop, string uuid);
        T ReadPropertyVersion<T>(PropertyInfo prop, string hash, int version);
    }
}
