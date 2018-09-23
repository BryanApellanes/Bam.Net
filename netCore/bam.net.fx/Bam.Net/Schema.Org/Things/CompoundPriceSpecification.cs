using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A compound price specification is one that bundles multiple prices that all apply in combination for different dimensions of consumption. Use the name property of the attached unit price specification for indicating the dimension of a price component (e.g. "electricity" or "final cleaning").</summary>
	public class CompoundPriceSpecification: PriceSpecification
	{
		///<summary>This property links to all UnitPriceSpecification nodes that apply in parallel for the CompoundPriceSpecification node.</summary>
		public UnitPriceSpecification PriceComponent {get; set;}
	}
}
