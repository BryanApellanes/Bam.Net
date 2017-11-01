using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Allows configuration of the supported OAuth Flows.
    /// </summary>
	public class OAuthFlowsObject
	{	
﻿		[JsonProperty("implicit")]
		public OAuthFlowObject Implicit { get; set; }

﻿		[JsonProperty("password")]
		public OAuthFlowObject Password { get; set; }

﻿		[JsonProperty("clientCredentials")]
		public OAuthFlowObject ClientCredentials { get; set; }

﻿		[JsonProperty("authorizationCode")]
		public OAuthFlowObject AuthorizationCode { get; set; }

	}
}