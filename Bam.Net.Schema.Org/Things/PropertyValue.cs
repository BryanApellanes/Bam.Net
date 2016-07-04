using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A property-value pair, e.g. representing a feature of a product or place. Use the 'name' property for the name of the property. If there is an additional human-readable version of the value, put that into the 'description' property.                Always use specific schema.org properties when a) they exist and b) you can populate them. Using PropertyValue as a substitute will typically not trigger the same effect as using the original, specific property.</summary>
	public class PropertyValue: StructuredValue
	{
		///<summary>The upper value of some characteristic or property.</summary>
		public Number MaxValue {get; set;}
		///<summary>The lower value of some characteristic or property.</summary>
		public Number MinValue {get; set;}
		///<summary>A commonly used identifier for the characteristic represented by the property, e.g. a manufacturer or a standard code for a property. propertyID can be(1) a prefixed string, mainly meant to be used with standards for product properties; (2) a site-specific, non-prefixed string (e.g. the primary key of the property or the vendor-specific id of the property), or (3)a URL indicating the type of the property, either pointing to an external vocabulary, or a Web resource that describes the property (e.g. a glossary entry).Standards bodies should promote a standard prefix for the identifiers of properties from their standards.</summary>
		public OneOfThese<Text , URL> PropertyID {get; set;}
		///<summary>The unit of measurement given using the UN/CEFACT Common Code (3 characters) or a URL. Other codes than the UN/CEFACT Common Code may be used with a prefix followed by a colon.</summary>
		public OneOfThese<Text , URL> UnitCode {get; set;}
		///<summary>A string or text indicating the unit of measurement. Useful if you cannot provide a standard unit code forunitCode.</summary>
		public Text UnitText {get; set;}
		///<summary>The value of the quantitative value or property value node.    For QuantitativeValue and MonetaryValue, the recommended type for values is 'Number'.     For PropertyValue, it can be 'Text;', 'Number', 'Boolean', or 'StructuredValue'.</summary>
		public OneOfThese<Boolean , Number , StructuredValue , Text> Value {get; set;}
		///<summary>A pointer to a secondary value that provides additional information on the original value, e.g. a reference temperature.</summary>
		public OneOfThese<Enumeration , PropertyValue , QualitativeValue , QuantitativeValue , StructuredValue> ValueReference {get; set;}
	}
}
