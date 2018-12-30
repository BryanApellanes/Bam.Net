using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A house is a building or structure that has the ability to be occupied for habitation by humans or other creatures (Source: Wikipedia, the free encyclopedia, see http://en.wikipedia.org/wiki/House).</summary>
	public class House: Accommodation
	{
		///<summary>The number of rooms (excluding bathrooms and closets) of the acccommodation or lodging business.Typical unit code(s): ROM for room or C62 for no unit. The type of room can be put in the unitText property of the QuantitativeValue.</summary>
		public OneOfThese<Number,QuantitativeValue> NumberOfRooms {get; set;}
	}
}
