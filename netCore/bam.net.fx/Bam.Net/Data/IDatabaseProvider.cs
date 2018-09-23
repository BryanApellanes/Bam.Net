using Bam.Net.Logging;
using System;

namespace Bam.Net.Data
{
    public interface IDatabaseProvider
    {
        ILogger Logger { get; set; }
        void SetDatabases(params object[] instances);

        Database GetAppDatabase(IApplicationNameProvider appNameProvider, string databaseName);
        Database GetSysDatabase(string databaseName);
        Database GetAppDatabaseFor(IApplicationNameProvider appNameProvider, object instance);
        Database GetSysDatabaseFor(object instance);
        Database GetAppDatabaseFor(IApplicationNameProvider appNameProvider, Type objectType, string info = null);
        Database GetSysDatabaseFor(Type objectType, string info = null);
    }
}