using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// An object representing a Server Variable for server URL template substitution.
    /// </summary>
	public class ServerVariableObject
	{	
﻿		[JsonProperty("enum")]
		public string Enum { get; set; }

﻿		[JsonProperty("default")]
		public string Default { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

	}
}