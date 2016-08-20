using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of expressing a preference from a fixed/finite/structured set of choices/options.</summary>
	public class VoteAction: ChooseAction
	{
		///<summary>A sub property of object. The candidate subject of this action.</summary>
		public Person Candidate {get; set;}
	}
}
