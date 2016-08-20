using Bam.Net.Schema.Org.DataTypes;

namespace Bam.Net.Schema.Org.Things
{
	///<summary>A body of structured information describing some topic(s) of interest.</summary>
	public class Dataset: CreativeWork
	{
		///<summary>A downloadable form of this dataset, at a specific location, in a specific format.</summary>
		public DataDownload Distribution {get; set;}
		///<summary>A data catalog which contains this dataset. Supersedes catalog, includedDataCatalog. Inverse property: dataset.</summary>
		public DataCatalog IncludedInDataCatalog {get; set;}
	}
}
