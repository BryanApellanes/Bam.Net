using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A type of financial product that typically requires the client to transfer funds to a financial service in return for potential beneficial financial return.</summary>
	public class InvestmentOrDeposit: FinancialProduct
	{
		///<summary>The amount of money.</summary>
		public OneOfThese<MonetaryAmount , Number> Amount {get; set;}
	}
}
