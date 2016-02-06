using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any collection of tests commonly ordered together.</summary>
	public class MedicalTestPanel: MedicalTest
	{
		///<summary>A component test of the panel.</summary>
		public MedicalTest SubTest {get; set;}
	}
}
