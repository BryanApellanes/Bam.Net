/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A risk factor is anything that increases a person's likelihood of developing or contracting a disease, medical condition, or complication.</summary>
	public class MedicalRiskFactor: MedicalEntity
	{
		///<summary>The condition, complication, etc. influenced by this factor.</summary>
		public MedicalEntity IncreasesRiskOf {get; set;}
	}
}
