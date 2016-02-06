using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any condition of the human body that affects the normal functioning of a person, whether physically or mentally. Includes diseases, injuries, disabilities, disorders, syndromes, etc.</summary>
	public class MedicalCondition: MedicalEntity
	{
		///<summary>The anatomy of the underlying organ system or structures associated with this entity.</summary>
		public OneOfThese<AnatomicalSystem , SuperficialAnatomy , AnatomicalStructure> AssociatedAnatomy {get; set;}
		///<summary>An underlying cause. More specifically, one of the causative agent(s) that are most directly responsible for the pathophysiologic process that eventually results in the occurrence.</summary>
		public MedicalCause Cause {get; set;}
		///<summary>One of a set of differential diagnoses for the condition. Specifically, a closely-related or competing diagnosis typically considered later in the cognitive process whereby this medical condition is distinguished from others most likely responsible for a similar collection of signs and symptoms to reach the most parsimonious diagnosis or diagnoses in a patient.</summary>
		public DDxElement DifferentialDiagnosis {get; set;}
		///<summary>The characteristics of associated patients, such as age, gender, race etc.</summary>
		public Text Epidemiology {get; set;}
		///<summary>The likely outcome in either the short term or long term of the medical condition.</summary>
		public Text ExpectedPrognosis {get; set;}
		///<summary>The expected progression of the condition if it is not treated and allowed to progress naturally.</summary>
		public Text NaturalProgression {get; set;}
		///<summary>Changes in the normal mechanical, physical, and biochemical functions that are associated with this activity or condition.</summary>
		public Text Pathophysiology {get; set;}
		///<summary>A possible unexpected and unfavorable evolution of a medical condition. Complications may include worsening of the signs or symptoms of the disease, extension of the condition to other organ systems, etc.</summary>
		public Text PossibleComplication {get; set;}
		///<summary>A possible treatment to address this condition, sign or symptom.</summary>
		public MedicalTherapy PossibleTreatment {get; set;}
		///<summary>A preventative therapy used to prevent an initial occurrence of the medical condition, such as vaccination.</summary>
		public MedicalTherapy PrimaryPrevention {get; set;}
		///<summary>A modifiable or non-modifiable factor that increases the risk of a patient contracting this condition, e.g. age,  coexisting condition.</summary>
		public MedicalRiskFactor RiskFactor {get; set;}
		///<summary>A preventative therapy used to prevent reoccurrence of the medical condition after an initial episode of the condition.</summary>
		public MedicalTherapy SecondaryPrevention {get; set;}
		///<summary>A sign or symptom of this condition. Signs are objective or physically observable manifestations of the medical condition while symptoms are the subjective experience of the medical condition.</summary>
		public MedicalSignOrSymptom SignOrSymptom {get; set;}
		///<summary>The stage of the condition, if applicable.</summary>
		public MedicalConditionStage Stage {get; set;}
		///<summary>A more specific type of the condition, where applicable, for example 'Type 1 Diabetes', 'Type 2 Diabetes', or 'Gestational Diabetes' for Diabetes.</summary>
		public Text Subtype {get; set;}
		///<summary>A medical test typically performed given this condition.</summary>
		public MedicalTest TypicalTest {get; set;}
	}
}
