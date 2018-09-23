using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Nutritional information about the recipe.</summary>
	public class NutritionInformation: StructuredValue
	{
		///<summary>The number of calories.</summary>
		public Energy Calories {get; set;}
		///<summary>The number of grams of carbohydrates.</summary>
		public Mass CarbohydrateContent {get; set;}
		///<summary>The number of milligrams of cholesterol.</summary>
		public Mass CholesterolContent {get; set;}
		///<summary>The number of grams of fat.</summary>
		public Mass FatContent {get; set;}
		///<summary>The number of grams of fiber.</summary>
		public Mass FiberContent {get; set;}
		///<summary>The number of grams of protein.</summary>
		public Mass ProteinContent {get; set;}
		///<summary>The number of grams of saturated fat.</summary>
		public Mass SaturatedFatContent {get; set;}
		///<summary>The serving size, in terms of the number of volume or mass.</summary>
		public Text ServingSize {get; set;}
		///<summary>The number of milligrams of sodium.</summary>
		public Mass SodiumContent {get; set;}
		///<summary>The number of grams of sugar.</summary>
		public Mass SugarContent {get; set;}
		///<summary>The number of grams of trans fat.</summary>
		public Mass TransFatContent {get; set;}
		///<summary>The number of grams of unsaturated fat.</summary>
		public Mass UnsaturatedFatContent {get; set;}
	}
}
