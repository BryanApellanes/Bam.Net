using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A recipe. For dietary restrictions covered by the recipe,    a few common restrictions are enumerated via suitableForDiet.    The keywords property can also be used to add more detail.</summary>
	public class Recipe: CreativeWork
	{
		///<summary>The time it takes to actually cook the dish, in ISO 8601 duration format.</summary>
		public Duration CookTime {get; set;}
		///<summary>The method of cooking, such as Frying, Steaming, ...</summary>
		public Text CookingMethod {get; set;}
		///<summary>Nutrition information about the recipe.</summary>
		public NutritionInformation Nutrition {get; set;}
		///<summary>The length of time it takes to prepare the recipe, in ISO 8601 duration format.</summary>
		public Duration PrepTime {get; set;}
		///<summary>The category of the recipeâ€”for example, appetizer, entree, etc.</summary>
		public Text RecipeCategory {get; set;}
		///<summary>The cuisine of the recipe (for example, French or Ethiopian).</summary>
		public Text RecipeCuisine {get; set;}
		///<summary>A single ingredient used in the recipe, e.g. sugar, flour or garlic. Supersedes ingredients.</summary>
		public Text RecipeIngredient {get; set;}
		///<summary>A step or instruction involved in making the recipe.</summary>
		public OneOfThese<ItemList , Text> RecipeInstructions {get; set;}
		///<summary>The quantity produced by the recipe (for example, number of people served, number of servings, etc).</summary>
		public Text RecipeYield {get; set;}
		///<summary>Indicates a dietary restriction or guideline for which this recipe is suitable, e.g. diabetic, halal etc.</summary>
		public RestrictedDiet SuitableForDiet {get; set;}
		///<summary>The total time it takes to prepare and cook the recipe, in ISO 8601 duration format.</summary>
		public Duration TotalTime {get; set;}
	}
}
