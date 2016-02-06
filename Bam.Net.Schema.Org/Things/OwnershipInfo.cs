/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A structured value providing information about when a certain organization or person owned a certain product.</summary>
	public class OwnershipInfo: StructuredValue
	{
		///<summary>The organization or person from which the product was acquired.</summary>
		public ThisOrThat<Person , Organization> AcquiredFrom {get; set;}
		///<summary>The date and time of obtaining the product.</summary>
		public DateTime OwnedFrom {get; set;}
		///<summary>The date and time of giving up ownership on the product.</summary>
		public DateTime OwnedThrough {get; set;}
		///<summary>The product that this structured value is referring to.</summary>
		public Product TypeOfGood {get; set;}
	}
}
