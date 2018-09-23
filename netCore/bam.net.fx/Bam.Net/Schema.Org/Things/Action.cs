using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An action performed by a direct agent and indirect participants upon a direct object. Optionally happens at a location with the help of an inanimate instrument. The execution of the action may produce a result. Specific action sub-type documentation specifies the exact expectation of each argument/role.See also blog post and Actions overview document.</summary>
	public class Action: Thing
	{
		///<summary>Indicates the current disposition of the Action.</summary>
		public ActionStatusType ActionStatus {get; set;}
		///<summary>The direct performer or driver of the action (animate or inanimate). e.g. John wrote a book.</summary>
		public OneOfThese<Organization,Person> Agent {get; set;}
		///<summary>The endTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to end. For actions that span a period of time, when the action was performed. e.g. John wrote a book from January to December.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date EndTime {get; set;}
		///<summary>For failed actions, more information on the cause of the failure.</summary>
		public Thing Error {get; set;}
		///<summary>The object that helped the agent perform the action. e.g. John wrote a book with a pen.</summary>
		public Thing Instrument {get; set;}
		///<summary>The location of for example where the event is happening, an organization is located, or where an action takes place.</summary>
		public OneOfThese<Place,PostalAddress,Text> Location {get; set;}
		///<summary>The object upon the action is carried out, whose state is kept intact or changed. Also known as the semantic roles patient, affected or undergoer (which change their state) or theme (which doesn't). e.g. John read a book.</summary>
		public Thing Object {get; set;}
		///<summary>Other co-agents that participated in the action indirectly. e.g. John wrote a book with Steve.</summary>
		public OneOfThese<Organization,Person> Participant {get; set;}
		///<summary>The result produced in the action. e.g. John wrote a book.</summary>
		public Thing Result {get; set;}
		///<summary>The startTime of something. For a reserved event or service (e.g. FoodEstablishmentReservation), the time that it is expected to start. For actions that span a period of time, when the action was performed. e.g. John wrote a book from January to December.Note that Event uses startDate/endDate instead of startTime/endTime, even when describing dates with times. This situation may be clarified in future revisions.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date StartTime {get; set;}
		///<summary>Indicates a target EntryPoint for an Action.</summary>
		public EntryPoint Target {get; set;}
	}
}
