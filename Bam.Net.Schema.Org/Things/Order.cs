/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An order is a confirmation of a transaction (a receipt), which can contain multiple line items, each represented by an Offer that has been accepted by the customer.</summary>
	public class Order: Intangible
	{
		///<summary>The offer(s) -- e.g., product, quantity and price combinations -- included in the order.</summary>
		public Offer AcceptedOffer {get; set;}
		///<summary>The billing address for the order.</summary>
		public PostalAddress BillingAddress {get; set;}
		///<summary>An entity that arranges for an exchange between a buyer and a seller.  In most cases a broker never acquires or releases ownership of a product or service involved in an exchange.  If it is not clear whether an entity is a broker, seller, or buyer, the latter two terms are preferred. Supersedes bookingAgent.</summary>
		public OneOfThese<Person , Organization> Broker {get; set;}
		///<summary>A number that confirms the given order or payment has been received.</summary>
		public Text ConfirmationNumber {get; set;}
		///<summary>Party placing the order or paying the invoice.</summary>
		public OneOfThese<Person , Organization> Customer {get; set;}
		///<summary>Any discount applied (to an Order).</summary>
		public OneOfThese<Number , Text> Discount {get; set;}
		///<summary>Code used to redeem a discount.</summary>
		public Text DiscountCode {get; set;}
		///<summary>The currency (in 3-letter ISO 4217 format) of the discount.</summary>
		public Text DiscountCurrency {get; set;}
		///<summary>Was the offer accepted as a gift for someone other than the buyer.</summary>
		public Boolean IsGift {get; set;}
		///<summary>Date order was placed.</summary>
		public DateTime OrderDate {get; set;}
		///<summary>The identifier of the transaction.</summary>
		public Text OrderNumber {get; set;}
		///<summary>The current status of the order.</summary>
		public OrderStatus OrderStatus {get; set;}
		///<summary>The item ordered.</summary>
		public Product OrderedItem {get; set;}
		///<summary>The order is being paid as part of the referenced Invoice.</summary>
		public Invoice PartOfInvoice {get; set;}
		///<summary>The date that payment is due.</summary>
		public DateTime PaymentDue {get; set;}
		///<summary>The name of the credit card or other method of payment for the order.</summary>
		public PaymentMethod PaymentMethod {get; set;}
		///<summary>An identifier for the method of payment used (e.g. the last 4 digits of the credit card).</summary>
		public Text PaymentMethodId {get; set;}
		///<summary>The URL for sending a payment.</summary>
		public URL PaymentUrl {get; set;}
		///<summary>An entity which offers (sells / leases / lends / loans) the services / goods.  A seller may also be a provider. Supersedes merchant, vendor.</summary>
		public OneOfThese<Person , Organization> Seller {get; set;}
	}
}
