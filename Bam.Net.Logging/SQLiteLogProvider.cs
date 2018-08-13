using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data;
using Bam.Net.Data.SQLite;

namespace Bam.Net.Logging
{
    public class SQLiteLogProvider : DaoLogProvider
    {
        public SQLiteLogProvider(string directoryPath, string dbName) : this(new SQLiteLogger(new SQLiteDatabase(directoryPath, dbName)))
        { }

        public SQLiteLogProvider(SQLiteLogger logger) : this(logger, new LogReaderFactory())
        { }

        public SQLiteLogProvider(SQLiteLogger2 logger) : this(logger, new LogReaderFactory())
        { }

        public SQLiteLogProvider(SQLiteLogger logger, LogReaderFactory factory) : base(logger.Database, LogSchemaType.Flat, logger, factory)
        {
            LogReaderFactory.Register<SQLiteLogger>(() => new DaoLoggerLogReader(logger));
        }

        public SQLiteLogProvider(SQLiteLogger2 logger, LogReaderFactory factory): base(logger.Database, LogSchemaType.EventRelationships, logger, factory)
        {
            LogReaderFactory.Register<SQLiteLogger2>(() => new DaoLogger2LogReader(logger));
        }
    }
}
