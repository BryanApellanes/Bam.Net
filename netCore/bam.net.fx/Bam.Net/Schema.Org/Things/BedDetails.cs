using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An entity holding detailed information about the available bed types, e.g. the quantity of twin beds for a hotel room. For the single case of just one bed of a certain type, you can use bed directly with a text. See also BedType (under development).</summary>
	public class BedDetails: Intangible
	{
		///<summary>The quantity of the given bed type available in the HotelRoom, Suite, House, or Apartment.</summary>
		public Number NumberOfBeds {get; set;}
		///<summary>The type of bed to which the BedDetail refers, i.e. the type of bed available in the quantity indicated by quantity.</summary>
		public Text TypeOfBed {get; set;}
	}
}
