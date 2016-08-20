using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A lodging business, such as a motel, hotel, or inn.</summary>
	public class LodgingBusiness: LocalBusiness
	{
		///<summary>An amenity feature (e.g. a characteristic or service) of the Accommodation. This generic property does not make a statement about whether the feature is included in an offer for the main accommodation or available at extra costs.</summary>
		public LocationFeatureSpecification AmenityFeature {get; set;}
		///<summary>An intended audience, i.e. a group for whom something was created. Supersedes serviceAudience.</summary>
		public Audience Audience {get; set;}
		///<summary>A language someone may use with the item. Please use one of the language codes from the IETF BCP 47 standard. See also inLanguage</summary>
		public OneOfThese<Language,Text> AvailableLanguage {get; set;}
		///<summary>The earliest someone may check into a lodging establishment.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CheckinTime {get; set;}
		///<summary>The latest someone may check out of a lodging establishment.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date CheckoutTime {get; set;}
		///<summary>Indicates whether pets are allowed to enter the accommodation or lodging business. More detailed information can be put in a text value.</summary>
		public OneOfThese<Boolean,Text> PetsAllowed {get; set;}
		///<summary>An official rating for a lodging business or food establishment, e.g. from national associations or standards bodies. Use the author property to indicate the rating organization, e.g. as an Organization with name such as (e.g. HOTREC, DEHOGA, WHR, or Hotelstars).</summary>
		public Rating StarRating {get; set;}
	}
}
