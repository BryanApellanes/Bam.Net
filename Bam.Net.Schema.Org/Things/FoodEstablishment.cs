using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A food-related business.</summary>
	public class FoodEstablishment: LocalBusiness
	{
		///<summary>Indicates whether a FoodEstablishment accepts reservations. Values can be Boolean, an URL at which reservations can be made or (for backwards compatibility) the strings Yes or No.</summary>
		public OneOfThese<Boolean , Text , URL> AcceptsReservations {get; set;}
		///<summary>Either the actual menu or a URL of the menu.</summary>
		public OneOfThese<Text , URL> Menu {get; set;}
		///<summary>The cuisine of the restaurant.</summary>
		public Text ServesCuisine {get; set;}
	}
}
