/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent orders an object/product/service to be delivered/sent.</summary>
	public class OrderAction: TradeAction
	{
		///<summary>A sub property of instrument. The method of delivery.</summary>
		public DeliveryMethod DeliveryMethod {get; set;}
	}
}
