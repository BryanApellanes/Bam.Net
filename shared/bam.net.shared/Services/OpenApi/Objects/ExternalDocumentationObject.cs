using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Allows referencing an external resource for extended documentation.
    /// </summary>
	public class ExternalDocumentationObject
	{	
﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("url")]
		public string Url { get; set; }

	}
}