using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The costs of settling the payment using a particular payment method.</summary>
	public class PaymentChargeSpecification: PriceSpecification
	{
		///<summary>The delivery method(s) to which the delivery charge or payment charge specification applies.</summary>
		public DeliveryMethod AppliesToDeliveryMethod {get; set;}
		///<summary>The payment method(s) to which the payment charge specification applies.</summary>
		public PaymentMethod AppliesToPaymentMethod {get; set;}
	}
}
