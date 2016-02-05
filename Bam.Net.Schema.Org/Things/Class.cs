using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A class, also often called a 'Type'; equivalent to rdfs:Class.</summary>
	public class Class: Intangible
	{
		///<summary>Relates a term (i.e. a property, class or enumeration) to one that supersedes it.</summary>
		public ThisOrThat<Class , Property , Enumeration> SupersededBy {get; set;}
	}
}
