using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Computer programming source code. Example: Full (compile ready) solutions, code snippet samples, scripts, templates.</summary>
	public class SoftwareSourceCode: CreativeWork
	{
		///<summary>Link to the repository where the un-compiled, human readable code and related code is located (SVN, github, CodePlex).</summary>
		public Url CodeRepository {get; set;}
		///<summary>What type of code sample: full (compile ready) solution, code snippet, inline code, scripts, template. Supersedes sampleType.</summary>
		public Text CodeSampleType {get; set;}
		///<summary>The computer programming language.</summary>
		public OneOfThese<ComputerLanguage,Text> ProgrammingLanguage {get; set;}
		///<summary>Runtime platform or script interpreter dependencies (Example - Java v1, Python2.3, .Net Framework 3.0). Supersedes runtime.</summary>
		public Text RuntimePlatform {get; set;}
		///<summary>Target Operating System / Product to which the code applies.  If applies to several versions, just the product name can be used.</summary>
		public SoftwareApplication TargetProduct {get; set;}
	}
}
