using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// A simple object to allow referencing other components in the specification, internally and externally.The Reference Object is defined by <a href="https://tools.ietf.org/html/draft-pbryan-zyp-json-ref-03">JSON Reference</a> and follows the same structure, behavior and rules. For this specification, reference resolution is accomplished as 
	/// defined by the JSON Reference specification and not by the JSON Schema 
	/// specification.
    /// </summary>
	public class ReferenceObject
	{	
﻿		[JsonProperty("$ref")]
		public string Ref { get; set; }

	}
}