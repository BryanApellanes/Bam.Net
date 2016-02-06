using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of authoring written creative content.</summary>
	public class WriteAction: CreateAction
	{
		///<summary>The language of the content or performance or used in an action. Please use one of the language codes from the IETF BCP 47 standard. Supersedes language.</summary>
		public OneOfThese<Text , Language> InLanguage {get; set;}
	}
}
