using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// When request bodies or response payloads may be one of a number of different schemas, a <code>discriminator</code>
	///  object can be used to aid in serialization, deserialization, and 
	/// validation.  The discriminator is a specific object in a schema which is
	///  used to inform the consumer of the specification of an alternative 
	/// schema based on the value associated with it.When using the discriminator, <em>inline</em> schemas will not be considered.
    /// </summary>
	public class DiscriminatorObject
	{	
﻿		[JsonProperty("propertyName")]
		public string PropertyName { get; set; }

﻿		[JsonProperty("mapping")]
		public Dictionary<string, object> Mapping { get; set; }

	}
}