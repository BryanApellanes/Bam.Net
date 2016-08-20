using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A Property value specification.</summary>
	public class PropertyValueSpecification: Intangible
	{
		///<summary>The default value of the input.  For properties that expect a literal, the default is a literal value, for properties that expect an object, it's an ID reference to one of the current values.</summary>
		public OneOfThese<Text,Thing> DefaultValue {get; set;}
		///<summary>The upper value of some characteristic or property.</summary>
		public Number MaxValue {get; set;}
		///<summary>The lower value of some characteristic or property.</summary>
		public Number MinValue {get; set;}
		///<summary>Whether multiple values are allowed for the property.  Default is false.</summary>
		public Boolean MultipleValues {get; set;}
		///<summary>Whether or not a property is mutable.  Default is false. Specifying this for a property that also has a value makes it act similar to a "hidden" input in an HTML form.</summary>
		public Boolean ReadonlyValue {get; set;}
		///<summary>The stepValue attribute indicates the granularity that is expected (and required) of the value in a PropertyValueSpecification.</summary>
		public Number StepValue {get; set;}
		///<summary>Specifies the allowed range for number of characters in a literal value.</summary>
		public Number ValueMaxLength {get; set;}
		///<summary>Specifies the minimum allowed range for number of characters in a literal value.</summary>
		public Number ValueMinLength {get; set;}
		///<summary>Indicates the name of the PropertyValueSpecification to be used in URL templates and form encoding in a manner analogous to HTML's input@name.</summary>
		public Text ValueName {get; set;}
		///<summary>Specifies a regular expression for testing literal values according to the HTML spec.</summary>
		public Text ValuePattern {get; set;}
		///<summary>Whether the property must be filled in to complete the action.  Default is false.</summary>
		public Boolean ValueRequired {get; set;}
	}
}
