using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An order item is a line of an order. It includes the quantity and shipping details of a bought offer.</summary>
	public class OrderItem: Intangible
	{
		///<summary>The delivery of the parcel related to this order or order item.</summary>
		public ParcelDelivery OrderDelivery {get; set;}
		///<summary>The identifier of the order item.</summary>
		public Text OrderItemNumber {get; set;}
		///<summary>The current status of the order item.</summary>
		public OrderStatus OrderItemStatus {get; set;}
		///<summary>The number of the item ordered. If the property is not set, assume the quantity is one.</summary>
		public Number OrderQuantity {get; set;}
		///<summary>The item ordered.</summary>
		public OneOfThese<OrderItem,Product> OrderedItem {get; set;}
	}
}
