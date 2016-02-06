/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>An intangible item that describes an alignment between a learning resource and a node in an educational framework.</summary>
	public class AlignmentObject: Intangible
	{
		///<summary>A category of alignment between the learning resource and the framework node. Recommended values include: 'assesses', 'teaches', 'requires', 'textComplexity', 'readingLevel', 'educationalSubject', and 'educationLevel'.</summary>
		public Text AlignmentType {get; set;}
		///<summary>The framework to which the resource being described is aligned.</summary>
		public Text EducationalFramework {get; set;}
		///<summary>The description of a node in an established educational framework.</summary>
		public Text TargetDescription {get; set;}
		///<summary>The name of a node in an established educational framework.</summary>
		public Text TargetName {get; set;}
		///<summary>The URL of a node in an established educational framework.</summary>
		public URL TargetUrl {get; set;}
	}
}
