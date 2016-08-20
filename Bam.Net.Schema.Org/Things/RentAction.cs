using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of giving money in return for temporary use, but not ownership, of an object such as a vehicle or property. For example, an agent rents a property from a landlord in exchange for a periodic payment.</summary>
	public class RentAction: TradeAction
	{
		///<summary>A sub property of participant. The owner of the real estate property.</summary>
		public OneOfThese<Organization,Person> Landlord {get; set;}
		///<summary>A sub property of participant. The real estate agent involved in the action.</summary>
		public RealEstateAgent RealEstateAgent {get; set;}
	}
}
