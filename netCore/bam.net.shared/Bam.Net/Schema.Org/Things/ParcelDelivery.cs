using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The delivery of a parcel either via the postal service or a commercial service.</summary>
	public class ParcelDelivery: Intangible
	{
		///<summary>Destination address.</summary>
		public PostalAddress DeliveryAddress {get; set;}
		///<summary>New entry added as the package passes through each leg of its journey (from shipment to final delivery).</summary>
		public DeliveryEvent DeliveryStatus {get; set;}
		///<summary>The earliest date the package may arrive.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ExpectedArrivalFrom {get; set;}
		///<summary>The latest date the package may arrive.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ExpectedArrivalUntil {get; set;}
		///<summary>Method used for delivery or shipping.</summary>
		public DeliveryMethod HasDeliveryMethod {get; set;}
		///<summary>Item(s) being shipped.</summary>
		public Product ItemShipped {get; set;}
		///<summary>Shipper's address.</summary>
		public PostalAddress OriginAddress {get; set;}
		///<summary>The overall order the items in this delivery were included in.</summary>
		public Order PartOfOrder {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public OneOfThese<Organization,Person> Provider {get; set;}
		///<summary>Shipper tracking number.</summary>
		public Text TrackingNumber {get; set;}
		///<summary>Tracking url for the parcel delivery.</summary>
		public Url TrackingUrl {get; set;}
	}
}
