using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of an agent relocating to a place.Related actions:TransferAction: Unlike TransferAction, the subject of the move is a living Person or Organization rather than an inanimate object.</summary>
	public class MoveAction: Action
	{
		///<summary>A sub property of location. The original location of the object or the agent before the action.</summary>
		public Place FromLocation {get; set;}
		///<summary>A sub property of location. The final location of the object or the agent after the action.</summary>
		public Place ToLocation {get; set;}
	}
}
