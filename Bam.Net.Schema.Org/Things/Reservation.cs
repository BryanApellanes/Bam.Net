using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Describes a reservation for travel, dining or an event. Some reservations require tickets.</summary>
	public class Reservation: Intangible
	{
		///<summary>The date and time the reservation was booked.</summary>
		public DateTime BookingTime {get; set;}
		///<summary>An entity that arranges for an exchange between a buyer and a seller.  In most cases a broker never acquires or releases ownership of a product or service involved in an exchange.  If it is not clear whether an entity is a broker, seller, or buyer, the latter two terms are preferred. Supersedes bookingAgent.</summary>
		public OneOfThese<Organization , Person> Broker {get; set;}
		///<summary>The date and time the reservation was modified.</summary>
		public DateTime ModifiedTime {get; set;}
		///<summary>The currency (in 3-letter ISO 4217 format) of the price or a price component, when attached to PriceSpecification and its subtypes.</summary>
		public Text PriceCurrency {get; set;}
		///<summary>Any membership in a frequent flyer, hotel loyalty program, etc. being applied to the reservation.</summary>
		public ProgramMembership ProgramMembershipUsed {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization , Person> Provider {get; set;}
		///<summary>The thing -- flight, event, restaurant,etc. being reserved.</summary>
		public Thing ReservationFor {get; set;}
		///<summary>A unique identifier for the reservation.</summary>
		public Text ReservationId {get; set;}
		///<summary>The current status of the reservation.</summary>
		public ReservationStatusType ReservationStatus {get; set;}
		///<summary>A ticket associated with the reservation.</summary>
		public Ticket ReservedTicket {get; set;}
		///<summary>The total price for the reservation or ticket, including applicable taxes, shipping, etc.</summary>
		public OneOfThese<PriceSpecification , Text , Number> TotalPrice {get; set;}
		///<summary>The person or organization the reservation or ticket is for.</summary>
		public OneOfThese<Organization , Person> UnderName {get; set;}
	}
}
