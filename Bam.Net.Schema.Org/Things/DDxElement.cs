/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An alternative, closely-related condition typically considered later in the differential diagnosis process along with the signs that are used to distinguish it.</summary>
	public class DDxElement: MedicalIntangible
	{
		///<summary>One or more alternative conditions considered in the differential diagnosis process.</summary>
		public MedicalCondition Diagnosis {get; set;}
		///<summary>One of a set of signs and symptoms that can be used to distinguish this diagnosis from others in the differential diagnosis.</summary>
		public MedicalSignOrSymptom DistinguishingSign {get; set;}
	}
}
