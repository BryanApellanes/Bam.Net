using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A permission for a particular person or group to access a particular file.</summary>
	public class DigitalDocumentPermission: Intangible
	{
		///<summary>The person, organization, contact point, or audience that has been granted this permission.</summary>
		public OneOfThese<Audience,ContactPoint,Organization,Person> Grantee {get; set;}
		///<summary>The type of permission granted the person, organization, or audience.</summary>
		public DigitalDocumentPermissionType PermissionType {get; set;}
	}
}
