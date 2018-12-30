using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>The act of authoring written creative content.</summary>
	public class WriteAction: CreateAction
	{
		///<summary>The language of the content or performance or used in an action. Please use one of the language codes from the IETF BCP 47 standard. See also availableLanguage. Supersedes language.</summary>
		public OneOfThese<Language,Text> InLanguage {get; set;}
	}
}
