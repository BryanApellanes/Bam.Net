using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// Lists the required security schemes to execute this operation.
	/// The name used for each property MUST correspond to a security scheme declared in the <a href="#componentsSecuritySchemes">Security Schemes</a> under the <a href="#componentsObject">Components Object</a>.Security Requirement Objects that contain multiple schemes require 
	/// that all schemes MUST be satisfied for a request to be authorized.
	/// This enables support for scenarios where multiple query parameters or 
	/// HTTP headers are required to convey security information.When a list of Security Requirement Objects is defined on the <a href="#oasObject">Open API object</a> or <a href="#operationObject">Operation Object</a>, only one of Security Requirement Objects in the list needs to be satisfied to authorize the request.  
    /// </summary>
	public class SecurityRequirementObject
	{	
	}
}