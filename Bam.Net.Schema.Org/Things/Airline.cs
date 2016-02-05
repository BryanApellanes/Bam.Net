using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An organization that provides flights for passengers.</summary>
	public class Airline: Organization
	{
		///<summary>The type of boarding policy used by the airline (e.g. zone-based or group-based).</summary>
		public BoardingPolicyType BoardingPolicy {get; set;}
		///<summary>IATA identifier for an airline or airport.</summary>
		public Text IataCode {get; set;}
	}
}
