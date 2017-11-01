using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// A container for the expected responses of an operation.
	/// The container maps a HTTP response code to the expected response.The documentation is not necessarily expected to cover all possible 
	/// HTTP response codes because they may not be known in advance.
	/// However, documentation is expected to cover a successful operation 
	/// response and any known errors.The <code>default</code> MAY be used as a default response object for all HTTP codes
	/// that are not covered individually by the specification.The <code>Responses Object</code> MUST contain at least one response code, and it
	/// SHOULD be the response for a successful operation call.
    /// </summary>
	public class ResponsesObject
	{	
﻿		[JsonProperty("default")]
		public ResponseObject Default { get; set; }

	}
}