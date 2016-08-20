using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An apartment (in American English) or flat (in British English) is a self-contained housing unit (a type of residential real estate) that occupies only part of a building (Source: Wikipedia, the free encyclopedia, see http://en.wikipedia.org/wiki/Apartment).</summary>
	public class Apartment: Accommodation
	{
		///<summary>The number of rooms (excluding bathrooms and closets) of the acccommodation or lodging business.Typical unit code(s): ROM for room or C62 for no unit. The type of room can be put in the unitText property of the QuantitativeValue.</summary>
		public OneOfThese<Number,QuantitativeValue> NumberOfRooms {get; set;}
		///<summary>The allowed total occupancy for the accommodation in persons (including infants etc). For individual accommodations, this is not necessarily the legal maximum but defines the permitted usage as per the contractual agreement (e.g. a double room used by a single person).Typical unit code(s): C62 for person</summary>
		public QuantitativeValue Occupancy {get; set;}
	}
}
