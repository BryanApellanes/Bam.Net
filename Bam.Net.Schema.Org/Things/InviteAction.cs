using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of asking someone to attend an event. Reciprocal of RsvpAction.</summary>
	public class InviteAction: CommunicateAction
	{
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
	}
}
