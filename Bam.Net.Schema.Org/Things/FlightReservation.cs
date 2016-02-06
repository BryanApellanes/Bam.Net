/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A reservation for air travel.</summary>
	public class FlightReservation: Reservation
	{
		///<summary>The airline-specific indicator of boarding order / preference.</summary>
		public Text BoardingGroup {get; set;}
	}
}
