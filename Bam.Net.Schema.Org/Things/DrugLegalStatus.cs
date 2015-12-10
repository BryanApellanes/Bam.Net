/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The legal availability status of a medical drug.</summary>
	public class DrugLegalStatus: MedicalIntangible
	{
		///<summary>The location in which the status applies.</summary>
		public AdministrativeArea ApplicableLocation {get; set;}
	}
}
