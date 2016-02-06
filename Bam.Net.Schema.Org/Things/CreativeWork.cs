/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The most generic kind of creative work, including books, movies, photographs, software programs, etc.</summary>
	public class CreativeWork: Thing
	{
		///<summary>The subject matter of the content.</summary>
		public Thing About {get; set;}
		///<summary>Indicates that the resource is compatible with the referenced accessibility API (WebSchemas wiki lists possible values).</summary>
		public Text AccessibilityAPI {get; set;}
		///<summary>Identifies input methods that are sufficient to fully control the described resource (WebSchemas wiki lists possible values).</summary>
		public Text AccessibilityControl {get; set;}
		///<summary>Content features of the resource, such as accessible media, alternatives and supported enhancements for accessibility (WebSchemas wiki lists possible values).</summary>
		public Text AccessibilityFeature {get; set;}
		///<summary>A characteristic of the described resource that is physiologically dangerous to some users. Related to WCAG 2.0 guideline 2.3 (WebSchemas wiki lists possible values).</summary>
		public Text AccessibilityHazard {get; set;}
		///<summary>Specifies the Person that is legally accountable for the CreativeWork.</summary>
		public Person AccountablePerson {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>A secondary title of the CreativeWork.</summary>
		public Text AlternativeHeadline {get; set;}
		///<summary>A media object that encodes this CreativeWork. This property is a synonym for encoding.</summary>
		public MediaObject AssociatedMedia {get; set;}
		///<summary>The intended audience of the item, i.e. the group for whom the item was created.</summary>
		public Audience Audience {get; set;}
		///<summary>An embedded audio object.</summary>
		public AudioObject Audio {get; set;}
		///<summary>The author of this content. Please note that author is special in that HTML 5 provides a special mechanism for indicating authorship via the rel tag. That is equivalent to this and may be used interchangeably.</summary>
		public ThisOrThat<Person , Organization> Author {get; set;}
		///<summary>An award won by this person or for this creative work. Supersedes awards.</summary>
		public Text Award {get; set;}
		///<summary>Fictional person connected with a creative work.</summary>
		public Person Character {get; set;}
		///<summary>A citation or reference to another creative work, such as another publication, web page, scholarly article, etc.</summary>
		public ThisOrThat<Text , CreativeWork> Citation {get; set;}
		///<summary>Comments, typically from users, on this CreativeWork.</summary>
		public ThisOrThat<UserComments , Comment> Comment {get; set;}
		///<summary>The number of comments this CreativeWork (e.g. Article, Question or Answer) has received. This is most applicable to works published in Web sites with commenting system; additional comments may exist elsewhere.</summary>
		public Integer CommentCount {get; set;}
		///<summary>The location of the content.</summary>
		public Place ContentLocation {get; set;}
		///<summary>Official rating of a piece of content—for example,'MPAA PG-13'.</summary>
		public Text ContentRating {get; set;}
		///<summary>A secondary contributor to the CreativeWork.</summary>
		public ThisOrThat<Person , Organization> Contributor {get; set;}
		///<summary>The party holding the legal copyright to the CreativeWork.</summary>
		public ThisOrThat<Person , Organization> CopyrightHolder {get; set;}
		///<summary>The year during which the claimed copyright for the CreativeWork was first asserted.</summary>
		public Number CopyrightYear {get; set;}
		///<summary>The creator/author of this CreativeWork or UserComments. This is the same as the Author property for CreativeWork.</summary>
		public ThisOrThat<Person , Organization> Creator {get; set;}
		///<summary>The date on which the CreativeWork was created.</summary>
		public Date DateCreated {get; set;}
		///<summary>The date on which the CreativeWork was most recently modified.</summary>
		public Date DateModified {get; set;}
		///<summary>Date of first broadcast/publication.</summary>
		public Date DatePublished {get; set;}
		///<summary>A link to the page containing the comments of the CreativeWork.</summary>
		public URL DiscussionUrl {get; set;}
		///<summary>Specifies the Person who edited the CreativeWork.</summary>
		public Person Editor {get; set;}
		///<summary>An alignment to an established educational framework.</summary>
		public AlignmentObject EducationalAlignment {get; set;}
		///<summary>The purpose of a work in the context of education; for example, 'assignment', 'group work'.</summary>
		public Text EducationalUse {get; set;}
		///<summary>A media object that encodes this CreativeWork. This property is a synonym for associatedMedia. Supersedes encodings.</summary>
		public MediaObject Encoding {get; set;}
		///<summary>A creative work that this work is an example/instance/realization/derivation of. Inverse property: workExample.</summary>
		public CreativeWork ExampleOfWork {get; set;}
		///<summary>Genre of the creative work or group.</summary>
		public Text Genre {get; set;}
		///<summary>Indicates a CreativeWork that is (in some sense) a part of this CreativeWork. Inverse property: isPartOf.</summary>
		public CreativeWork HasPart {get; set;}
		///<summary>Headline of the article.</summary>
		public Text Headline {get; set;}
		///<summary>The language of the content. please use one of the language codes from the IETF BCP 47 standard.</summary>
		public Text InLanguage {get; set;}
		///<summary>A count of a specific user interactions with this item—for example, 20 UserLikes, 5 UserComments, or 300 UserDownloads. The user interaction type should be one of the sub types of UserInteraction.</summary>
		public Text InteractionCount {get; set;}
		///<summary>The predominant mode of learning supported by the learning resource. Acceptable values are 'active', 'expositive', or 'mixed'.</summary>
		public Text InteractivityType {get; set;}
		///<summary>A resource that was used in the creation of this resource. This term can be repeated for multiple sources. For example, http://example.com/great-multiplication-intro.html.</summary>
		public URL IsBasedOnUrl {get; set;}
		///<summary>Indicates whether this content is family friendly.</summary>
		public Boolean IsFamilyFriendly {get; set;}
		///<summary>Indicates a CreativeWork that this CreativeWork is (in some sense) part of. Inverse property: hasPart.</summary>
		public CreativeWork IsPartOf {get; set;}
		///<summary>Keywords or tags used to describe this content. Multiple entries in a keywords list are typically delimited by commas.</summary>
		public Text Keywords {get; set;}
		///<summary>The predominant type or kind characterizing the learning resource. For example, 'presentation', 'handout'.</summary>
		public Text LearningResourceType {get; set;}
		///<summary>A license document that applies to this content, typically indicated by URL.</summary>
		public ThisOrThat<URL , CreativeWork> License {get; set;}
		///<summary>Indicates that the CreativeWork contains a reference to, but is not necessarily about a concept.</summary>
		public Thing Mentions {get; set;}
		///<summary>An offer to provide this item—for example, an offer to sell a product, rent the DVD of a movie, or give away tickets to an event.</summary>
		public Offer Offers {get; set;}
		///<summary>The position of an item in a series or sequence of items.</summary>
		public ThisOrThat<Integer , Text> Position {get; set;}
		///<summary>The person or organization who produced the work (e.g. music album, movie, tv/radio series etc.).</summary>
		public ThisOrThat<Person , Organization> Producer {get; set;}
		///<summary>The service provider, service operator, or service performer; the goods producer. Another party (a seller) may offer those services or goods on behalf of the provider. A provider may also serve as the seller. Supersedes carrier.</summary>
		public ThisOrThat<Person , Organization> Provider {get; set;}
		///<summary>The publisher of the creative work.</summary>
		public Organization Publisher {get; set;}
		///<summary>Link to page describing the editorial principles of the organization primarily responsible for the creation of the CreativeWork.</summary>
		public URL PublishingPrinciples {get; set;}
		///<summary>The Event where the CreativeWork was recorded. The CreativeWork may capture all or part of the event. Inverse property: recordedIn.</summary>
		public Event RecordedAt {get; set;}
		///<summary>The place and time the release was issued, expressed as a PublicationEvent.</summary>
		public PublicationEvent ReleasedEvent {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The Organization on whose behalf the creator was working.</summary>
		public Organization SourceOrganization {get; set;}
		///<summary>The textual content of this CreativeWork.</summary>
		public Text Text {get; set;}
		///<summary>A thumbnail image relevant to the Thing.</summary>
		public URL ThumbnailUrl {get; set;}
		///<summary>Approximate or typical time it takes to work with or through this learning resource for the typical intended target audience, e.g. 'P30M', 'P1H25M'.</summary>
		public Duration TimeRequired {get; set;}
		///<summary>Organization or person who adapts a creative work to different languages, regional differences and technical requirements of a target market.</summary>
		public ThisOrThat<Person , Organization> Translator {get; set;}
		///<summary>The typical expected age range, e.g. '7-9', '11-'.</summary>
		public Text TypicalAgeRange {get; set;}
		///<summary>The version of the CreativeWork embodied by a specified resource.</summary>
		public Number Version {get; set;}
		///<summary>An embedded video object.</summary>
		public VideoObject Video {get; set;}
		///<summary>Example/instance/realization/derivation of the concept of this creative work. eg. The paperback edition, first edition, or eBook. Inverse property: exampleOfWork.</summary>
		public CreativeWork WorkExample {get; set;}
	}
}
