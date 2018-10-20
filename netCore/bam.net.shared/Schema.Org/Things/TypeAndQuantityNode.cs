using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A structured value indicating the quantity, unit of measurement, and business function of goods included in a bundle offer.</summary>
	public class TypeAndQuantityNode: StructuredValue
	{
		///<summary>The quantity of the goods included in the offer.</summary>
		public Number AmountOfThisGood {get; set;}
		///<summary>The business function (e.g. sell, lease, repair, dispose) of the offer or component of a bundle (TypeAndQuantityNode). The default is http://purl.org/goodrelations/v1#Sell.</summary>
		public BusinessFunction BusinessFunction {get; set;}
		///<summary>The product that this structured value is referring to.</summary>
		public OneOfThese<Product,Service> TypeOfGood {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters) or a URL. Other codes than the UN/CEFACT Common Code may be used with a prefix followed by a colon.</summary>
		public OneOfThese<Text,Url> UnitCode {get; set;}
		///<summary>A string or text indicating the unit of measurement. Useful if you cannot provide a standard unit code forunitCode.</summary>
		public Text UnitText {get; set;}
	}
}
