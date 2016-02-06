/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The geographic coordinates of a place or event.</summary>
	public class GeoCoordinates: StructuredValue
	{
		///<summary>The elevation of a location.</summary>
		public OneOfThese<Number , Text> Elevation {get; set;}
		///<summary>The latitude of a location. For example 37.42242.</summary>
		public OneOfThese<Number , Text> Latitude {get; set;}
		///<summary>The longitude of a location. For example -122.08585.</summary>
		public OneOfThese<Number , Text> Longitude {get; set;}
	}
}
