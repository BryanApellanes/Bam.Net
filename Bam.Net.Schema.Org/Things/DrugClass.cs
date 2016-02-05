using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A class of medical drugs, e.g., statins. Classes can represent general pharmacological class, common mechanisms of action, common physiological effects, etc.</summary>
	public class DrugClass: MedicalTherapy
	{
		///<summary>A drug in this drug class.</summary>
		public Drug Drug {get; set;}
	}
}
