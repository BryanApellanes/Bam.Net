using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>An accommodation is a place that can accommodate human beings, e.g. a hotel room, a camping pitch, or a meeting room. Many accommodations are for overnight stays, but this is not a mandatory requirement.For more specific types of accommodations not defined in schema.org, one can use additionalType with external vocabularies.See also the dedicated document on the use of schema.org for marking up hotels and other forms of accommodations.</summary>
	public class Accommodation: Place
	{
		///<summary>An amenity feature (e.g. a characteristic or service) of the Accommodation. This generic property does not make a statement about whether the feature is included in an offer for the main accommodation or available at extra costs.</summary>
		public LocationFeatureSpecification AmenityFeature {get; set;}
		///<summary>The size of the accommodation, e.g. in square meter or squarefoot.Typical unit code(s): MTK for square meter, FTK for square foot, or YDK for square yard</summary>
		public QuantitativeValue FloorSize {get; set;}
		///<summary>The number of rooms (excluding bathrooms and closets) of the acccommodation or lodging business.Typical unit code(s): ROM for room or C62 for no unit. The type of room can be put in the unitText property of the QuantitativeValue.</summary>
		public OneOfThese<Number,QuantitativeValue> NumberOfRooms {get; set;}
		///<summary>Indications regarding the permitted usage of the accommodation.</summary>
		public Text PermittedUsage {get; set;}
		///<summary>Indicates whether pets are allowed to enter the accommodation or lodging business. More detailed information can be put in a text value.</summary>
		public OneOfThese<Boolean,Text> PetsAllowed {get; set;}
	}
}
