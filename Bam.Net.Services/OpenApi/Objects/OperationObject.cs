using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Describes a single API operation on a path.
    /// </summary>
	public class OperationObject
	{	
﻿		[JsonProperty("tags")]
		public string Tags { get; set; }

﻿		[JsonProperty("summary")]
		public string Summary { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("externalDocs")]
		public ExternalDocumentationObject ExternalDocs { get; set; }

﻿		[JsonProperty("operationId")]
		public string OperationId { get; set; }

﻿		[JsonProperty("parameters")]
		public ParameterObject Parameters { get; set; }

﻿		[JsonProperty("requestBody")]
		public RequestBodyObject RequestBody { get; set; }

﻿		[JsonProperty("responses")]
		public ResponsesObject Responses { get; set; }

﻿		[JsonProperty("callbacks")]
		public Dictionary<string, object> Callbacks { get; set; }

﻿		[JsonProperty("deprecated")]
		public bool Deprecated { get; set; }

﻿		[JsonProperty("security")]
		public SecurityRequirementObject Security { get; set; }

﻿		[JsonProperty("servers")]
		public ServerObject Servers { get; set; }

	}
}