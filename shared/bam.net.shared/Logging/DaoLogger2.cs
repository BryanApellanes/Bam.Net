/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bam.Net.Data;
using System.Threading.Tasks;
using Bam.Net.Logging.Data;

namespace Bam.Net.Logging
{
    /// <summary>
    /// A database logger that uses a 
    /// a schema that will grow less over
    /// time by breaking out the parts
    /// of the event into separate tables
    /// </summary>
    public class DaoLogger2: Logger, IDaoLogger
    {
		public DaoLogger2()
			: base()
		{
		}

		public DaoLogger2(Database logTo)
		{
			Database = logTo;
            Database.TryEnsureSchema<SourceName>();
		}

		public Database Database
		{
			get;
			set;
		}

        public Action<LogEvent> LogEventCreatedHandler { get; set; }

        protected internal override LogEvent CreateLogEvent(string messageSignature, string user, string category, LogEventType type, Exception ex, params string[] messageVariableValues)
        {
            LogEvent logEvent = base.CreateLogEvent(messageSignature, user, category, type, ex, messageVariableValues);
            LogEventCreatedHandler?.Invoke(logEvent);
            return logEvent;
        }

        public override void CommitLogEvent(LogEvent logEvent)
        {
            Task.Run(() =>
            {
                SourceName source = GetSource(logEvent, Database);
                CategoryName category = GetCategory(logEvent, Database);
                UserName user = GetUser(logEvent, Database);
                ComputerName computer = GetComputer(logEvent, Database);
                Signature signature = GetSignature(logEvent, Database);

                Event instance = new Event()
                {
                    SignatureId = signature.Id,
                    ComputerNameId = computer.Id,
                    CategoryNameId = category.Id,
                    SourceNameId = source.Id,
                    UserNameId = user.Id,
                    EventId = logEvent.EventID,
                    Time = logEvent.Time,
                    Severity = (int)logEvent.Severity
                };
                instance.Save(Database);
                logEvent.MessageVariableValues.Each((val, pos) =>
                {
                    Param p = instance.Params.AddNew();
                    p.Position = pos;
                    p.Value = val;
                });
                instance.Save(Database);
            });
        }

        private static Signature GetSignature(LogEvent logEvent, Database db)
        {
            Signature signature = Signature.OneWhere(sc => sc.Value == logEvent.MessageSignature, db);
            if (signature == null)
            {
                signature = new Signature()
                {
                    Value = logEvent.MessageSignature
                };
                signature.Save(db);
            }
            return signature;
        }

        private static ComputerName GetComputer(LogEvent logEvent, Database db)
        {
            ComputerName computer = ComputerName.OneWhere(cc => cc.Value == logEvent.Computer, db);
            if (computer == null)
            {
                computer = new ComputerName()
                {
                    Value = logEvent.Computer
                };
                computer.Save(db);
            }
            return computer;
        }

        private static UserName GetUser(LogEvent logEvent, Database db)
        {
            UserName user = UserName.OneWhere(uc => uc.Value == logEvent.User, db);
            if (user == null)
            {
                user = new UserName()
                {
                    Value = logEvent.User
                };
                user.Save(db);
            }
            return user;
        }

        private static CategoryName GetCategory(LogEvent logEvent, Database db)
        {
            CategoryName category = CategoryName.OneWhere(cc => cc.Value == logEvent.Category, db);
            if (category == null)
            {
                category = new CategoryName()
                {
                    Value = logEvent.Category
                };
                category.Save(db);
            }
            return category;
        }

        private static SourceName GetSource(LogEvent logEvent, Database db)
        {
            SourceName source = SourceName.OneWhere(sc => sc.Value == logEvent.Source, db);
            if (source == null)
            {
                source = new SourceName()
                {
                    Value = logEvent.Source
                };
                source.Save(db);
            }
            return source;
        }
    }
}
