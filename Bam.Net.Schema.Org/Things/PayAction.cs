using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent pays a price to a participant.</summary>
	public class PayAction: TradeAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Audience , Organization , Person> Recipient {get; set;}
	}
}
