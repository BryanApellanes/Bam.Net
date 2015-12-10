/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A hospital.</summary>
	public class Hospital: MedicalOrganization
	{
		///<summary>A medical service available from this provider.</summary>
		public ThisOrThat<MedicalTherapy , MedicalTest , MedicalProcedure> AvailableService {get; set;}
		///<summary>A medical specialty of the provider.</summary>
		public MedicalSpecialty MedicalSpecialty {get; set;}
	}
}
