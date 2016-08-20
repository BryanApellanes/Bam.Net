using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of transferring/moving (abstract or concrete) animate or inanimate objects from one place to another.</summary>
	public class TransferAction: Action
	{
		///<summary>A sub property of location. The original location of the object or the agent before the action.</summary>
		public Place FromLocation {get; set;}
		///<summary>A sub property of location. The final location of the object or the agent after the action.</summary>
		public Place ToLocation {get; set;}
	}
}
