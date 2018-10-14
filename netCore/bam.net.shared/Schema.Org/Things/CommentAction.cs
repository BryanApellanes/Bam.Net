using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of generating a comment about a subject.</summary>
	public class CommentAction: CommunicateAction
	{
		///<summary>A sub property of result. The Comment created or sent as a result of this action.</summary>
		public Comment ResultComment {get; set;}
	}
}
