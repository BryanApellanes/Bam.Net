using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A food-related business.</summary>
	public class FoodEstablishment: LocalBusiness
	{
		///<summary>Indicates whether a FoodEstablishment accepts reservations. Values can be Boolean, an URL at which reservations can be made or (for backwards compatibility) the strings Yes or No.</summary>
		public OneOfThese<Boolean,Text,Url> AcceptsReservations {get; set;}
		///<summary>Either the actual menu or a URL of the menu.</summary>
		public OneOfThese<Text,Url> Menu {get; set;}
		///<summary>The cuisine of the restaurant.</summary>
		public Text ServesCuisine {get; set;}
		///<summary>An official rating for a lodging business or food establishment, e.g. from national associations or standards bodies. Use the author property to indicate the rating organization, e.g. as an Organization with name such as (e.g. HOTREC, DEHOGA, WHR, or Hotelstars).</summary>
		public Rating StarRating {get; set;}
	}
}
