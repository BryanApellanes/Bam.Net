using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of expressing a preference from a set of options or a large or unbounded set of choices/options.</summary>
	public class ChooseAction: AssessAction
	{
		///<summary>A sub property of object. The options subject to this action. Supersedes option.</summary>
		public OneOfThese<Text,Thing> ActionOption {get; set;}
	}
}
