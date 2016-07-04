using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An over the air or online broadcast event.</summary>
	public class BroadcastEvent: PublicationEvent
	{
		///<summary>The event being broadcast such as a sporting event or awards ceremony.</summary>
		public Event BroadcastOfEvent {get; set;}
		///<summary>True is the broadcast is of a live event.</summary>
		public Boolean IsLiveBroadcast {get; set;}
	}
}
