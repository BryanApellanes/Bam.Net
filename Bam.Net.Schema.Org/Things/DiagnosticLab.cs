/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A medical laboratory that offers on-site or off-site diagnostic services.</summary>
	public class DiagnosticLab: MedicalOrganization
	{
		///<summary>A diagnostic test or procedure offered by this lab.</summary>
		public MedicalTest AvailableTest {get; set;}
	}
}
