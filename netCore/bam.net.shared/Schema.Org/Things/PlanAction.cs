using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of planning the execution of an event/task/action/reservation/plan to a future date.</summary>
	public class PlanAction: OrganizeAction
	{
		///<summary>The time the object is scheduled to.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date ScheduledTime {get; set;}
	}
}
