using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An airport.</summary>
	public class Airport: CivicStructure
	{
		///<summary>IATA identifier for an airline or airport.</summary>
		public Text IataCode {get; set;}
		///<summary>ICAO identifier for an airport.</summary>
		public Text IcaoCode {get; set;}
	}
}
