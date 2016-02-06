using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A code for a medical entity.</summary>
	public class MedicalCode: MedicalIntangible
	{
		///<summary>The actual code.</summary>
		public Text CodeValue {get; set;}
		///<summary>The coding system, e.g. 'ICD-10'.</summary>
		public Text CodingSystem {get; set;}
	}
}
