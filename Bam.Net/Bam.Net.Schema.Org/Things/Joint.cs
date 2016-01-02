/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The anatomical location at which two or more bones make contact.</summary>
	public class Joint: AnatomicalStructure
	{
		///<summary>The biomechanical properties of the bone.</summary>
		public Text BiomechnicalClass {get; set;}
		///<summary>The degree of mobility the joint allows.</summary>
		public Text FunctionalClass {get; set;}
		///<summary>The name given to how bone physically connects to each other.</summary>
		public Text StructuralClass {get; set;}
	}
}
