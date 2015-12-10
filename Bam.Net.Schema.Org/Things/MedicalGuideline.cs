/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any recommendation made by a standard society (e.g. ACC/AHA) or consensus statement that denotes how to diagnose and treat a particular condition. Note: this type should be used to tag the actual guideline recommendation; if the guideline recommendation occurs in a larger scholarly article, use MedicalScholarlyArticle to tag the overall article, not this type. Note also: the organization making the recommendation should be captured in the recognizingAuthority base property of MedicalEntity.</summary>
	public class MedicalGuideline: MedicalEntity
	{
		///<summary>Strength of evidence of the data used to formulate the guideline (enumerated).</summary>
		public MedicalEvidenceLevel EvidenceLevel {get; set;}
		///<summary>Source of the data used to formulate the guidance, e.g. RCT, consensus opinion, etc.</summary>
		public Text EvidenceOrigin {get; set;}
		///<summary>Date on which this guideline's recommendation was made.</summary>
		public Date GuidelineDate {get; set;}
		///<summary>The medical conditions, treatments, etc. that are the subject of the guideline.</summary>
		public MedicalEntity GuidelineSubject {get; set;}
	}
}
