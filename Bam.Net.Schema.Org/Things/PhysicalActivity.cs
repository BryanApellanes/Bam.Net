using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any bodily activity that enhances or maintains physical fitness and overall health and wellness. Includes activity that is part of daily living and routine, structured exercise, and exercise prescribed as part of a medical treatment or recovery plan.</summary>
	public class PhysicalActivity: LifestyleModification
	{
		///<summary>The anatomy of the underlying organ system or structures associated with this entity.</summary>
		public OneOfThese<AnatomicalSystem , SuperficialAnatomy , AnatomicalStructure> AssociatedAnatomy {get; set;}
		///<summary>A category for the item. Greater signs or slashes can be used to informally indicate a category hierarchy.</summary>
		public OneOfThese<Thing , PhysicalActivityCategory , Text> Category {get; set;}
		///<summary>The characteristics of associated patients, such as age, gender, race etc.</summary>
		public Text Epidemiology {get; set;}
		///<summary>Changes in the normal mechanical, physical, and biochemical functions that are associated with this activity or condition.</summary>
		public Text Pathophysiology {get; set;}
	}
}
