/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent pays a price to a participant.</summary>
	public class PayAction: TradeAction
	{
		///<summary>A goal towards an action is taken. Can be concrete or abstract.</summary>
		public ThisOrThat<MedicalDevicePurpose , Thing> Purpose {get; set;}
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public ThisOrThat<Person , Organization , Audience> Recipient {get; set;}
	}
}
