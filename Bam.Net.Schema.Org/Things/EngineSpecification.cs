using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Information about the engine of the vehicle. A vehicle can have multiple engines represented by multiple engine specification entities.</summary>
	public class EngineSpecification: StructuredValue
	{
		///<summary>The type of fuel suitable for the engine or engines of the vehicle. If the vehicle has only one engine, this property can be attached directly to the vehicle.</summary>
		public ThisOrThat<Text , URL , QualitativeValue> FuelType {get; set;}
	}
}
