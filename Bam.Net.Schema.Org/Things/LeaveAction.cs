using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An agent leaves an event / group with participants/friends at a location.Related actions:JoinAction: The antonym of LeaveAction.UnRegisterAction: Unlike UnRegisterAction, LeaveAction implies leaving a group/team of people rather than a service.</summary>
	public class LeaveAction: InteractAction
	{
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
	}
}
