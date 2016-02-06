using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent pays a price to a participant.</summary>
	public class PayAction: TradeAction
	{
		///<summary>A goal towards an action is taken. Can be concrete or abstract.</summary>
		public OneOfThese<Thing , MedicalDevicePurpose> Purpose {get; set;}
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Person , Audience , Organization> Recipient {get; set;}
	}
}
