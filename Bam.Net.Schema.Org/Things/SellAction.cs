/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of taking money from a buyer in exchange for goods or services rendered. An agent sells an object, product, or service to a buyer for a price. Reciprocal of BuyAction.</summary>
	public class SellAction: TradeAction
	{
		///<summary>A sub property of participant. The participant/person/organization that bought the object.</summary>
		public Person Buyer {get; set;}
		///<summary>The warranty promise(s) included in the offer.</summary>
		public WarrantyPromise WarrantyPromise {get; set;}
	}
}
