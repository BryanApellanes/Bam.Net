using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The price for the delivery of an offer using a particular delivery method.</summary>
	public class DeliveryChargeSpecification: PriceSpecification
	{
		///<summary>The delivery method(s) to which the delivery charge or payment charge specification applies.</summary>
		public DeliveryMethod AppliesToDeliveryMethod {get; set;}
		///<summary>The geographic area where a service or offered item is provided. Supersedes serviceArea.</summary>
		public OneOfThese<AdministrativeArea,GeoShape,Place,Text> AreaServed {get; set;}
		///<summary>The ISO 3166-1 (ISO 3166-1 alpha-2) or ISO 3166-2 code, the place, or the GeoShape for the geo-political region(s) for which the offer or delivery charge specification is valid.See also ineligibleRegion.</summary>
		public OneOfThese<GeoShape,Place,Text> EligibleRegion {get; set;}
		///<summary>The ISO 3166-1 (ISO 3166-1 alpha-2) or ISO 3166-2 code, the place, or the GeoShape for the geo-political region(s) for which the offer or delivery charge specification is not valid, e.g. a region where the transaction is not allowed.See also eligibleRegion.</summary>
		public OneOfThese<GeoShape,Place,Text> IneligibleRegion {get; set;}
	}
}
