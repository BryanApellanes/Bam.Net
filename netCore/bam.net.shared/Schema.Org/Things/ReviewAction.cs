using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of producing a balanced opinion about the object for an audience. An agent reviews an object with participants resulting in a review.</summary>
	public class ReviewAction: AssessAction
	{
		///<summary>A sub property of result. The review that resulted in the performing of the action.</summary>
		public Review ResultReview {get; set;}
	}
}
