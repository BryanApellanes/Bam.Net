using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An over the air or online broadcast event.</summary>
	public class BroadcastEvent: PublicationEvent
	{
		///<summary>The event being broadcast such as a sporting event or awards ceremony.</summary>
		public Event BroadcastOfEvent {get; set;}
		///<summary>True is the broadcast is of a live event.</summary>
		public Boolean IsLiveBroadcast {get; set;}
		///<summary>The type of screening or video broadcast used (e.g. IMAX, 3D, SD, HD, etc.).</summary>
		public Text VideoFormat {get; set;}
	}
}
