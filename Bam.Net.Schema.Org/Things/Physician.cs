using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A doctor's office.</summary>
	public class Physician: MedicalOrganization
	{
		///<summary>A medical service available from this provider.</summary>
		public ThisOrThat<MedicalTherapy , MedicalTest , MedicalProcedure> AvailableService {get; set;}
		///<summary>A hospital with which the physician or office is affiliated.</summary>
		public Hospital HospitalAffiliation {get; set;}
		///<summary>A medical specialty of the provider.</summary>
		public MedicalSpecialty MedicalSpecialty {get; set;}
	}
}
