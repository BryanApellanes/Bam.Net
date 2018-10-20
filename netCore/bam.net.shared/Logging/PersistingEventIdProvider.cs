using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;

namespace Bam.Net.Logging
{
    public class PersistingEventIdProvider : IEventIdProvider
    {
        public PersistingEventIdProvider(IRepository repository)
        {
            Repository = repository;
            Repository.AddType<EventIdentifier>();
        }
        protected IRepository Repository { get; set; }
        public int GetEventId(string applicationName, string messageSignature)
        {
            int eventId = $"{applicationName}.{messageSignature}".ToSha256Int();
            Task.Run(() =>
            {
                EventIdentifier id = Repository.Query<EventIdentifier>(new { ApplicationName = applicationName, MessageSignature = messageSignature }).FirstOrDefault();
                if (id == null)
                {
                    id = new EventIdentifier
                    {
                        EventId = eventId,
                        ApplicationName = applicationName,
                        MessageSignature = messageSignature
                    };
                    Repository.Save(id);
                }
            });
            return eventId;
        }
    }
}
