/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An agent leaves an event / group with participants/friends at a location.Related actions:JoinAction: The antagonym of LeaveAction.UnRegisterAction: Unlike UnRegisterAction, LeaveAction implies leaving a group/team of people rather than a service.</summary>
	public class LeaveAction: InteractAction
	{
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
	}
}
