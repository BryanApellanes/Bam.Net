using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A subclass of Role used to describe roles within organizations.</summary>
	public class OrganizationRole: Role
	{
		///<summary>A number associated with a role in an organization, for example, the number on an athlete's jersey.</summary>
		public Number NumberedPosition {get; set;}
	}
}
