using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A financial product for the loaning of an amount of money under agreed terms and charges.</summary>
	public class LoanOrCredit: FinancialProduct
	{
		///<summary>The amount of money.</summary>
		public OneOfThese<MonetaryAmount,Number> Amount {get; set;}
		///<summary>The duration of the loan or credit agreement.</summary>
		public QuantitativeValue LoanTerm {get; set;}
		///<summary>Assets required to secure loan or credit repayments. It may take form of third party pledge, goods, financial instruments (cash, securities, etc.)</summary>
		public OneOfThese<Text,Thing> RequiredCollateral {get; set;}
	}
}
