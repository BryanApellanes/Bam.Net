using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A summary of how users have interacted with this CreativeWork. In most cases, authors will use a subtype to specify the specific type of interaction.</summary>
	public class InteractionCounter: StructuredValue
	{
		///<summary>The WebSite or SoftwareApplication where the interactions took place.</summary>
		public OneOfThese<SoftwareApplication,WebSite> InteractionService {get; set;}
		///<summary>The Action representing the type of interaction. For up votes, +1s, etc. use LikeAction. For down votes use DislikeAction. Otherwise, use the most specific Action.</summary>
		public Action InteractionType {get; set;}
		///<summary>The number of interactions for the CreativeWork using the WebSite or SoftwareApplication.</summary>
		public Integer UserInteractionCount {get; set;}
	}
}
