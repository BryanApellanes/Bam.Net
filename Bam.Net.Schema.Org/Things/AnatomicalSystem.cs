using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An anatomical system is a group of anatomical structures that work together to perform a certain task. Anatomical systems, such as organ systems, are one organizing principle of anatomy, and can includes circulatory, digestive, endocrine, integumentary, immune, lymphatic, muscular, nervous, reproductive, respiratory, skeletal, urinary, vestibular, and other systems.</summary>
	public class AnatomicalSystem: MedicalEntity
	{
		///<summary>If applicable, a description of the pathophysiology associated with the anatomical system, including potential abnormal changes in the mechanical, physical, and biochemical functions of the system.</summary>
		public Text AssociatedPathophysiology {get; set;}
		///<summary>The underlying anatomical structures, such as organs, that comprise the anatomical system.</summary>
		public OneOfThese<AnatomicalSystem , AnatomicalStructure> ComprisedOf {get; set;}
		///<summary>A medical condition associated with this anatomy.</summary>
		public MedicalCondition RelatedCondition {get; set;}
		///<summary>Related anatomical structure(s) that are not part of the system but relate or connect to it, such as vascular bundles associated with an organ system.</summary>
		public AnatomicalStructure RelatedStructure {get; set;}
		///<summary>A medical therapy related to this anatomy.</summary>
		public MedicalTherapy RelatedTherapy {get; set;}
	}
}
