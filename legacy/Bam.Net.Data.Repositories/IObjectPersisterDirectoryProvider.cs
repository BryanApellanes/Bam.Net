using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public interface IObjectPersisterDirectoryProvider
    {
        string RootDirectory { get; set; }
        DirectoryInfo GetTypeDirectory(Type type);
        DirectoryInfo GetPropertyDirectory(PropertyInfo prop);
        DirectoryInfo GetPropertyDirectory(Type type, PropertyInfo prop);
    }
}
