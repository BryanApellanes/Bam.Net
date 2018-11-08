using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class NetCoreWebExtensions
    {
        public static AuthenticationBuilder AddBamAuthentication(this AuthenticationBuilder builder, Action<BamAuthOptions> options)
        {
            return builder.AddScheme<BamAuthOptions, BamAuthHandler>(BamAuthOptions.Scheme, options);
        }
    }
}
