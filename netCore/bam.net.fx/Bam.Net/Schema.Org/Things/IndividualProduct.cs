using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A single, identifiable product instance (e.g. a laptop with a particular serial number).</summary>
	public class IndividualProduct: Product
	{
		///<summary>The serial number or any alphanumeric identifier of a particular product. When attached to an offer, it is a shortcut for the serial number of the product included in the offer.</summary>
		public Text SerialNumber {get; set;}
	}
}
