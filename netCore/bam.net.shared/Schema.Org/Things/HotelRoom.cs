using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A hotel room is a single room in a hotel.See also the dedicated document on the use of schema.org for marking up hotels and other forms of accommodations.</summary>
	public class HotelRoom: Room
	{
		///<summary>The type of bed or beds included in the accommodation. For the single case of just one bed of a certain type, you use bed directly with a text.      If you want to indicate the quantity of a certain kind of bed, use an instance of BedDetails. For more detailed information, use the amenityFeature property.</summary>
		public OneOfThese<BedDetails,Text> Bed {get; set;}
		///<summary>The allowed total occupancy for the accommodation in persons (including infants etc). For individual accommodations, this is not necessarily the legal maximum but defines the permitted usage as per the contractual agreement (e.g. a double room used by a single person).Typical unit code(s): C62 for person</summary>
		public QuantitativeValue Occupancy {get; set;}
	}
}
