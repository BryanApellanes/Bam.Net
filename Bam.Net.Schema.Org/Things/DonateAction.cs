/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of providing goods, services, or money without compensation, often for philanthropic reasons.</summary>
	public class DonateAction: TradeAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Person , Organization , Audience> Recipient {get; set;}
	}
}
