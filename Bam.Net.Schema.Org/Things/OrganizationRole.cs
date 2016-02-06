using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A subclass of Role used to describe roles within organizations.</summary>
	public class OrganizationRole: Role
	{
		///<summary>A number associated with a role in an organization, for example, the number on an athlete's jersey.</summary>
		public Number NumberedPosition {get; set;}
	}
}
