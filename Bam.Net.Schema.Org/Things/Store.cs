using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A retail good store.</summary>
	public class Store: LocalBusiness
	{
		///<summary>A short textual code (also called "store code") that uniquely identifies a place of business. The code is typically assigned by the parentOrganization and used in structured URLs. For example, in the URL http://www.starbucks.co.uk/store-locator/etc/detail/3047 the code "3047" is a branchCode for a particular branch.</summary>
		public Text BranchCode {get; set;}
	}
}
