using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// An object representing a Server.
    /// </summary>
	public class ServerObject
	{	
﻿		[JsonProperty("url")]
		public string Url { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("variables")]
		public Dictionary<string, object> Variables { get; set; }

	}
}