/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A simple system that adds up the number of risk factors to yield a score that is associated with prognosis, e.g. CHAD score, TIMI risk score.</summary>
	public class MedicalRiskScore: MedicalRiskEstimator
	{
		///<summary>The algorithm or rules to follow to compute the score.</summary>
		public Text Algorithm {get; set;}
	}
}
