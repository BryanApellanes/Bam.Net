using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Used to describe membership in a loyalty programs (e.g. "StarAliance"), traveler clubs (e.g. "AAA"), purchase clubs ("Safeway Club"), etc.</summary>
	public class ProgramMembership: Intangible
	{
		///<summary>The organization (airline, travelers' club, etc.) the membership is made with.</summary>
		public Organization HostingOrganization {get; set;}
		///<summary>A member of an Organization or a ProgramMembership. Organizations can be members of organizations; ProgramMembership is typically for individuals. Supersedes members, musicGroupMember. Inverse property: memberOf.</summary>
		public OneOfThese<Organization,Person> Member {get; set;}
		///<summary>A unique identifier for the membership.</summary>
		public Text MembershipNumber {get; set;}
		///<summary>The program providing the membership.</summary>
		public Text ProgramName {get; set;}
	}
}
