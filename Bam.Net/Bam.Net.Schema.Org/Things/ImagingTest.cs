/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any medical imaging modality typically used for diagnostic purposes.</summary>
	public class ImagingTest: MedicalTest
	{
		///<summary>Imaging technique used.</summary>
		public MedicalImagingTechnique ImagingTechnique {get; set;}
	}
}
