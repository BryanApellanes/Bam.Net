using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.Diagnostic;

namespace Bam.Net.CoreServices // fx
{
    [Serializable]
    public partial class DiagnosticInfo
    {
        public DiagnosticInfo(ServiceRegistry registry = null)
        {
            ServiceRegistry = registry;

            SetAssemblies();

            SetDatabases();

            SetDaoProxies();

            SetServiceProxies();

            SetDiagnosableSettings();

            AppDiagInfo = new ApplicationDiagnosticInfo();
        }

        public DaoProxyRegistrationInfo[] DaoProxies
        {
            get;
            set;
        }

        private void SetDaoProxies()
        {
            List<DaoProxyRegistrationInfo> daoProxies = new List<DaoProxyRegistrationInfo>();
            DaoProxyRegistration.Registrations.Keys.Each(ctx =>
            {
                daoProxies.Add(new DaoProxyRegistrationInfo(DaoProxyRegistration.Registrations[ctx]));
            });
            this.DaoProxies = daoProxies.ToArray();
        }
    }
}
