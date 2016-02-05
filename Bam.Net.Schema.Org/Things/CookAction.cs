using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of producing/preparing food.</summary>
	public class CookAction: CreateAction
	{
		///<summary>A sub property of location. The specific food establishment where the action occurred.</summary>
		public ThisOrThat<FoodEstablishment , Place> FoodEstablishment {get; set;}
		///<summary>A sub property of location. The specific food event where the action occurred.</summary>
		public FoodEvent FoodEvent {get; set;}
		///<summary>A sub property of instrument. The recipe/instructions used to perform the action.</summary>
		public Recipe Recipe {get; set;}
	}
}
