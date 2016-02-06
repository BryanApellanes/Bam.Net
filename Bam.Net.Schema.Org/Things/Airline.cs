/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An organization that provides flights for passengers.</summary>
	public class Airline: Organization
	{
		///<summary>IATA identifier for an airline or airport.</summary>
		public Text IataCode {get; set;}
	}
}
