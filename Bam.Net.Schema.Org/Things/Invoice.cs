using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A statement of the money due for goods or services; a bill.</summary>
	public class Invoice: Intangible
	{
		///<summary>The identifier for the account the payment will be applied to.</summary>
		public Text AccountId {get; set;}
		///<summary>The time interval used to compute the invoice.</summary>
		public Duration BillingPeriod {get; set;}
		///<summary>An entity that arranges for an exchange between a buyer and a seller.  In most cases a broker never acquires or releases ownership of a product or service involved in an exchange.  If it is not clear whether an entity is a broker, seller, or buyer, the latter two terms are preferred. Supersedes bookingAgent.</summary>
		public OneOfThese<Organization,Person> Broker {get; set;}
		///<summary>A category for the item. Greater signs or slashes can be used to informally indicate a category hierarchy.</summary>
		public OneOfThese<Text,Thing> Category {get; set;}
		///<summary>A number that confirms the given order or payment has been received.</summary>
		public Text ConfirmationNumber {get; set;}
		///<summary>Party placing the order or paying the invoice.</summary>
		public OneOfThese<Organization,Person> Customer {get; set;}
		///<summary>The minimum payment required at this time.</summary>
		public OneOfThese<MonetaryAmount,PriceSpecification> MinimumPaymentDue {get; set;}
		///<summary>The date that payment is due. Supersedes paymentDue.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date PaymentDueDate {get; set;}
		///<summary>The name of the credit card or other method of payment for the order.</summary>
		public PaymentMethod PaymentMethod {get; set;}
		///<summary>An identifier for the method of payment used (e.g. the last 4 digits of the credit card).</summary>
		public Text PaymentMethodId {get; set;}
		///<summary>The status of payment; whether the invoice has been paid or not.</summary>
		public OneOfThese<PaymentStatusType,Text> PaymentStatus {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization,Person> Provider {get; set;}
		///<summary>The Order(s) related to this Invoice. One or more Orders may be combined into a single Invoice.</summary>
		public Order ReferencesOrder {get; set;}
		///<summary>The date the invoice is scheduled to be paid.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ScheduledPaymentDate {get; set;}
		///<summary>The total amount due.</summary>
		public OneOfThese<MonetaryAmount,PriceSpecification> TotalPaymentDue {get; set;}
	}
}
