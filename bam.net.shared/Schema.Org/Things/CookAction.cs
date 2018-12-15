using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of producing/preparing food.</summary>
	public class CookAction: CreateAction
	{
		///<summary>A sub property of location. The specific food establishment where the action occurred.</summary>
		public OneOfThese<FoodEstablishment,Place> FoodEstablishment {get; set;}
		///<summary>A sub property of location. The specific food event where the action occurred.</summary>
		public FoodEvent FoodEvent {get; set;}
		///<summary>A sub property of instrument. The recipe/instructions used to perform the action.</summary>
		public Recipe Recipe {get; set;}
	}
}
