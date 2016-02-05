using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A particular physical business or branch of an organization. Examples of LocalBusiness include a restaurant, a particular branch of a restaurant chain, a branch of a bank, a medical practice, a club, a bowling alley, etc.</summary>
	public class LocalBusiness: Place
	{
		///<summary>A short textual code (also called "store code") that uniquely identifies a place of business. The code is typically assigned by the parentOrganization and used in structured URLs. For example, in the URL http://www.starbucks.co.uk/store-locator/etc/detail/3047 the code "3047" is a branchCode for a particular branch.</summary>
		public Text BranchCode {get; set;}
		///<summary>The currency accepted (in ISO 4217 currency format).</summary>
		public Text CurrenciesAccepted {get; set;}
		///<summary>The opening hours for a business. Opening hours can be specified as a weekly time range, starting with days, then times per day. Multiple days can be listed with commas ',' separating each day. Day or time ranges are specified using a hyphen '-'.- Days are specified using the following two-letter combinations: Mo, Tu, We, Th, Fr, Sa, Su.- Times are specified using 24:00 time. For example, 3pm is specified as 15:00. - Here is an example: <time itemprop="openingHours" datetime="Tu,Th 16:00-20:00">Tuesdays and Thursdays 4-8pm</time>. - If a business is open 7 days a week, then it can be specified as <time itemprop="openingHours" datetime="Mo-Su">Monday through Sunday, all day</time>.</summary>
		public Text OpeningHours {get; set;}
		///<summary>Cash, credit card, etc.</summary>
		public Text PaymentAccepted {get; set;}
		///<summary>The price range of the business, for example $$$.</summary>
		public Text PriceRange {get; set;}
	}
}
