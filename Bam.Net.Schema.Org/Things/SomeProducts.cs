using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A placeholder for multiple similar products of the same kind.</summary>
	public class SomeProducts: Product
	{
		///<summary>The current approximate inventory level for the item or items.</summary>
		public QuantitativeValue InventoryLevel {get; set;}
	}
}
