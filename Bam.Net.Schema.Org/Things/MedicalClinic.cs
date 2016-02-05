using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A medical clinic.</summary>
	public class MedicalClinic: MedicalOrganization
	{
		///<summary>A medical service available from this provider.</summary>
		public ThisOrThat<MedicalTest , MedicalProcedure , MedicalTherapy> AvailableService {get; set;}
		///<summary>A medical specialty of the provider.</summary>
		public MedicalSpecialty MedicalSpecialty {get; set;}
	}
}
