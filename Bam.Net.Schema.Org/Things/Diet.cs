/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A strategy of regulating the intake of food to achieve or maintain a specific health-related goal.</summary>
	public class Diet: LifestyleModification
	{
		///<summary>Nutritional information specific to the dietary plan. May include dietary recommendations on what foods to avoid, what foods to consume, and specific alterations/deviations from the USDA or other regulatory body's approved dietary guidelines.</summary>
		public Text DietFeatures {get; set;}
		///<summary>People or organizations that endorse the plan.</summary>
		public ThisOrThat<Organization , Person> Endorsers {get; set;}
		///<summary>Medical expert advice related to the plan.</summary>
		public Text ExpertConsiderations {get; set;}
		///<summary>Descriptive information establishing the overarching theory/philosophy of the plan. May include the rationale for the name, the population where the plan first came to prominence, etc.</summary>
		public Text Overview {get; set;}
		///<summary>Specific physiologic benefits associated to the plan.</summary>
		public Text PhysiologicalBenefits {get; set;}
		///<summary>Proprietary name given to the diet plan, typically by its originator or creator.</summary>
		public Text ProprietaryName {get; set;}
		///<summary>Specific physiologic risks associated to the plan.</summary>
		public Text Risks {get; set;}
	}
}
