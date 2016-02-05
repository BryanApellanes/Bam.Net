using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any medical intervention designed to prevent, treat, and cure human diseases and medical conditions, including both curative and palliative therapies. Medical therapies are typically processes of care relying upon pharmacotherapy, behavioral therapy, supportive therapy (with fluid or nutrition for example), or detoxification (e.g. hemodialysis) aimed at improving or preventing a health condition.</summary>
	public class MedicalTherapy: MedicalEntity
	{
		///<summary>A possible complication and/or side effect of this therapy. If it is known that an adverse outcome is serious (resulting in death, disability, or permanent damage; requiring hospitalization; or is otherwise life-threatening or requires immediate medical attention), tag it as a seriouseAdverseOutcome instead.</summary>
		public MedicalEntity AdverseOutcome {get; set;}
		///<summary>A contraindication for this therapy.</summary>
		public MedicalContraindication Contraindication {get; set;}
		///<summary>A therapy that duplicates or overlaps this one.</summary>
		public MedicalTherapy DuplicateTherapy {get; set;}
		///<summary>A factor that indicates use of this therapy for treatment and/or prevention of a condition, symptom, etc. For therapies such as drugs, indications can include both officially-approved indications as well as off-label uses. These can be distinguished by using the ApprovedIndication subtype of MedicalIndication.</summary>
		public MedicalIndication Indication {get; set;}
		///<summary>A possible serious complication and/or serious side effect of this therapy. Serious adverse outcomes include those that are life-threatening; result in death, disability, or permanent damage; require hospitalization or prolong existing hospitalization; cause congenital anomalies or birth defects; or jeopardize the patient and may require medical or surgical intervention to prevent one of the outcomes in this definition.</summary>
		public MedicalEntity SeriousAdverseOutcome {get; set;}
	}
}
