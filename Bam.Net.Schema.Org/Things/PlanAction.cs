using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of planning the execution of an event/task/action/reservation/plan to a future date.</summary>
	public class PlanAction: OrganizeAction
	{
		///<summary>The time the object is scheduled to.</summary>
		public DateTime ScheduledTime {get; set;}
	}
}
