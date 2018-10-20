using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of managing by changing/editing the state of the object.</summary>
	public class UpdateAction: Action
	{
		///<summary>A sub property of object. The collection target of the action. Supersedes collection.</summary>
		public Thing TargetCollection {get; set;}
	}
}
