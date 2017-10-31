using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Describes a single operation parameter.A unique parameter is defined by a combination of a <a href="#parameterName">name</a> and <a href="#parameterIn">location</a>.
    /// </summary>
	public class ParameterObject
	{	
﻿		[JsonProperty("name")]
		public string Name { get; set; }

﻿		[JsonProperty("in")]
		public string In { get; set; }

﻿		[JsonProperty("description")]
		public string Description { get; set; }

﻿		[JsonProperty("required")]
		public bool Required { get; set; }

﻿		[JsonProperty("deprecated")]
		public bool Deprecated { get; set; }

﻿		[JsonProperty("allowEmptyValue")]
		public bool AllowEmptyValue { get; set; }

	}
}