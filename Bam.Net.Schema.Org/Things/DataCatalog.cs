using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A collection of datasets.</summary>
	public class DataCatalog: CreativeWork
	{
		///<summary>A dataset contained in this catalog. Inverse property: includedInDataCatalog.</summary>
		public Dataset Dataset {get; set;}
	}
}
