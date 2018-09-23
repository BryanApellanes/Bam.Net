using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An electronic file or document.</summary>
	public class DigitalDocument: CreativeWork
	{
		///<summary>A permission related to the access to this document (e.g. permission to read or write an electronic document). For a public document, specify a grantee with an Audience with audienceType equal to "public".</summary>
		public DigitalDocumentPermission HasDigitalDocumentPermission {get; set;}
	}
}
