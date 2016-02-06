/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A structured value indicating the quantity, unit of measurement, and business function of goods included in a bundle offer.</summary>
	public class TypeAndQuantityNode: StructuredValue
	{
		///<summary>The quantity of the goods included in the offer.</summary>
		public Number AmountOfThisGood {get; set;}
		///<summary>The business function (e.g. sell, lease, repair, dispose) of the offer or component of a bundle (TypeAndQuantityNode). The default is http://purl.org/goodrelations/v1#Sell.</summary>
		public BusinessFunction BusinessFunction {get; set;}
		///<summary>The product that this structured value is referring to.</summary>
		public Product TypeOfGood {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters).</summary>
		public Text UnitCode {get; set;}
	}
}
