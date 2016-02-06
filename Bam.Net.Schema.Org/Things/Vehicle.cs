using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A vehicle is a device that is designed or used to transport people or cargo over land, water, air, or through space.</summary>
	public class Vehicle: Product
	{
		///<summary>The available volume for cargo or luggage. For automobiles, this is usually the trunk volume.Typical unit code(s): LTR for liters, FTQ for cubic foot/feetNote: You can use minValue and maxValue to indicate ranges.</summary>
		public QuantitativeValue CargoVolume {get; set;}
		///<summary>The date of the first registration of the vehicle with the respective public authorities.</summary>
		public Date DateVehicleFirstRegistered {get; set;}
		///<summary>The drive wheel configuration, i.e. which roadwheels will receive torque from the vehicle's engine via the drivetrain.</summary>
		public OneOfThese<Text , DriveWheelConfigurationValue> DriveWheelConfiguration {get; set;}
		///<summary>The amount of fuel consumed for traveling a particular distance or temporal duration with the given vehicle (e.g. liters per 100 km).Note 1: There are unfortunately no standard unit codes for liters per 100 km.Use unitText to indicate the unit of measurement, e.g. L/100 km.Note 2: There are two ways of indicating the fuel consumption, fuelConsumption (e.g. 8 liters per 100 km) and fuelEfficiency (e.g. 30 miles per gallon). They are reciprocal.Note 3: Often, the absolute value is useful only when related to driving speed ("at 80 km/h") or usage pattern ("city traffic"). You can use valueReference to link the value for the fuel consumption to another value.</summary>
		public QuantitativeValue FuelConsumption {get; set;}
		///<summary>The distance traveled per unit of fuel used; most commonly miles per gallon (mpg) or kilometers per liter (km/L).Note 1: There are unfortunately no standard unit codes for miles per gallon or kilometers per liter.Use unitText to indicate the unit of measurement, e.g. mpg or km/L.Note 2: There are two ways of indicating the fuel consumption, fuelConsumption (e.g. 8 liters per 100 km) and fuelEfficiency (e.g. 30 miles per gallon). They are reciprocal.Note 3: Often, the absolute value is useful only when related to driving speed ("at 80 km/h") or usage pattern ("city traffic"). You can use valueReference to link the value for the fuel economy to another value.</summary>
		public QuantitativeValue FuelEfficiency {get; set;}
		///<summary>The type of fuel suitable for the engine or engines of the vehicle. If the vehicle has only one engine, this property can be attached directly to the vehicle.</summary>
		public OneOfThese<QualitativeValue , Text , URL> FuelType {get; set;}
		///<summary>A textual description of known damages, both repaired and unrepaired.</summary>
		public Text KnownVehicleDamages {get; set;}
		///<summary>The total distance travelled by the particular vehicle since its initial production, as read from its odometer.Typical unit code(s): KMT for kilometers, SMI for statute miles</summary>
		public QuantitativeValue MileageFromOdometer {get; set;}
		///<summary>The number or type of airbags in the vehicle.</summary>
		public OneOfThese<Text , Number> NumberOfAirbags {get; set;}
		///<summary>The number of axles.Typical unit code(s): C62</summary>
		public OneOfThese<QuantitativeValue , Number> NumberOfAxles {get; set;}
		///<summary>The number of doors.Typical unit code(s): C62</summary>
		public OneOfThese<QuantitativeValue , Number> NumberOfDoors {get; set;}
		///<summary>The total number of forward gears available for the transmission system of the vehicle.Typical unit code(s): C62</summary>
		public OneOfThese<QuantitativeValue , Number> NumberOfForwardGears {get; set;}
		///<summary>The number of owners of the vehicle, including the current one.Typical unit code(s): C62</summary>
		public OneOfThese<QuantitativeValue , Number> NumberOfPreviousOwners {get; set;}
		///<summary>The date of production of the item, e.g. vehicle.</summary>
		public Date ProductionDate {get; set;}
		///<summary>The date the item e.g. vehicle was purchased by the current owner.</summary>
		public Date PurchaseDate {get; set;}
		///<summary>The position of the steering wheel or similar device (mostly for cars).</summary>
		public SteeringPositionValue SteeringPosition {get; set;}
		///<summary>A short text indicating the configuration of the vehicle, e.g. '5dr hatchback ST 2.5 MT 225 hp' or 'limited edition'.</summary>
		public Text VehicleConfiguration {get; set;}
		///<summary>Information about the engine or engines of the vehicle.</summary>
		public EngineSpecification VehicleEngine {get; set;}
		///<summary>The Vehicle Identification Number (VIN) is a unique serial number used by the automotive industry to identify individual motor vehicles.</summary>
		public Text VehicleIdentificationNumber {get; set;}
		///<summary>The color or color combination of the interior of the vehicle.</summary>
		public Text VehicleInteriorColor {get; set;}
		///<summary>The type or material of the interior of the vehicle (e.g. synthetic fabric, leather, wood, etc.). While most interior types are characterized by the material used, an interior type can also be based on vehicle usage or target audience.</summary>
		public Text VehicleInteriorType {get; set;}
		///<summary>The release date of a vehicle model (often used to differentiate versions of the same make and model).</summary>
		public Date VehicleModelDate {get; set;}
		///<summary>The number of passengers that can be seated in the vehicle, both in terms of the physical space available, and in terms of limitations set by law.Typical unit code(s): C62 for persons.</summary>
		public OneOfThese<QuantitativeValue , Number> VehicleSeatingCapacity {get; set;}
		///<summary>The type of component used for transmitting the power from a rotating power source to the wheels or other relevant component(s) ("gearbox" for cars).</summary>
		public OneOfThese<QualitativeValue , Text , URL> VehicleTransmission {get; set;}
	}
}
