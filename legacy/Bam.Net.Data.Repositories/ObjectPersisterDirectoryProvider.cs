using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
    public class ObjectPersisterDirectoryProvider : IObjectPersisterDirectoryProvider
    {
        public ObjectPersisterDirectoryProvider(string rootDirectory)
        {
            RootDirectory = rootDirectory;
        }

        public string RootDirectory { get; set; }

        public DirectoryInfo GetTypeDirectory(Type type)
        {
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(RootDirectory, type.Name));
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }

        public DirectoryInfo GetPropertyDirectory(PropertyInfo prop)
        {
            return GetPropertyDirectory(prop.DeclaringType, prop);
        }

        public DirectoryInfo GetPropertyDirectory(Type type, PropertyInfo prop)
        {
            DirectoryInfo typeDirectory = GetTypeDirectory(type);
            DirectoryInfo dir = new DirectoryInfo(Path.Combine(typeDirectory.FullName, prop.Name));
            if (!dir.Exists)
            {
                dir.Create();
            }
            return dir;
        }
    }
}
