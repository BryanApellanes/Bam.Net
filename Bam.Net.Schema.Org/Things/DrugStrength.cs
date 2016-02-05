using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A specific strength in which a medical drug is available in a specific country.</summary>
	public class DrugStrength: MedicalIntangible
	{
		///<summary>An active ingredient, typically chemical compounds and/or biologic substances.</summary>
		public Text ActiveIngredient {get; set;}
		///<summary>The location in which the strength is available.</summary>
		public AdministrativeArea AvailableIn {get; set;}
		///<summary>The units of an active ingredient's strength, e.g. mg.</summary>
		public Text StrengthUnit {get; set;}
		///<summary>The value of an active ingredient's strength, e.g. 325.</summary>
		public Number StrengthValue {get; set;}
	}
}
