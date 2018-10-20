using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// A single encoding definition applied to a single schema property.
    /// </summary>
	public class EncodingObject
	{	
﻿		[JsonProperty("contentType")]
		public string ContentType { get; set; }

﻿		[JsonProperty("headers")]
		public Dictionary<string, object> Headers { get; set; }

﻿		[JsonProperty("style")]
		public string Style { get; set; }

﻿		[JsonProperty("explode")]
		public bool Explode { get; set; }

﻿		[JsonProperty("allowReserved")]
		public bool AllowReserved { get; set; }

	}
}