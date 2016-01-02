/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Anatomical features that can be observed by sight (without dissection), including the form and proportions of the human body as well as surface landmarks that correspond to deeper subcutaneous structures. Superficial anatomy plays an important role in sports medicine, phlebotomy, and other medical specialties as underlying anatomical structures can be identified through surface palpation. For example, during back surgery, superficial anatomy can be used to palpate and count vertebrae to find the site of incision. Or in phlebotomy, superficial anatomy can be used to locate an underlying vein; for example, the median cubital vein can be located by palpating the borders of the cubital fossa (such as the epicondyles of the humerus) and then looking for the superficial signs of the vein, such as size, prominence, ability to refill after depression, and feel of surrounding tissue support. As another example, in a subluxation (dislocation) of the glenohumeral joint, the bony structure becomes pronounced with the deltoid muscle failing to cover the glenohumeral joint allowing the edges of the scapula to be superficially visible. Here, the superficial anatomy is the visible edges of the scapula, implying the underlying dislocation of the joint (the related anatomical structure).</summary>
	public class SuperficialAnatomy: MedicalEntity
	{
		///<summary>If applicable, a description of the pathophysiology associated with the anatomical system, including potential abnormal changes in the mechanical, physical, and biochemical functions of the system.</summary>
		public Text AssociatedPathophysiology {get; set;}
		///<summary>Anatomical systems or structures that relate to the superficial anatomy.</summary>
		public ThisOrThat<AnatomicalSystem , AnatomicalStructure> RelatedAnatomy {get; set;}
		///<summary>A medical condition associated with this anatomy.</summary>
		public MedicalCondition RelatedCondition {get; set;}
		///<summary>A medical therapy related to this anatomy.</summary>
		public MedicalTherapy RelatedTherapy {get; set;}
		///<summary>The significance associated with the superficial anatomy; as an example, how characteristics of the superficial anatomy can suggest underlying medical conditions or courses of treatment.</summary>
		public Text Significance {get; set;}
	}
}
