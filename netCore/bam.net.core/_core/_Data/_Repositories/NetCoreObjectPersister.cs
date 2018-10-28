using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Net.Data.Repositories
{
    public class NetCoreObjectPersister
    {
        static NetCoreObjectPersister()
        {
            Default = new NetCoreFileSystemObjectPersister();
        }

        public static IObjectPersister Default { get; set; }
    }
}
