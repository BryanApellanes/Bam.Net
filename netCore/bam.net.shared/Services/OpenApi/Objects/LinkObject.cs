using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// The <code>Link object</code> represents a possible design-time link 
	/// for a response.
	/// The presence of a link does not guarantee the caller's ability to 
	/// successfully invoke it, rather it provides a known relationship and 
	/// traversal mechanism between responses and other operations.Unlike <em>dynamic</em> links (i.e. links provided <strong>in</strong> the response payload), the OAS linking mechanism does not require link information in the runtime response.For computing links, and providing instructions to execute them, a <a href="#runtimeExpression">runtime expression</a> is used for accessing values in an operation and using them as parameters while invoking the linked operation.  
    /// </summary>
	public class LinkObject
	{	
﻿		[JsonProperty("operationRef")]
		public string OperationRef { get; set; }

﻿		[JsonProperty("operationId")]
		public string OperationId { get; set; }

﻿		[JsonProperty("parameters")]
		public Dictionary<string, object> Parameters { get; set; }

﻿		[JsonProperty("requestBody")]
		public object RequestBody { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("server")]
		public ServerObject Server { get; set; }

	}
}