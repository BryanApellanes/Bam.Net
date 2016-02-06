using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of notifying someone of information pertinent to them, with no expectation of a response.</summary>
	public class InformAction: CommunicateAction
	{
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
	}
}
