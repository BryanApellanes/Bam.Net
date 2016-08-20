using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A structured value providing information about when a certain organization or person owned a certain product.</summary>
	public class OwnershipInfo: StructuredValue
	{
		///<summary>The organization or person from which the product was acquired.</summary>
		public OneOfThese<Organization,Person> AcquiredFrom {get; set;}
		///<summary>The date and time of obtaining the product.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date OwnedFrom {get; set;}
		///<summary>The date and time of giving up ownership on the product.</summary>
		public Bam.Net.Schema.Org.DataTypes.Date OwnedThrough {get; set;}
		///<summary>The product that this structured value is referring to.</summary>
		public OneOfThese<Product,Service> TypeOfGood {get; set;}
	}
}
