using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Information about the engine of the vehicle. A vehicle can have multiple engines represented by multiple engine specification entities.</summary>
	public class EngineSpecification: StructuredValue
	{
		///<summary>The type of fuel suitable for the engine or engines of the vehicle. If the vehicle has only one engine, this property can be attached directly to the vehicle.</summary>
		public OneOfThese<QualitativeValue,Text,Url> FuelType {get; set;}
	}
}
