using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A service provided by a government organization, e.g. food stamps, veterans benefits, etc.</summary>
	public class GovernmentService: Service
	{
		///<summary>The operating organization, if different from the provider.  This enables the representation of services that are provided by an organization, but operated by another organization like a subcontractor.</summary>
		public Organization ServiceOperator {get; set;}
	}
}
