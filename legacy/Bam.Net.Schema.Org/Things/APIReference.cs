using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>Reference documentation for application programming interfaces (APIs).</summary>
	public class APIReference: TechArticle
	{
		///<summary>Associated product/technology version. e.g., .NET Framework 4.5.</summary>
		public Text AssemblyVersion {get; set;}
		///<summary>Library file name e.g., mscorlib.dll, system.web.dll. Supersedes assembly.</summary>
		public Text ExecutableLibraryName {get; set;}
		///<summary>Indicates whether API is managed or unmanaged.</summary>
		public Text ProgrammingModel {get; set;}
		///<summary>Type of app development: phone, Metro style, desktop, XBox, etc.</summary>
		public Text TargetPlatform {get; set;}
	}
}
