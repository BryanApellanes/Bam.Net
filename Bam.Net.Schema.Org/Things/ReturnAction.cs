using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of returning to the origin that which was previously received (concrete objects) or taken (ownership).</summary>
	public class ReturnAction: TransferAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Person , Organization , Audience> Recipient {get; set;}
	}
}
