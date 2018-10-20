using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A datasheet or vendor specification of a product (in the sense of a prototypical description).</summary>
	public class ProductModel: Product
	{
		///<summary>A pointer to a base product from which this product is a variant. It is safe to infer that the variant inherits all product features from the base model, unless defined locally. This is not transitive.</summary>
		public ProductModel IsVariantOf {get; set;}
		///<summary>A pointer from a previous, often discontinued variant of the product to its newer variant.</summary>
		public ProductModel PredecessorOf {get; set;}
		///<summary>A pointer from a newer variant of a product  to its previous, often discontinued predecessor.</summary>
		public ProductModel SuccessorOf {get; set;}
	}
}
