using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public class BamAuthOptions : AuthenticationSchemeOptions
    {
        public BamAuthOptions()
        {
            SignOnUrl = "https://bamapps.net/signon";
        }

        public const string Scheme = "BamAuth";

        public bool Local { get; set; }

        public string SignOnUrl { get; set; }
    }
}
