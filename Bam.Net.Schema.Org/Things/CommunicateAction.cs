/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of conveying information to another person via a communication medium (instrument) such as speech, email, or telephone conversation.</summary>
	public class CommunicateAction: InteractAction
	{
		///<summary>The subject matter of the content.</summary>
		public Thing About {get; set;}
		///<summary>A sub property of instrument. The language used on this action.</summary>
		public Language Language {get; set;}
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public ThisOrThat<Person , Organization , Audience> Recipient {get; set;}
	}
}
