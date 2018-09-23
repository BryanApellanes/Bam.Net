using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bam.Net.Services.OpenApi.Objects
{
    /// <summary>
    /// A map of possible out-of band callbacks related to the parent operation.
	/// Each value in the map is a <a href="#pathItemObject">Path Item Object</a>
	///  that describes a set of requests that may be initiated by the API 
	/// provider and the expected responses.
	/// The key value used to identify the callback object is an expression, 
	/// evaluated at runtime, that identifies a URL to use for the callback 
	/// operation.
    /// </summary>
	public class CallbackObject
	{	
	}
}