using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The event has been rescheduled. The event's previousStartDate should be set to the old date and the startDate should be set to the event's new date. (If the event has been rescheduled multiple times, the previousStartDate property may be repeated).</summary>
	public class EventRescheduled: EventStatusType
	{
	}
}
