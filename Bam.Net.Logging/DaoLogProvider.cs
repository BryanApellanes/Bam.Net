using Bam.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging
{
    public class DaoLogProvider : LogProvider
    {
        readonly Dictionary<LogSchemaType, Func<ILogger>> _loggers;
        public DaoLogProvider(Database database, LogSchemaType schemaType, ILogger defaultLogger, LogReaderFactory factory) : base(defaultLogger, factory)
        {
            Args.ThrowIfNull(database, "DaoLogProvider.Database");
            _loggers = new Dictionary<LogSchemaType, Func<ILogger>>()
            {
                { LogSchemaType.Invalid, () => DefaultLogger },
                { LogSchemaType.Flat, () => new DaoLogger(Database) },
                { LogSchemaType.EventRelationships, () => new DaoLogger2(Database) }
            };
            Database = database;
            LogSchemaType = schemaType;
        }

        public LogSchemaType LogSchemaType { get; set; }

        public Database Database { get; set; }

        public override ILogger GetLogger()
        {
            return _loggers[LogSchemaType]();
        }
    }
}
