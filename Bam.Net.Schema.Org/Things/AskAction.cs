using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of posing a question / favor to someone.Related actions:ReplyAction: Appears generally as a response to AskAction.</summary>
	public class AskAction: CommunicateAction
	{
		///<summary>A sub property of object. A question.</summary>
		public Question Question {get; set;}
	}
}
