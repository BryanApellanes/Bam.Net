using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Used to describe a seat, such as a reserved seat in an event reservation.</summary>
	public class Seat: Intangible
	{
		///<summary>The location of the reserved seat (e.g., 27).</summary>
		public Text SeatNumber {get; set;}
		///<summary>The row location of the reserved seat (e.g., B).</summary>
		public Text SeatRow {get; set;}
		///<summary>The section location of the reserved seat (e.g. Orchestra).</summary>
		public Text SeatSection {get; set;}
		///<summary>The type/class of the seat.</summary>
		public OneOfThese<QualitativeValue,Text> SeatingType {get; set;}
	}
}
