using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// License information for the exposed API.
    /// </summary>
	public class LicenseObject
	{	
﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("url")]
		public string Url { get; set; }

	}
}