using System;
using System.Collections.Generic;
using System.Linq;
using Bam.Net.Data;
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.ApplicationRegistration;

namespace Bam.Net.CoreServices
{
    [Serializable]
    public class DiagnosticInfo
    {
        public DiagnosticInfo()
        {
            SetAssemblies();

            SetDatabases();

            SetDaoProxies();

            SetServiceProxies();

            this.AppDiagInfo = new ApplicationDiagnosticInfo();
        }
        
        public ProcessDescriptor ProcessDescriptor
        {
            get
            {
                return ProcessDescriptor.Current;
            }
        }

        public string[] Assemblies
        {
            get;
            set;
        }

        public ApplicationDiagnosticInfo AppDiagInfo
        {
            get;
            set;
        }

        public DatabaseInfo[] Databases
        {
            get;
            set;
        }

        public DaoProxyRegistrationInfo[] DaoProxies
        {
            get;
            set;
        }

        public ServiceProxyInfo[] ServiceProxies
        {
            get;
            set;
        }

        private void SetServiceProxies()
        {
            List<ServiceProxyInfo> serviceProxies = new List<ServiceProxyInfo>();
            ServiceProxySystem.Incubator.ClassNames.Each(cn =>
            {
                serviceProxies.Add(new ServiceProxyInfo(ServiceProxySystem.Incubator[cn]));
            });
            this.ServiceProxies = serviceProxies.ToArray();
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

        private void SetDatabases()
        {
            HashSet<DatabaseInfo> dbInfos = new HashSet<DatabaseInfo>();
            Db.DefaultContainer.GetInfos().Each(dbInfo => dbInfos.Add(dbInfo));
            Database.Infos.Each(dbInfo => dbInfos.Add(dbInfo));
            Databases = dbInfos.ToArray();
        }

        private void SetAssemblies()
        {
            HashSet<string> assemblyNames = new HashSet<string>();
            AppDomain.CurrentDomain.GetAssemblies().Each(ass => assemblyNames.Add(ass.FullName));
            Assemblies = assemblyNames.ToArray();
        }
    }
}
