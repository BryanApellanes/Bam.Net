using Bam.Net.Data.Repositories;
using Bam.Net.ServiceProxy.Secure;
using System.Collections.Generic;
using System;
using System.Collections;
using Bam.Net.Data;
using System.IO;

namespace Bam.Net.CoreServices.Services
{
    [Encrypt]
    [Proxy("persistence")]
    [ApiKeyRequired]
    public class PersistenceService: CompositeRepository
    {
        DaoRepository _localRepo;

        public PersistenceService(DaoRepository repo, string workspacePath) : base(repo, workspacePath)
        {
            _localRepo = repo;
            RemoteRepositories = new HashSet<IRepository>();
        }
        
        public HashSet<IRepository> RemoteRepositories { get; }
        public IRepository AddRemote(string hostName, int port)
        {
            ProxyFactory proxyFactory = new ProxyFactory(Path.Combine(WorkspacePath, "ProxyFactoryTemp", $"{hostName}_{port}"), Logger);
            PersistenceService svc = proxyFactory.GetProxy<PersistenceService>(hostName, port);
            RemoteRepositories.Add(svc);
            WriteRepositories.Add(svc);
            return svc;
        }
    }
}
