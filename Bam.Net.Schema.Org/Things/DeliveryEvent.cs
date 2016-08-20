using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An event involving the delivery of an item.</summary>
	public class DeliveryEvent: Event
	{
		///<summary>Password, PIN, or access code needed for delivery (e.g. from a locker).</summary>
		public Text AccessCode {get; set;}
		///<summary>When the item is available for pickup from the store, locker, etc.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date AvailableFrom {get; set;}
		///<summary>After this date, the item will no longer be available for pickup.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date AvailableThrough {get; set;}
		///<summary>Method used for delivery or shipping.</summary>
		public DeliveryMethod HasDeliveryMethod {get; set;}
	}
}
