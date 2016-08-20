using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A point value or interval for product characteristics and other purposes.</summary>
	public class QuantitativeValue: StructuredValue
	{
		///<summary>A property-value pair representing an additional characteristics of the entitity, e.g. a product feature or another characteristic for which there is no matching property in schema.org.Note: Publishers should be aware that applications designed to use specific schema.org properties (e.g. http://schema.org/width, http://schema.org/color, http://schema.org/gtin13, ...) will typically expect such data to be provided using those properties, rather than using the generic property/value mechanism.</summary>
		public PropertyValue AdditionalProperty {get; set;}
		///<summary>The upper value of some characteristic or property.</summary>
		public Number MaxValue {get; set;}
		///<summary>The lower value of some characteristic or property.</summary>
		public Number MinValue {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters) or a URL. Other codes than the UN/CEFACT Common Code may be used with a prefix followed by a colon.</summary>
		public OneOfThese<Text,Url> UnitCode {get; set;}
		///<summary>A string or text indicating the unit of measurement. Useful if you cannot provide a standard unit code forunitCode.</summary>
		public Text UnitText {get; set;}
		///<summary>The value of the quantitative value or property value node.For QuantitativeValue and MonetaryAmount, the recommended type for values is 'Number'.For PropertyValue, it can be 'Text;', 'Number', 'Boolean', or 'StructuredValue'.</summary>
		public OneOfThese<Boolean,Number,StructuredValue,Text> Value {get; set;}
		///<summary>A pointer to a secondary value that provides additional information on the original value, e.g. a reference temperature.</summary>
		public OneOfThese<Enumeration,PropertyValue,QualitativeValue,QuantitativeValue,StructuredValue> ValueReference {get; set;}
	}
}
