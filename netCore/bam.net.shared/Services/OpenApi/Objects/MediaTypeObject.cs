using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Each Media Type Object provides schema and examples for the media type identified by its key.
    /// </summary>
	public class MediaTypeObject
	{	
﻿		[JsonProperty("schema")]
		public SchemaObject Schema { get; set; }

﻿		[JsonProperty("example")]
		public object Example { get; set; }

﻿		[JsonProperty("examples")]
		public Dictionary<string, object> Examples { get; set; }

﻿		[JsonProperty("encoding")]
		public Dictionary<string, object> Encoding { get; set; }

	}
}