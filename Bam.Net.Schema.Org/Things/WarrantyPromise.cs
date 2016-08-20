using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A structured value representing the duration and scope of services that will be provided to a customer free of charge in case of a defect or malfunction of a product.</summary>
	public class WarrantyPromise: StructuredValue
	{
		///<summary>The duration of the warranty promise. Common unitCode values are ANN for year, MON for months, or DAY for days.</summary>
		public QuantitativeValue DurationOfWarranty {get; set;}
		///<summary>The scope of the warranty promise.</summary>
		public WarrantyScope WarrantyScope {get; set;}
	}
}
