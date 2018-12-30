using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Holds a set of reusable objects for different aspects of the OAS.
	/// All objects defined within the components object will have no effect on 
	/// the API unless they are explicitly referenced from properties outside 
	/// the components object.
    /// </summary>
	public class ComponentsObject
	{	
﻿		[JsonProperty("schemas")]
		public Dictionary<string, object> Schemas { get; set; }

﻿		[JsonProperty("responses")]
		public Dictionary<string, object> Responses { get; set; }

﻿		[JsonProperty("parameters")]
		public Dictionary<string, object> Parameters { get; set; }

﻿		[JsonProperty("examples")]
		public Dictionary<string, object> Examples { get; set; }

﻿		[JsonProperty("requestBodies")]
		public Dictionary<string, object> RequestBodies { get; set; }

﻿		[JsonProperty("headers")]
		public Dictionary<string, object> Headers { get; set; }

﻿		[JsonProperty("securitySchemes")]
		public Dictionary<string, object> SecuritySchemes { get; set; }

﻿		[JsonProperty("links")]
		public Dictionary<string, object> Links { get; set; }

﻿		[JsonProperty("callbacks")]
		public Dictionary<string, object> Callbacks { get; set; }

	}
}