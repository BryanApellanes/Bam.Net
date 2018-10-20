using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of responding to a question/message asked/sent by the object. Related to AskActionRelated actions:AskAction: Appears generally as an origin of a ReplyAction.</summary>
	public class ReplyAction: CommunicateAction
	{
		///<summary>A sub property of result. The Comment created or sent as a result of this action.</summary>
		public Comment ResultComment {get; set;}
	}
}
