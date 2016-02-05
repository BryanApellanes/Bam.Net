using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any rule set or interactive tool for estimating the risk of developing a complication or condition.</summary>
	public class MedicalRiskEstimator: MedicalEntity
	{
		///<summary>The condition, complication, or symptom whose risk is being estimated.</summary>
		public MedicalEntity EstimatesRiskOf {get; set;}
		///<summary>A modifiable or non-modifiable risk factor included in the calculation, e.g. age, coexisting condition.</summary>
		public MedicalRiskFactor IncludedRiskFactor {get; set;}
	}
}
