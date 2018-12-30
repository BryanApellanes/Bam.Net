using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// A metadata object that allows for more fine-tuned XML model definitions.When using arrays, XML element names are <em>not</em> inferred (for singular/plural forms) and the <code>name</code> property SHOULD be used to add that information.
	/// See examples for expected behavior.
    /// </summary>
	public class XMLObject
	{	
﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("namespace")]
		public string Namespace { get; set; }

﻿		[JsonProperty("prefix")]
		public string Prefix { get; set; }

﻿		[JsonProperty("attribute")]
		public bool Attribute { get; set; }

﻿		[JsonProperty("wrapped")]
		public bool Wrapped { get; set; }

	}
}