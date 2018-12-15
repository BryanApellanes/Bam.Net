using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// This is the root document object of the <a href="#oasDocument">OpenAPI document</a>.
    /// </summary>
	public class OpenAPIObject
	{	
﻿		[JsonProperty("openapi")]
		public string Openapi { get; set; }

﻿		[JsonProperty("info")]
		public InfoObject Info { get; set; }

﻿		[JsonProperty("servers")]
		public ServerObject Servers { get; set; }

﻿		[JsonProperty("paths")]
		public PathsObject Paths { get; set; }

﻿		[JsonProperty("components")]
		public ComponentsObject Components { get; set; }

﻿		[JsonProperty("security")]
		public SecurityRequirementObject Security { get; set; }

﻿		[JsonProperty("tags")]
		public TagObject Tags { get; set; }

﻿		[JsonProperty("externalDocs")]
		public ExternalDocumentationObject ExternalDocs { get; set; }

	}
}