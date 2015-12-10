/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>A body of structured information describing some topic(s) of interest.</summary>
	public class Dataset: CreativeWork
	{
		///<summary>A data catalog which contains a dataset.</summary>
		public DataCatalog Catalog {get; set;}
		///<summary>A downloadable form of this dataset, at a specific location, in a specific format.</summary>
		public DataDownload Distribution {get; set;}
		///<summary>The range of spatial applicability of a dataset, e.g. for a dataset of New York weather, the state of New York.</summary>
		public Place Spatial {get; set;}
		///<summary>The range of temporal applicability of a dataset, e.g. for a 2011 census dataset, the year 2011 (in ISO 8601 time interval format).</summary>
		public DateTime Temporal {get; set;}
	}
}
