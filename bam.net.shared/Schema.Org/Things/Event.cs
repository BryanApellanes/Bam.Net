using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An event happening at a certain time and location, such as a concert, lecture, or festival. Ticketing information may be added via the offers property. Repeated events may be structured as separate Event objects.</summary>
	public class Event: Thing
	{
		///<summary>An actor, e.g. in tv, radio, movie, video games etc., or in an event. Actors can be associated with individual items or with a series, episode, clip. Supersedes actors.</summary>
		public Person Actor {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>A person or organization attending the event. Supersedes attendees.</summary>
		public OneOfThese<Organization,Person> Attendee {get; set;}
		///<summary>The person or organization who wrote a composition, or who is the composer of a work performed at some event.</summary>
		public OneOfThese<Organization,Person> Composer {get; set;}
		///<summary>A secondary contributor to the CreativeWork or Event.</summary>
		public OneOfThese<Organization,Person> Contributor {get; set;}
		///<summary>A director of e.g. tv, radio, movie, video gaming etc. content, or of an event. Directors can be associated with individual items or with a series, episode, clip. Supersedes directors.</summary>
		public Person Director {get; set;}
		///<summary>The time admission will commence.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date DoorTime {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public OneOfThese<Bam.Net.Schema.Org.DataTypes.Date,Bam.Net.Schema.Org.DataTypes.Date> EndDate {get; set;}
		///<summary>An eventStatus of an event represents its status; particularly useful when an event is cancelled or rescheduled.</summary>
		public EventStatusType EventStatus {get; set;}
		///<summary>A person or organization that supports (sponsors) something through some kind of financial contribution.</summary>
		public OneOfThese<Organization,Person> Funder {get; set;}
		///<summary>The language of the content or performance or used in an action. Please use one of the language codes from the IETF BCP 47 standard. See also availableLanguage. Supersedes language.</summary>
		public OneOfThese<Language,Text> InLanguage {get; set;}
		///<summary>A flag to signal that the publication is accessible for free. Supersedes free.</summary>
		public Boolean IsAccessibleForFree {get; set;}
		///<summary>The location of for example where the event is happening, an organization is located, or where an action takes place.</summary>
		public OneOfThese<Place,PostalAddress,Text> Location {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, perform a service, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>An organizer of an Event.</summary>
		public OneOfThese<Organization,Person> Organizer {get; set;}
		///<summary>A performer at the event—for example, a presenter, musician, musical group or actor. Supersedes performers.</summary>
		public OneOfThese<Organization,Person> Performer {get; set;}
		///<summary>Used in conjunction with eventStatus for rescheduled or cancelled events. This property contains the previously scheduled start date. For rescheduled events, the startDate property should be used for the newly scheduled start date. In the (rare) case of an event that has been postponed and rescheduled multiple times, this field may be repeated.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date PreviousStartDate {get; set;}
		///<summary>The CreativeWork that captured all or part of this Event. Inverse property: recordedAt.</summary>
		public CreativeWork RecordedIn {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>A person or organization that supports a thing through a pledge, promise, or financial contribution. e.g. a sponsor of a Medical Study or a corporate sponsor of an event.</summary>
		public OneOfThese<Organization,Person> Sponsor {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public OneOfThese<Bam.Net.Schema.Org.DataTypes.Date,Bam.Net.Schema.Org.DataTypes.Date> StartDate {get; set;}
		///<summary>An Event that is part of this event. For example, a conference event includes many presentations, each of which is a subEvent of the conference. Supersedes subEvents. Inverse property: superEvent.</summary>
		public Event SubEvent {get; set;}
		///<summary>An event that this event is a part of. For example, a collection of individual music performances might each have a music festival as their superEvent. Inverse property: subEvent.</summary>
		public Event SuperEvent {get; set;}
		///<summary>Organization or person who adapts a creative work to different languages, regional differences and technical requirements of a target market, or that translates during some event.</summary>
		public OneOfThese<Organization,Person> Translator {get; set;}
		///<summary>The typical expected age range, e.g. '7-9', '11-'.</summary>
		public Text TypicalAgeRange {get; set;}
		///<summary>A work featured in some event, e.g. exhibited in an ExhibitionEvent.       Specific subproperties are available for workPerformed (e.g. a play), or a workPresented (a Movie at a ScreeningEvent).</summary>
		public CreativeWork WorkFeatured {get; set;}
		///<summary>A work performed in some event, for example a play performed in a TheaterEvent.</summary>
		public CreativeWork WorkPerformed {get; set;}
	}
}
