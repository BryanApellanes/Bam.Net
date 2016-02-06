/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Entities that have a somewhat fixed, physical extension.</summary>
	public class Place: Thing
	{
		///<summary>Physical address of the item.</summary>
		public PostalAddress Address {get; set;}
		///<summary>The overall rating, based on a collection of reviews or ratings, of the item.</summary>
		public AggregateRating AggregateRating {get; set;}
		///<summary>The basic containment relation between places.</summary>
		public Place ContainedIn {get; set;}
		///<summary>Upcoming or past event associated with this place, organization, or action. Supersedes events.</summary>
		public Event Event {get; set;}
		///<summary>The fax number.</summary>
		public Text FaxNumber {get; set;}
		///<summary>The geo coordinates of the place.</summary>
		public OneOfThese<GeoShape , GeoCoordinates> Geo {get; set;}
		///<summary>The Global Location Number (GLN, sometimes also referred to as International Location Number or ILN) of the respective organization, person, or place. The GLN is a 13-digit number used to identify parties and physical locations.</summary>
		public Text GlobalLocationNumber {get; set;}
		///<summary>A URL to a map of the place. Supersedes map, maps.</summary>
		public OneOfThese<Map , URL> HasMap {get; set;}
		///<summary>A count of a specific user interactions with this item—for example, 20 UserLikes, 5 UserComments, or 300 UserDownloads. The user interaction type should be one of the sub types of UserInteraction.</summary>
		public Text InteractionCount {get; set;}
		///<summary>The International Standard of Industrial Classification of All Economic Activities (ISIC), Revision 4 code for a particular organization, business person, or place.</summary>
		public Text IsicV4 {get; set;}
		///<summary>An associated logo.</summary>
		public OneOfThese<URL , ImageObject> Logo {get; set;}
		///<summary>The opening hours of a certain place.</summary>
		public OpeningHoursSpecification OpeningHoursSpecification {get; set;}
		///<summary>A photograph of this place. Supersedes photos.</summary>
		public OneOfThese<Photograph , ImageObject> Photo {get; set;}
		///<summary>A review of the item. Supersedes reviews.</summary>
		public Review Review {get; set;}
		///<summary>The telephone number.</summary>
		public Text Telephone {get; set;}
	}
}
