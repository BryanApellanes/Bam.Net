using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A web page that provides medical information.</summary>
	public class MedicalWebPage: WebPage
	{
		///<summary>An aspect of medical practice that is considered on the page, such as 'diagnosis', 'treatment', 'causes', 'prognosis', 'etiology', 'epidemiology', etc.</summary>
		public Text Aspect {get; set;}
	}
}
