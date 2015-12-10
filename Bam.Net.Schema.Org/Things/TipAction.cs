/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of giving money voluntarily to a beneficiary in recognition of services rendered.</summary>
	public class TipAction: TradeAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public ThisOrThat<Person , Organization , Audience> Recipient {get; set;}
	}
}
