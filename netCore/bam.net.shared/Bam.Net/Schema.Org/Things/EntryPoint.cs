using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An entry point, within some Web-based protocol.</summary>
	public class EntryPoint: Intangible
	{
		///<summary>An application that can complete the request. Supersedes application.</summary>
		public SoftwareApplication ActionApplication {get; set;}
		///<summary>The high level platform(s) where the Action can be performed for the given URL. To specify a specific application or operating system instance, use actionApplication.</summary>
		public OneOfThese<Text,Url> ActionPlatform {get; set;}
		///<summary>The supported content type(s) for an EntryPoint response.</summary>
		public Text ContentType {get; set;}
		///<summary>The supported encoding type(s) for an EntryPoint request.</summary>
		public Text EncodingType {get; set;}
		///<summary>An HTTP method that specifies the appropriate HTTP method for a request to an HTTP EntryPoint. Values are capitalized strings as used in HTTP.</summary>
		public Text HttpMethod {get; set;}
		///<summary>An url template (RFC6570) that will be used to construct the target of the execution of the action.</summary>
		public Text UrlTemplate {get; set;}
	}
}
