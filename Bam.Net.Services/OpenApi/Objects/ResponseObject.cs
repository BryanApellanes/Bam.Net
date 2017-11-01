using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Describes a single response from an API Operation, including design-time, static
	/// <code>links</code> to operations based on the response.
    /// </summary>
	public class ResponseObject
	{	
﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("headers")]
		public Dictionary<string, object> Headers { get; set; }

﻿		[JsonProperty("content")]
		public Dictionary<string, object> Content { get; set; }

﻿		[JsonProperty("links")]
		public Dictionary<string, object> Links { get; set; }

	}
}