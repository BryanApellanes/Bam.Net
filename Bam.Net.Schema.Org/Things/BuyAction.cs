using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of giving money to a seller in exchange for goods or services rendered. An agent buys an object, product, or service from a seller for a price. Reciprocal of SellAction.</summary>
	public class BuyAction: TradeAction
	{
		///<summary>An entity which offers (sells / leases / lends / loans) the services / goods.  A seller may also be a provider. Supersedes vendor, merchant.</summary>
		public ThisOrThat<Organization , Person> Seller {get; set;}
	}
}
