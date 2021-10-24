using System;
using System.Collections.Generic;
using Bam.Net.Incubation;
using Bam.Net.Messaging;
using Bam.Net.ServiceProxy;
using Bam.Net.UserAccounts;
using System.IO;
using Bam.Net.Data.Repositories;
using Bam.Net.Server;
using Bam.Net.Logging;
using Bam.Net.Data;
using Bam.Net.ServiceProxy.Secure;
using Bam.Net.CoreServices.Files;
using Bam.Net.CoreServices.ServiceRegistration.Data.Dao.Repository;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
using Bam.Net.CoreServices;

namespace Bam.Net.Services.DataReplication
{
    /// <summary>
    /// Service container for DataReplication services
    /// </summary>
    public static partial class JournalRegistryContainer
    {
        public static ServiceRegistry Create()
        {
            ServiceRegistry registry = LocalServiceRegistryContainer.Create();
            registry
                .For<ISequenceProvider>().Use<FileSequenceProvider>()
                .For<ITypeConverter>().Use<DefaultTypeConverter>()
                .For<TypeMap>().Use<TypeMap>()
                .For<IJournalEntryValueFlusher>().Use<JournalEntryValueFlusher>()
                .For<IJournalEntryValueLoader>().Use<JournalEntryValueLoader>()
                .For<Journal>().Use<Journal>()
                .For<IJournalManager>().Use<JournalManager>();

            return registry;
        }
    }
}
