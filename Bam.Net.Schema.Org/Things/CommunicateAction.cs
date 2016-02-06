using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of conveying information to another person via a communication medium (instrument) such as speech, email, or telephone conversation.</summary>
	public class CommunicateAction: InteractAction
	{
		///<summary>The subject matter of the content.</summary>
		public Thing About {get; set;}
		///<summary>The language of the content or performance or used in an action. Please use one of the language codes from the IETF BCP 47 standard. Supersedes language.</summary>
		public ThisOrThat<Text , Language> InLanguage {get; set;}
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Person , Organization , Audience> Recipient {get; set;}
	}
}
