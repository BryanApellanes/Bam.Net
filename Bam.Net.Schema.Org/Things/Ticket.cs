/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Used to describe a ticket to an event, a flight, a bus ride, etc.</summary>
	public class Ticket: Intangible
	{
		///<summary>The date the ticket was issued.</summary>
		public DateTime DateIssued {get; set;}
		///<summary>The organization issuing the ticket or permit.</summary>
		public Organization IssuedBy {get; set;}
		///<summary>The currency (in 3-letter ISO 4217 format) of the price or a price component, when attached to PriceSpecification and its subtypes.</summary>
		public Text PriceCurrency {get; set;}
		///<summary>The unique identifier for the ticket.</summary>
		public Text TicketNumber {get; set;}
		///<summary>Reference to an asset (e.g., Barcode, QR code image or PDF) usable for entrance.</summary>
		public ThisOrThat<URL , Text> TicketToken {get; set;}
		///<summary>The seat associated with the ticket.</summary>
		public Seat TicketedSeat {get; set;}
		///<summary>The total price for the reservation or ticket, including applicable taxes, shipping, etc.</summary>
		public ThisOrThat<Number , Text , PriceSpecification> TotalPrice {get; set;}
		///<summary>The person or organization the reservation or ticket is for.</summary>
		public ThisOrThat<Person , Organization> UnderName {get; set;}
	}
}
