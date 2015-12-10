/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The most generic type of entity related to health and the practice of medicine.</summary>
	public class MedicalEntity: Thing
	{
		///<summary>A medical code for the entity, taken from a controlled vocabulary or ontology such as ICD-9, DiseasesDB, MeSH, SNOMED-CT, RxNorm, etc.</summary>
		public MedicalCode Code {get; set;}
		///<summary>A medical guideline related to this entity.</summary>
		public MedicalGuideline Guideline {get; set;}
		///<summary>The system of medicine that includes this MedicalEntity, for example 'evidence-based', 'homeopathic', 'chiropractic', etc.</summary>
		public MedicineSystem MedicineSystem {get; set;}
		///<summary>If applicable, the organization that officially recognizes this entity as part of its endorsed system of medicine.</summary>
		public Organization RecognizingAuthority {get; set;}
		///<summary>If applicable, a medical specialty in which this entity is relevant.</summary>
		public MedicalSpecialty RelevantSpecialty {get; set;}
		///<summary>A medical study or trial related to this entity.</summary>
		public MedicalStudy Study {get; set;}
	}
}
