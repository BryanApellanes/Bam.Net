using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Configuration details for a supported OAuth Flow
    /// </summary>
	public class OAuthFlowObject
	{	
﻿		[JsonProperty("authorizationUrl")]
		public string AuthorizationUrl { get; set; }

﻿		[JsonProperty("tokenUrl")]
		public string TokenUrl { get; set; }

﻿		[JsonProperty("refreshUrl")]
		public string RefreshUrl { get; set; }

﻿		[JsonProperty("scopes")]
		public Dictionary<string, object> Scopes { get; set; }

	}
}