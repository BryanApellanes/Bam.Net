using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Lists or enumerations—for example, a list of cuisines or music genres, etc.</summary>
	public class Enumeration: Intangible
	{
		///<summary>Relates a term (i.e. a property, class or enumeration) to one that supersedes it.</summary>
		public OneOfThese<Property , Enumeration , Class> SupersededBy {get; set;}
	}
}
