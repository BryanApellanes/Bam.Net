using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A single message from a sender to one or more organizations or people.</summary>
	public class Message: CreativeWork
	{
		///<summary>The date/time at which the message has been read by the recipient if a single recipient exists.</summary>
		public DateTime DateRead {get; set;}
		///<summary>The date/time the message was received if a single recipient exists.</summary>
		public DateTime DateReceived {get; set;}
		///<summary>The date/time at which the message was sent.</summary>
		public DateTime DateSent {get; set;}
		///<summary>A CreativeWork attached to the message.</summary>
		public CreativeWork MessageAttachment {get; set;}
		///<summary>A sub property of participant. The participant who is at the receiving end of the action.</summary>
		public OneOfThese<Audience , Organization , Person> Recipient {get; set;}
		///<summary>A sub property of participant. The participant who is at the sending end of the action.</summary>
		public OneOfThese<Audience , Organization , Person> Sender {get; set;}
	}
}
