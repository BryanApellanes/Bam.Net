using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A television station.</summary>
	public class TelevisionStation: LocalBusiness
	{
		///<summary>The type of screening or video broadcast used (e.g. IMAX, 3D, SD, HD, etc.).</summary>
		public Text VideoFormat {get; set;}
	}
}
