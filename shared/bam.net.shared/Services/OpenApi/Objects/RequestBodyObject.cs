using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Describes a single request body.
    /// </summary>
	public class RequestBodyObject
	{	
﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("content")]
		public Dictionary<string, object> Content { get; set; }

﻿		[JsonProperty("required")]
		public bool Required { get; set; }

	}
}