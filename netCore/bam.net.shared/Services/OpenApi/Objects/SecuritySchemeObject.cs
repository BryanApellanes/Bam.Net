using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Defines a security scheme that can be used by the operations.
	/// Supported schemes are HTTP authentication, an API key (either as a 
	/// header or as a query parameter), OAuth2's common flows (implicit, 
	/// password, application and access code) as defined in <a href="https://tools.ietf.org/html/rfc6749">RFC6749</a>, and <a href="https://tools.ietf.org/html/draft-ietf-oauth-discovery-06">OpenID Connect Discovery</a>.
    /// </summary>
	public class SecuritySchemeObject
	{	
﻿		[JsonProperty("type")]
		public string Type { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("in")]
		public string In { get; set; }

﻿		[JsonProperty("scheme")]
		public string Scheme { get; set; }

﻿		[JsonProperty("bearerFormat")]
		public string BearerFormat { get; set; }

﻿		[JsonProperty("flows")]
		public OAuthFlowsObject Flows { get; set; }

﻿		[JsonProperty("openIdConnectUrl")]
		public string OpenIdConnectUrl { get; set; }

	}
}