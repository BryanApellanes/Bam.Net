using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A predefined value for a product characteristic, e.g. the power cord plug type 'US' or the garment sizes 'S', 'M', 'L', and 'XL'.</summary>
	public class QualitativeValue: Enumeration
	{
		///<summary>A property-value pair representing an additional characteristics of the entitity, e.g. a product feature or another characteristic for which there is no matching property in schema.org.Note: Publishers should be aware that applications designed to use specific schema.org properties (e.g. http://schema.org/width, http://schema.org/color, http://schema.org/gtin13, ...) will typically expect such data to be provided using those properties, rather than using the generic property/value mechanism.</summary>
		public PropertyValue AdditionalProperty {get; set;}
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
		public OneOfThese<Enumeration,PropertyValue,QualitativeValue,QuantitativeValue,StructuredValue> ValueReference {get; set;}
	}
}
