using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The price for the delivery of an offer using a particular delivery method.</summary>
	public class DeliveryChargeSpecification: PriceSpecification
	{
		///<summary>The delivery method(s) to which the delivery charge or payment charge specification applies.</summary>
		public DeliveryMethod AppliesToDeliveryMethod {get; set;}
		///<summary>The geographic area where a service or offered item is provided. Supersedes serviceArea.</summary>
		public AdministrativeArea  or  Place  or  Text  or  GeoShape AreaServed {get; set;}
		///<summary>The ISO 3166-1 (ISO 3166-1 alpha-2) or ISO 3166-2 code, the place, or the GeoShape for the geo-political region(s) for which the offer or delivery charge specification is valid.       See also ineligibleRegion.</summary>
		public ThisOrThat<Place , Text , GeoShape> EligibleRegion {get; set;}
		///<summary>The ISO 3166-1 (ISO 3166-1 alpha-2) or ISO 3166-2 code, the place, or the GeoShape for the geo-political region(s) for which the offer or delivery charge specification is not valid, e.g. a region where the transaction is not allowed.       See also eligibleRegion.</summary>
		public ThisOrThat<Place , Text , GeoShape> IneligibleRegion {get; set;}
	}
}
