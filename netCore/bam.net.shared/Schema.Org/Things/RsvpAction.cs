using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of notifying an event organizer as to whether you expect to attend the event.</summary>
	public class RsvpAction: InformAction
	{
		///<summary>If responding yes, the number of guests who will attend in addition to the invitee.</summary>
		public Number AdditionalNumberOfGuests {get; set;}
		///<summary>Comments, typically from users.</summary>
		public Comment Comment {get; set;}
		///<summary>The response (yes, no, maybe) to the RSVP.</summary>
		public RsvpResponseType RsvpResponse {get; set;}
	}
}
