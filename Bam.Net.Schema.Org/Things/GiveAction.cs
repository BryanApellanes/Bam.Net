using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of transferring ownership of an object to a destination. Reciprocal of TakeAction.Related actions:TakeAction: Reciprocal of GiveAction.SendAction: Unlike SendAction, GiveAction implies that ownership is being transferred (e.g. I may send my laptop to you, but that doesn't mean I'm giving it to you).</summary>
	public class GiveAction: TransferAction
	{
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Person , Audience , Organization> Recipient {get; set;}
	}
}
