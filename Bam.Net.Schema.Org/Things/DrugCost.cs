/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The cost per unit of a medical drug. Note that this type is not meant to represent the price in an offer of a drug for sale; see the Offer type for that. This type will typically be used to tag wholesale or average retail cost of a drug, or maximum reimbursable cost. Costs of medical drugs vary widely depending on how and where they are paid for, so while this type captures some of the variables, costs should be used with caution by consumers of this schema's markup.</summary>
	public class DrugCost: MedicalIntangible
	{
		///<summary>The location in which the status applies.</summary>
		public AdministrativeArea ApplicableLocation {get; set;}
		///<summary>The category of cost, such as wholesale, retail, reimbursement cap, etc.</summary>
		public DrugCostCategory CostCategory {get; set;}
		///<summary>The currency (in 3-letter ISO 4217 format) of the drug cost.</summary>
		public Text CostCurrency {get; set;}
		///<summary>Additional details to capture the origin of the cost data. For example, 'Medicare Part B'.</summary>
		public Text CostOrigin {get; set;}
		///<summary>The cost per unit of the drug.</summary>
		public ThisOrThat<Number , Text> CostPerUnit {get; set;}
		///<summary>The unit in which the drug is measured, e.g. '5 mg tablet'.</summary>
		public Text DrugUnit {get; set;}
	}
}
