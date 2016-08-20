using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An agent orders an object/product/service to be delivered/sent.</summary>
	public class OrderAction: TradeAction
	{
		///<summary>A sub property of instrument. The method of delivery.</summary>
		public DeliveryMethod DeliveryMethod {get; set;}
	}
}
