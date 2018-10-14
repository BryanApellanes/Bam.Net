using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Adds metadata to a single tag that is used by the <a href="#operationObject">Operation Object</a>.
	/// It is not mandatory to have a Tag Object per tag defined in the Operation Object instances.
    /// </summary>
	public class TagObject
	{	
﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("externalDocs")]
		public ExternalDocumentationObject ExternalDocs { get; set; }

	}
}