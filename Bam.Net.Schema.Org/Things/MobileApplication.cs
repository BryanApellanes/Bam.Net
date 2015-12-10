/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A software application designed specifically to work well on a mobile device such as a telephone.</summary>
	public class MobileApplication: SoftwareApplication
	{
		///<summary>Specifies specific carrier(s) requirements for the application (e.g. an application may only work on a specific carrier network).</summary>
		public Text CarrierRequirements {get; set;}
	}
}
