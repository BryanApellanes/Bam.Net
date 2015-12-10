/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A predefined value for a product characteristic, e.g. the power cord plug type "US" or the garment sizes "S", "M", "L", and "XL".</summary>
	public class QualitativeValue: Enumeration
	{
		///<summary>This ordering relation for qualitative values indicates that the subject is equal to the object.</summary>
		public QualitativeValue Equal {get; set;}
		///<summary>This ordering relation for qualitative values indicates that the subject is greater than the object.</summary>
		public QualitativeValue Greater {get; set;}
		///<summary>This ordering relation for qualitative values indicates that the subject is greater than or equal to the object.</summary>
		public QualitativeValue GreaterOrEqual {get; set;}
		///<summary>This ordering relation for qualitative values indicates that the subject is lesser than the object.</summary>
		public QualitativeValue Lesser {get; set;}
		///<summary>This ordering relation for qualitative values indicates that the subject is lesser than or equal to the object.</summary>
		public QualitativeValue LesserOrEqual {get; set;}
		///<summary>This ordering relation for qualitative values indicates that the subject is not equal to the object.</summary>
		public QualitativeValue NonEqual {get; set;}
		///<summary>A pointer to a secondary value that provides additional information on the original value, e.g. a reference temperature.</summary>
		public ThisOrThat<Enumeration , StructuredValue> ValueReference {get; set;}
	}
}
