using Bam.Net.CoreServices.ApplicationRegistration.Data;
using Bam.Net.CoreServices.ApplicationRegistration.Data.Dao.Repository;
using Bam.Net.ServiceProxy;
using Bam.Net.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.CoreServices
{
    /// <summary>
    /// Resolves a client application name from a request header or domain name
    /// </summary>
    public class ClientApplicationNameResolver : IApplicationNameResolver
    {
        public ClientApplicationNameResolver(ApplicationRegistrationRepository repo)
        {
            ApplicationRegistrationRepository = repo;
        }

        public ClientApplicationNameResolver(ApplicationRegistrationRepository repo, IHttpContext context) : this(repo)
        {
            HttpContext = context;
        }

        public ApplicationRegistrationRepository ApplicationRegistrationRepository { get; set; }
        IHttpContext _context;
        public IHttpContext HttpContext
        {
            get
            {
                return _context;
            }
            set
            {
                _context = value;
            }
        }

        public string GetApplicationName()
        {
            return ResolveApplicationName(HttpContext);
        }

        public string ResolveApplicationName(IHttpContext context)
        {
            string fromHeader = context?.Request?.Headers[CustomHeaders.ApplicationName];
            if (string.IsNullOrEmpty(fromHeader))
            {
                string domainName = context?.Request?.Url?.Host;
                if (!string.IsNullOrEmpty(domainName))
                {
                    HostDomain hostDomain = ApplicationRegistrationRepository.OneHostDomainWhere(d => d.DomainName == domainName);
                    if(hostDomain != null)
                    {
                        return hostDomain.DefaultApplicationName;
                    }
                }
            }
            return fromHeader.Or(Bam.Net.CoreServices.ApplicationRegistration.Data.Application.Unknown.Name);
        }
    }
}
