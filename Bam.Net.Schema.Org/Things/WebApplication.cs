using System;

namespace Bam.Net.Schema.Org
{
	///<summary>Web applications.</summary>
	public class WebApplication: SoftwareApplication
	{
		///<summary>Specifies browser requirements in human-readable text. For example,"requires HTML5 support".</summary>
		public Text BrowserRequirements {get; set;}
	}
}
