using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Any part of the human body, typically a component of an anatomical system. Organs, tissues, and cells are all anatomical structures.</summary>
	public class AnatomicalStructure: MedicalEntity
	{
		///<summary>If applicable, a description of the pathophysiology associated with the anatomical system, including potential abnormal changes in the mechanical, physical, and biochemical functions of the system.</summary>
		public Text AssociatedPathophysiology {get; set;}
		///<summary>Location in the body of the anatomical structure.</summary>
		public Text BodyLocation {get; set;}
		///<summary>Other anatomical structures to which this structure is connected.</summary>
		public AnatomicalStructure ConnectedTo {get; set;}
		///<summary>An image containing a diagram that illustrates the structure and/or its component substructures and/or connections with other structures.</summary>
		public ImageObject Diagram {get; set;}
		///<summary>Function of the anatomical structure.</summary>
		public Text Function {get; set;}
		///<summary>The anatomical or organ system that this structure is part of.</summary>
		public AnatomicalSystem PartOfSystem {get; set;}
		///<summary>A medical condition associated with this anatomy.</summary>
		public MedicalCondition RelatedCondition {get; set;}
		///<summary>A medical therapy related to this anatomy.</summary>
		public MedicalTherapy RelatedTherapy {get; set;}
		///<summary>Component (sub-)structure(s) that comprise this anatomical structure.</summary>
		public AnatomicalStructure SubStructure {get; set;}
	}
}
