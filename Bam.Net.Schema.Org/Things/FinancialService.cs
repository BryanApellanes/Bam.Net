using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Financial services business.</summary>
	public class FinancialService: LocalBusiness
	{
		///<summary>Description of fees, commissions, and other terms applied either to a class of financial product, or by a financial service organization.</summary>
		public OneOfThese<Text , URL> FeesAndCommissionsSpecification {get; set;}
	}
}
