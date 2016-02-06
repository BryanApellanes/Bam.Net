using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A property, used to indicate attributes and relationships of some Thing; equivalent to rdf:Property.</summary>
	public class Property: Intangible
	{
		///<summary>Relates a property to a class that is (one of) the type(s) the property is expected to be used on.</summary>
		public Class DomainIncludes {get; set;}
		///<summary>Relates a property to a property that is its inverse. Inverse properties relate the same pairs of items to each other, but in reversed direction. For example, the 'alumni' and 'alumniOf' properties are inverseOf each other. Some properties don't have explicit inverses; in these situations RDFa and JSON-LD syntax for reverse properties can be used.</summary>
		public Property InverseOf {get; set;}
		///<summary>Relates a property to a class that constitutes (one of) the expected type(s) for values of the property.</summary>
		public Class RangeIncludes {get; set;}
		///<summary>Relates a term (i.e. a property, class or enumeration) to one that supersedes it.</summary>
		public OneOfThese<Enumeration , Class , Property> SupersededBy {get; set;}
	}
}
