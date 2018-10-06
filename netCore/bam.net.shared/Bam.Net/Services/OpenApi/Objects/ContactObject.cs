using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Contact information for the exposed API.
    /// </summary>
	public class ContactObject
	{	
﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("url")]
		public string Url { get; set; }

﻿		[JsonProperty("email")]
		public string Email { get; set; }

	}
}