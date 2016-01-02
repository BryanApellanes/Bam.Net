/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A medical test performed by a laboratory that typically involves examination of a tissue sample by a pathologist.</summary>
	public class PathologyTest: MedicalTest
	{
		///<summary>The type of tissue sample required for the test.</summary>
		public Text TissueSample {get; set;}
	}
}
