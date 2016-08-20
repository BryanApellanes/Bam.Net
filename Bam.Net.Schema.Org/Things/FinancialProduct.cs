using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A product provided to consumers and businesses by financial institutions such as banks, insurance companies, brokerage firms, consumer finance companies, and investment companies which comprise the financial services industry.</summary>
	public class FinancialProduct: Service
	{
		///<summary>The annual rate that is charged for borrowing (or made by investing), expressed as a single percentage number that represents the actual yearly cost of funds over the term of a loan. This includes any fees or additional costs associated with the transaction.</summary>
		public OneOfThese<Number,QuantitativeValue> AnnualPercentageRate {get; set;}
		///<summary>Description of fees, commissions, and other terms applied either to a class of financial product, or by a financial service organization.</summary>
		public OneOfThese<Text,Url> FeesAndCommissionsSpecification {get; set;}
		///<summary>The interest rate, charged or paid, applicable to the financial product. Note: This is different from the calculated annualPercentageRate.</summary>
		public OneOfThese<Number,QuantitativeValue> InterestRate {get; set;}
	}
}
