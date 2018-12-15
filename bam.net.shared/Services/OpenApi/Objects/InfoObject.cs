using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// The object provides metadata about the API.
	/// The metadata MAY be used by the clients if needed, and MAY be presented 
	/// in editing or documentation generation tools for convenience.
    /// </summary>
	public class InfoObject
	{	
﻿		[JsonProperty("title")]
		public string Title { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("termsOfService")]
		public string TermsOfService { get; set; }

﻿		[JsonProperty("contact")]
		public ContactObject Contact { get; set; }

﻿		[JsonProperty("license")]
		public LicenseObject License { get; set; }

﻿		[JsonProperty("version")]
		public string Version { get; set; }

	}
}