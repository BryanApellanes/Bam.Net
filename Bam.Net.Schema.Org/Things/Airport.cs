/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An airport.</summary>
	public class Airport: CivicStructure
	{
		///<summary>IATA identifier for an airline or airport.</summary>
		public Text IataCode {get; set;}
		///<summary>IACO identifier for an airport.</summary>
		public Text IcaoCode {get; set;}
	}
}
