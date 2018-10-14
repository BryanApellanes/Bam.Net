using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// In an <code>example</code>, a JSON Reference MAY be used, with the
	/// explicit restriction that examples having a JSON format with object named
	/// <code>$ref</code> are not allowed. Therefore, that <code>example</code>, structurally, can be
	/// either a string primitive or an object, similar to <code>additionalProperties</code>.In all cases, the payload is expected to be compatible with the type schema
	/// for the associated value.  Tooling implementations MAY choose to
	/// validate compatibility automatically, and reject the example value(s) if they
	/// are incompatible.
    /// </summary>
	public class ExamplesObject
	{	
﻿		[JsonProperty("$ref")]
		public string Ref { get; set; }

	}
}