using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public class BamAuthHandler : AuthenticationHandler<BamAuthOptions>
    {
        public BamAuthHandler(IOptionsMonitor<BamAuthOptions> options, Logging.ILoggerFactory logger, System.Text.Encodings.Web.UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // there is Context here
            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
