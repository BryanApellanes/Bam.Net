using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An event happening at a certain time and location, such as a concert, lecture, or festival. Ticketing information may be added via the 'offers' property. Repeated events may be structured as separate Event objects.</summary>
	public class Event: Thing
	{
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>A person or organization attending the event. Supersedes attendees.</summary>
		public OneOfThese<Person , Organization> Attendee {get; set;}
		///<summary>The time admission will commence.</summary>
		public DateTime DoorTime {get; set;}
		///<summary>The duration of the item (movie, audio recording, event, etc.) in ISO 8601 date format.</summary>
		public Duration Duration {get; set;}
		///<summary>The end date and time of the item (in ISO 8601 date format).</summary>
		public Date EndDate {get; set;}
		///<summary>An eventStatus of an event represents its status; particularly useful when an event is cancelled or rescheduled.</summary>
		public EventStatusType EventStatus {get; set;}
		///<summary>The language of the content or performance or used in an action. Please use one of the language codes from the IETF BCP 47 standard. Supersedes language.</summary>
		public OneOfThese<Language , Text> InLanguage {get; set;}
		///<summary>The location of for example where the event is happening, an organization is located, or where an action takes place.</summary>
		public OneOfThese<PostalAddress , Place , Text> Location {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, perform a service, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>An organizer of an Event.</summary>
		public OneOfThese<Person , Organization> Organizer {get; set;}
		///<summary>A performer at the event—for example, a presenter, musician, musical group or actor. Supersedes performers.</summary>
		public OneOfThese<Person , Organization> Performer {get; set;}
		///<summary>Used in conjunction with eventStatus for rescheduled or cancelled events. This property contains the previously scheduled start date. For rescheduled events, the startDate property should be used for the newly scheduled start date. In the (rare) case of an event that has been postponed and rescheduled multiple times, this field may be repeated.</summary>
		public Date PreviousStartDate {get; set;}
		///<summary>The CreativeWork that captured all or part of this Event. Inverse property: recordedAt.</summary>
		public CreativeWork RecordedIn {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The start date and time of the item (in ISO 8601 date format).</summary>
		public Date StartDate {get; set;}
		///<summary>An Event that is part of this event. For example, a conference event includes many presentations, each of which is a subEvent of the conference. Supersedes subEvents.</summary>
		public Event SubEvent {get; set;}
		///<summary>An event that this event is a part of. For example, a collection of individual music performances might each have a music festival as their superEvent.</summary>
		public Event SuperEvent {get; set;}
		///<summary>The typical expected age range, e.g. '7-9', '11-'.</summary>
		public Text TypicalAgeRange {get; set;}
		///<summary>A work featured in some event, e.g. exhibited in an ExhibitionEvent.       Specific subproperties are available for workPerformed (e.g. a play), or a workPresented (a Movie at a ScreeningEvent).</summary>
		public CreativeWork WorkFeatured {get; set;}
		///<summary>A work performed in some event, for example a play performed in a TheaterEvent.</summary>
		public CreativeWork WorkPerformed {get; set;}
	}
}
