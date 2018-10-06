using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Describes the operations available on a single path.
	/// A Path Item MAY be empty, due to <a href="#securityFiltering">ACL constraints</a>.
	/// The path itself is still exposed to the documentation viewer but they 
	/// will not know which operations and parameters are available.
    /// </summary>
	public class PathItemObject
	{	
﻿		[JsonProperty("$ref")]
		public string Ref { get; set; }

﻿		[JsonProperty("summary")]
		public string Summary { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("get")]
		public OperationObject Get { get; set; }

﻿		[JsonProperty("put")]
		public OperationObject Put { get; set; }

﻿		[JsonProperty("post")]
		public OperationObject Post { get; set; }

﻿		[JsonProperty("delete")]
		public OperationObject Delete { get; set; }

﻿		[JsonProperty("options")]
		public OperationObject Options { get; set; }

﻿		[JsonProperty("head")]
		public OperationObject Head { get; set; }

﻿		[JsonProperty("patch")]
		public OperationObject Patch { get; set; }

﻿		[JsonProperty("trace")]
		public OperationObject Trace { get; set; }

﻿		[JsonProperty("servers")]
		public ServerObject Servers { get; set; }

﻿		[JsonProperty("parameters")]
		public ParameterObject Parameters { get; set; }

	}
}