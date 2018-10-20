using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A permit issued by an organization, e.g. a parking pass.</summary>
	public class Permit: Intangible
	{
		///<summary>The organization issuing the ticket or permit.</summary>
		public Organization IssuedBy {get; set;}
		///<summary>The service through with the permit was granted.</summary>
		public Service IssuedThrough {get; set;}
		///<summary>The target audience for this permit.</summary>
		public Audience PermitAudience {get; set;}
		///<summary>The time validity of the permit.</summary>
		public Duration ValidFor {get; set;}
		///<summary>The date when the item becomes valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidFrom {get; set;}
		///<summary>The geographic area where the permit is valid.</summary>
		public AdministrativeArea ValidIn {get; set;}
		///<summary>The date when the item is no longer valid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ValidUntil {get; set;}
	}
}
