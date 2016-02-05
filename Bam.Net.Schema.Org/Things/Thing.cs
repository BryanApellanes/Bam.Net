using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The most generic type of item.</summary>
	public class Thing: DataType
	{
		///<summary>An additional type for the item, typically used for adding more specific types from external vocabularies in microdata syntax. This is a relationship between something and a class that the thing is in. In RDFa syntax, it is better to use the native RDFa syntax - the 'typeof' attribute - for multiple types. Schema.org tools may have only weaker understanding of extra types, in particular those defined externally.</summary>
		public URL AdditionalType {get; set;}
		///<summary>An alias for the item.</summary>
		public Text AlternateName {get; set;}
		///<summary>A short description of the item.</summary>
		public Text Description {get; set;}
		///<summary>An image of the item. This can be a URL or a fully described ImageObject.</summary>
		public ThisOrThat<ImageObject , URL> Image {get; set;}
		///<summary>Indicates a page (or other CreativeWork) for which this thing is the main entity being described.            See background notes for details.       Inverse property: mainEntity.</summary>
		public ThisOrThat<CreativeWork , URL> MainEntityOfPage {get; set;}
		///<summary>The name of the item.</summary>
		public Text Name {get; set;}
		///<summary>Indicates a potential Action, which describes an idealized action in which this thing would play an 'object' role.</summary>
		public Action PotentialAction {get; set;}
		///<summary>URL of a reference Web page that unambiguously indicates the item's identity. E.g. the URL of the item's Wikipedia page, Freebase page, or official website.</summary>
		public URL SameAs {get; set;}
		///<summary>URL of the item.</summary>
		public URL Url {get; set;}
	}
}
