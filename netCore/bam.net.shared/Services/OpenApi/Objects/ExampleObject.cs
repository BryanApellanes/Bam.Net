using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// 
    /// </summary>
	public class ExampleObject
	{	
﻿		[JsonProperty("summary")]
		public string Summary { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("value")]
		public object Value { get; set; }

﻿		[JsonProperty("externalValue")]
		public string ExternalValue { get; set; }

	}
}