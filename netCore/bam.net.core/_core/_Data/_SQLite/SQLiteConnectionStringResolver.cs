using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web;
using System.Data.Common;

namespace Bam.Net.Data
{
    /// <summary>
    /// Resolves connection string requests to a sqlite database in the
    /// directory specified by the Directory property.
    /// </summary>
    public partial class SQLiteConnectionStringResolver // core
    {
        Func<DirectoryInfo> _directoryResolver;
        object _directoryResolverLock = new object();
        public Func<DirectoryInfo> DirectoryResolver
        {
            get
            {
                return _directoryResolverLock.DoubleCheckLock(ref _directoryResolver, () =>
                {
                    return () =>
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo($".\\{ApplicationNameProvider.Default.GetApplicationName()}");
                        return dirInfo;
                    };
                });
            }
            set
            {
                _directoryResolver = value;
            }
        }
    }
}
