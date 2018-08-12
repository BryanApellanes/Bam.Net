using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;

namespace Bam.Net.CoreServices.ApplicationRegistration.Data
{
    /// <summary>
    /// Persistable Client data.
    /// </summary>
    [Serializable]
    public class Client: AuditRepoData
    {
        public Client()
        {
            Secret = ServiceProxySystem.GenerateId();
        }
        public ulong MachineId { get; set; }
        Machine _machine;
        public virtual Machine Machine
        {
            get
            {
                return _machine;
            }
            set
            {
                _machine = value;
                MachineName = _machine?.Name;                
            }
        }
        public ulong ApplicationId { get; set; }
        public virtual Application Application { get; set; }
        public string ApplicationName { get; set; }
        public string MachineName { get; set; }        
        public string ServerHost { get; set; }
        public int Port { get; set; }
        public string Secret { get; set; }
        public override string ToString()
        {
            return $"{GetIdentifier()}";
        }

        protected internal string GetUserName()
        {
            Args.ThrowIfNullOrEmpty(ApplicationName);
            return $"{ApplicationName}.{MachineName}";
        }

        public string GetPseudoEmail()
        {
            return $"{GetIdentifier()}@{MachineName}";
        }

        protected internal string GetIdentifier()
        {
            return $"{GetUserName()}=>{ServerHost}:{Port}";
        }

        public static Client Of(ApplicationRegistrationRepository repo, string applicationName, string serverHost, int serverPort)
        {
            Machine persistedCurrent = repo.OneMachineWhere(m => m.Name == Machine.Current.Name);
            if(persistedCurrent == null)
            {
                persistedCurrent = repo.Save(Machine.Current);
            }
            Application app = repo.GetOneApplicationWhere(a => a.Name == applicationName);
            Client result = repo.OneClientWhere(c => 
                c.MachineId == persistedCurrent.Id && 
                c.MachineName == persistedCurrent.Name &&
                c.ApplicationId == app.Id &&
                c.ApplicationName == app.Name &&
                c.ServerHost == serverHost && 
                c.Port == serverPort);

            if(result == null)
            {
                result = new Client
                {
                    MachineId = persistedCurrent.Id,
                    MachineName = persistedCurrent.Name,
                    ApplicationId = app.Id,
                    ApplicationName = app.Name,
                    ServerHost = serverHost,
                    Port = serverPort
                };
                result = repo.Save(result);
            }
           
            return result;
        }
    }
}
